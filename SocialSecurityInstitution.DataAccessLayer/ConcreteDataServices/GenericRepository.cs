using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class GenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class
    {
        public async Task<List<TEntity>> GetAllAsync()
        {
            using var context = new Context();
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using var context = new Context();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using var context = new Context();
            context.Set<TEntity>().Remove(entity);
            int affectedRows = await context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            using var context = new Context();
            await context.Set<TEntity>().AddAsync(entity);
            int affectedRows = await context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using var context = new Context();
            context.Entry(entity).State = EntityState.Modified;
            int affectedRows = await context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> ContainsAsync(TEntity entity)
        {
            using var context = new Context();
            return await context.Set<TEntity>().ContainsAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            using var context = new Context();
            return await context.Set<TEntity>().CountAsync();
        }
    }
}
