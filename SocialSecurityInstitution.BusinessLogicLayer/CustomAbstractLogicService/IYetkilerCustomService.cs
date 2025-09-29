using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IYetkilerCustomService
    {
        Task<List<YetkilerDto>> GetOrtaYetkilerByAnaYetkiIdAsync(int anaYetkiId);

        Task<List<YetkilerDto>> GetAltYetkilerByOrtaYetkiIdAsync(int ortaYetkiId);

        Task<List<PersonelYetkileriDto>> GetPersonelYetkileriAsync(string tcKimlikNo);

        Task<List<YetkilerDto>> GetAllYetkilerWithIncludesAsync();
    }
}