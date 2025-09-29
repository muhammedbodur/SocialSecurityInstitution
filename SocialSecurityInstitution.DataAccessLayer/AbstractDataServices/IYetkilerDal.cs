using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IYetkilerDal : IGenericDal<YetkilerDto>
    {
        Task<List<YetkilerWithPersonelDto>> GetYetkilerByPersonelTcKimlikNoAsync(string tcKimlikNo);
        Task<List<YetkilerDto>> GetOrtaYetkilerByAnaYetkiIdAsync(int anaYetkiId);
        Task<List<YetkilerDto>> GetAltYetkilerByOrtaYetkiIdAsync(int ortaYetkiId);
        Task<List<YetkilerDto>> GetAllYetkilerWithIncludesAsync();
        Task<List<PersonelYetkileriDto>> GetPersonelYetkileriAsync(string tcKimlikNo);
    }
}
