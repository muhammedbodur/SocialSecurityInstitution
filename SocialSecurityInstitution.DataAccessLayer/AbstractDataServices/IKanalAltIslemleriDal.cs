using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IKanalAltIslemleriDal : IGenericDal<KanalAltIslemleriDto>
    {
        Task<List<KanalAltIslemleriDto>> GetAllKanalAltIslemleriAsync();
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<KanalAltIslemleriRequestDto> GetKanalAltIslemleriByIdWithDetailsAsync(int kanalAltIslemId);
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByIslemIdAsync(int kanalIslemId);
        Task<List<KanalAltIslemleriEslestirmeSayisiRequestDto>> GetKanalAltIslemleriEslestirmeSayisiAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId);
        
        // Kiosk related methods
        Task<List<KanalAltIslemleriRequestDto>> GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKioskIslemGruplariKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(int kioskIslemGrupId);
    }
}
