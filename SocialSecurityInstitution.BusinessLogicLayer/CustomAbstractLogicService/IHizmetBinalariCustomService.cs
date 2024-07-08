using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IHizmetBinalariCustomService
    {
        Task<List<HizmetBinalariDto>> GetHizmetBinalariByDepartmanIdAsync(int departmanId);
        Task<HizmetBinalariDepartmanlarDto> GetActiveHizmetBinasiAsync(int hizmetBinasiId, int departmanId);
        Task<HizmetBinalariDepartmanlarDto> GetDepartmanHizmetBinasiAsync(int hizmetBinasiId);
    }

}
