using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IGenericDal<TEntity> where TEntity : class
    {
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<bool> ContainsAsync(TEntity entity);
        Task<int> CountAsync();
    }
}
