using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class GenericRepository<TEntity, TDto> : IGenericDal<TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IMapper _mapper;

        public GenericRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            using var context = new Context();
            var entities = await context.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            using var context = new Context();
            var entity = await context.Set<TEntity>().FindAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<InsertResult> InsertAsync(TDto dto)
        {
            using var context = new Context();
            var entity = _mapper.Map<TEntity>(dto);
            await context.Set<TEntity>().AddAsync(entity);
            int affectedRows = await context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                var entityType = context.Model.FindEntityType(typeof(TEntity));
                var key = entityType.FindPrimaryKey();
                var lastPrimaryKeyValue = entity.GetType().GetProperty(key.Properties[0].Name)?.GetValue(entity, null);

                return new InsertResult(true, lastPrimaryKeyValue);
            }
            else
            {
                return new InsertResult(false, null);
            }
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            using var context = new Context();
            var entity = _mapper.Map<TEntity>(dto);
            context.Entry(entity).State = EntityState.Modified;
            int affectedRows = await context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(TDto dto)
        {
            using var context = new Context();
            var entity = _mapper.Map<TEntity>(dto);
            context.Set<TEntity>().Remove(entity);
            int affectedRows = await context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> ContainsAsync(TDto dto)
        {
            using var context = new Context();
            var entity = _mapper.Map<TEntity>(dto);
            return await context.Set<TEntity>().ContainsAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            using var context = new Context();
            return await context.Set<TEntity>().CountAsync();
        }
    }
}
