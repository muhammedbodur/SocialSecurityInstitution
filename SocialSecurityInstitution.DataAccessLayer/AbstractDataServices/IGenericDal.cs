using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IGenericDal<TDto> where TDto : class
    {
        Task<InsertResult> InsertAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(TDto dto);
        Task<TDto> GetByIdAsync(int id);
        Task<List<TDto>> GetAllAsync();
        Task<bool> ContainsAsync(TDto dto);
        Task<int> CountAsync();
    }
}
