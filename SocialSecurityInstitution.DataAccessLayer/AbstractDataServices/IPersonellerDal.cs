using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IPersonellerDal : IGenericDal<PersonellerDto>
    {
        Task<List<PersonellerDto>> GetPersonellerByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<PersonelAltKanallarDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo);
        Task<List<PersonelAltKanallarDto>> GetPersonelAltKanallariAsync(string tcKimlikNo);
        Task<List<PersonellerAltKanallarDto>> GetPersonellerAltKanallarAsync(int hizmetBinasiId);
        Task<List<KanalPersonelleriViewRequestDto>> GetKanalPersonelleriViewByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<LoginDto> AuthenticateUserAsync(string tcKimlikNo, string password);
        Task<List<PersonellerDto>> GetPersonellerByDepartmanAndHizmetBinasiAsync(int departmanId, int hizmetBinasiId);
        Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync();
        Task<PersonellerDto> GetByTcKimlikNoAsync(string tcKimlikNo);
        Task<PersonellerDto> UpdateSessionIdAsync(string tcKimlikNo, string newSessionId);
        Task<List<PersonellerLiteDto>> GetActivePersonelListAsync();
        Task<PersonellerDto> GetPersonelWithDetailsByTcKimlikNoAsync(string tcKimlikNo);
        Task<PersonellerViewDto> GetPersonelViewForEditAsync(string tcKimlikNo);
    }
}
