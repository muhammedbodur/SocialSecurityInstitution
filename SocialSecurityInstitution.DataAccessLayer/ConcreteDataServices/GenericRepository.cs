using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class GenericRepository<TEntity, TDto> : IGenericDal<TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public GenericRepository(Context context, IMapper mapper, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
                return _mapper.Map<List<TDto>>(entities);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>()
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(CreateExpressionForId(id));

                return _mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        public async Task<InsertResult> InsertAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                await _context.Set<TEntity>().AddAsync(entity);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    var entityType = _context.Model.FindEntityType(typeof(TEntity));
                    var key = entityType.FindPrimaryKey();
                    var lastPrimaryKeyValue = entity.GetType().GetProperty(key.Properties[0].Name)?.GetValue(entity, null);

                    return new InsertResult(true, lastPrimaryKeyValue);
                }
                else
                {
                    return new InsertResult(false, null);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var oldEntity = await _context.Set<TEntity>()
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(CreateExpressionForId(Convert.ToInt32(entity.GetType().GetProperty(GetKeyName()).GetValue(entity))));

                var oldData = JsonSerializer.Serialize(oldEntity);

                _context.Entry(entity).State = EntityState.Modified;
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    var afterData = JsonSerializer.Serialize(dto);
                    _logService.LogAction(typeof(TEntity).Name, DatabaseAction.UPDATE, beforeData: oldData, afterData: afterData);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var oldEntity = await _context.Set<TEntity>()
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(CreateExpressionForId(Convert.ToInt32(entity.GetType().GetProperty(GetKeyName()).GetValue(entity))));

                var oldData = JsonSerializer.Serialize(oldEntity);

                _context.Set<TEntity>().Remove(entity);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    _logService.LogAction(typeof(TEntity).Name, DatabaseAction.DELETE, beforeData: oldData);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        public async Task<bool> ContainsAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);

                return await _context.Set<TEntity>()
                                     .AsNoTracking()
                                     .AnyAsync(CreateExpressionForId(Convert.ToInt32(entity.GetType().GetProperty(GetKeyName()).GetValue(entity))));
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.Set<TEntity>().CountAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private void HandleException(Exception ex)
        {
            // Bu metot ileride log dosyasına yazdırma işlevi için genişletilebilir.
            Console.WriteLine($"Hata: {ex.Message}");
        }

        private string GetKeyName()
        {
            return _context.Model.FindEntityType(typeof(TEntity))
                                 .FindPrimaryKey()
                                 .Properties
                                 .Select(p => p.Name)
                                 .FirstOrDefault();
        }

        private Expression<Func<TEntity, bool>> CreateExpressionForId(int id)
        {
            var keyName = GetKeyName();
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, keyName);
            var constant = Expression.Constant(id);
            var body = Expression.Equal(property, constant);

            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }
    }
}
