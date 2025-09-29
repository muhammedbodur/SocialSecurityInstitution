using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IKioskIslemGruplariDal : IGenericDal<KioskIslemGruplariDto>
    {
        Task<List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>> GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(int hizmetBinasiId);
        Task<List<KioskIslemGruplariRequestDto>> GetKioskIslemGruplariByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<KioskIslemGruplariRequestDto> GetKioskIslemGruplariByIdWithDetailsAsync(int kioskIslemGrupId);
    }
}
