using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IPersonelCustomService
    {
        Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync();
        Task<PersonellerDto> TGetByTcKimlikNoAsync(string tcKimlikNo);
        Task<List<PersonellerDto>> GetPersonellerDepartmanIdAndHizmetBinasiIdAsync(int departmanId, int hizmetBinasiId);
        Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<PersonellerDto> UpdateSessionIDAsync(string tcKimlikNo, string newSessionId);
        Task<List<PersonellerLiteDto>> GetActivePersonelListAsync();
        Task<PersonellerViewDto> GetPersonelViewForEditAsync(string tcKimlikNo);
    }
}
