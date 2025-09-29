using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface ISiralarCustomService
    {
        Task<SiraNoBilgisiDto> GetSiraNoAsync(int kanalAltIslemId);
        Task<List<siraCagirmaDto>> GetSiraListeAsync(string tcKimlikNo);
        Task<siraCagirmaDto?> GetSiraCagirmaAsync(string tcKimlikNo);
        Task<List<SiralarDto>> GetSiralarWithHizmetBinasiAsync(int hizmetBinasiId);
        Task<List<SiralarTvListeDto>> GetSiralarForTvWithHizmetBinasi(int hizmetBinasiId);
        Task<List<SiralarTvListeDto>> GetSiralarForTvWithTvId(int tvId);
    }
}
