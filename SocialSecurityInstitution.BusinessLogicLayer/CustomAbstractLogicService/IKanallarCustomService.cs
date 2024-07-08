using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IKanallarCustomService
    {
        Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriAsync(int hizmetBinasiId);
        Task<KanalIslemleriRequestDto> GetKanalIslemleriByIdAsync(int kanalIslemId);
        Task<List<KanalAltIslemleriDto>> GetKanalAltIslemleriAsync();
        Task<List<KanalPersonelleriViewDto>> GetKanalPersonelleriAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriAsync(int hizmetBinasiId);
        Task<KanalAltIslemleriRequestDto> GetKanalAltIslemleriByIdAsync(int kanalAltIslemId);
        Task<List<KanalAltIslemleriEslestirmeSayisiRequestDto>> GetKanalAltIslemleriEslestirmeSayisiAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByIslemIdAsync(int kanalIslemId);
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId);
    }
}
