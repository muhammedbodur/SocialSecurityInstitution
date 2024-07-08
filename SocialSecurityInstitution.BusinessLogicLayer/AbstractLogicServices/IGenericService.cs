using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices
{
    public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<InsertResult> TInsertAsync(TDto dto);
        Task<bool> TUpdateAsync(TDto dto);
        Task<bool> TDeleteAsync(TDto dto);
        Task<TDto> TGetByIdAsync(int id);
        Task<List<TDto>> TGetAllAsync();
        Task<bool> TContainsAsync(TDto dto);
        Task<int> TCountAsync();
    }
}
