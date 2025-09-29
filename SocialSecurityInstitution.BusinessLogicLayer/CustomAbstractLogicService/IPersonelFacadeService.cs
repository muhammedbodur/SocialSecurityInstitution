using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IPersonelFacadeService
    {
        Task<(bool Success, string Message)> CreatePersonelAsync(PersonellerDto personellerDto);
        Task<(bool Success, string Message)> UpdatePersonelAsync(PersonellerViewDto personellerViewDto);
        Task<(bool Success, string Message)> DeletePersonelAsync(string tcKimlikNo);

        Task<PersonellerViewDto?> GetPersonelForEditAsync(string tcKimlikNo);
        Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync();

        Task<PersonellerViewDto> CreateEmptyPersonelViewDtoWithDropdownsAsync();
        Task<PersonellerViewDto> PopulateDropdownListsAsync(PersonellerViewDto model);

        Task<List<PersonellerDto>> GetPersonellerByDepartmanAndHizmetBinasiAsync(int departmanId, int hizmetBinasiId);
        Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<PersonellerLiteDto>> GetActivePersonelListAsync();
        Task<PersonellerDto> UpdateSessionIDAsync(string tcKimlikNo, string newSessionId);

        Task<List<IlcelerDto>> GetIlcelerByIlIdAsync(int ilId);
        Task<List<ServislerDto>> GetServislerByDepartmanIdAsync(int departmanId);
        Task<List<AtanmaNedenleriDto>> GetAtanmaNedenleriAsync();

    }
}