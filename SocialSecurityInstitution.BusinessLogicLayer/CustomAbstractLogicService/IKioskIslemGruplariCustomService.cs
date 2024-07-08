using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IKioskIslemGruplariCustomService
    {
        Task<List<KioskIslemGruplariRequestDto>> GetKioskIslemGruplariAsync(int hizmetBinasiId);
        Task<KioskIslemGruplariRequestDto> GetKioskIslemGruplariByIdAsync(int kioskIslemGrupId);
        Task<List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>> GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKioskIslemGruplariKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(int kioskIslemGrupId);
        Task<List<KanalAltIslemleriRequestDto>> GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(int kioskIslemGrupId);
    }
}
