using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IBankolarKullaniciCustomService
    {
        Task<BankolarKullaniciDto> GetBankolarKullaniciByBankoIdAsync(int bankoId);
        Task<BankolarKullaniciDto> GetBankolarKullaniciByTcKimlikNoAsync(string tcKimlikNo);

        Task<List<BankolarKullaniciDto>> GetActiveBankolarKullaniciByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<bool> IsPersonelAssignedToBankoAsync(string tcKimlikNo);
        Task<List<BankolarKullaniciDto>> GetBankolarByTcKimlikNoAsync(string tcKimlikNo);
    }
}
