using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IBankolarDal : IGenericDal<BankolarDto>
    {
        Task<BankolarRequestDto> GetBankoWithDetailsByIdAsync(int bankoId);
        Task<List<BankolarRequestDto>> GetBankolarWithDetailsAsync();
        Task<List<DepartmanPersonelleriDto>> GetDepartmanPersonelleriByBankoIdAsync(int bankoId);
        Task<List<HizmetBinasiPersonelleriDto>> GetHizmetBinasiPersonelleriByBankoIdAsync(int bankoId);
    }
}
