using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IBankolarCustomService
    {
        Task<List<BankolarRequestDto>> GetBankolarWithDetailsAsync();
        Task<List<DepartmanPersonelleriDto>> GetDeparmanPersonelleriAsync(int bankoId);
        Task<BankolarRequestDto> GetBankoByIdAsync(int bankoId);
        Task<PersonellerDto> GetBankoPersonelDetailAsync(string tcKimlikNo);
    }
}
