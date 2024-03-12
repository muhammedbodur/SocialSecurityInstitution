using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<TEntity> TGetByIdAsync(int id);
        Task<List<TEntity>> TGetAllAsync();
        Task TInsertAsync(TEntity entity);
        Task TUpdateAsync(TEntity entity);
        Task TDeleteAsync(TEntity entity);
        Task<bool> TContainsAsync(TEntity entity);
        Task<int> TCountAsync();
    }
}
