using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IBankolarKullaniciDal : IGenericDal<BankolarKullaniciDto>
    {
        Task<BankolarKullaniciDto> GetBankolarKullaniciByBankoIdAsync(int bankoId);
        Task<BankolarKullaniciDto> GetBankolarKullaniciWithDetailsByTcKimlikNoAsync(string tcKimlikNo);
        Task<List<BankolarKullaniciDto>> GetActiveBankolarKullaniciByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<bool> IsTcKimlikNoAssignedToBankoAsync(string tcKimlikNo);
        Task<List<BankolarKullaniciDto>> GetBankolarByTcKimlikNoAsync(string tcKimlikNo);
    }
}
