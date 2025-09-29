using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface ISiralarDal : IGenericDal<SiralarDto>
    {
        Task<siraCagirmaDto?> GetSiraCagirmaAsync(string tcKimlikNo);
        Task<List<SiralarForTvDto>> GetSiralarForTvWithHizmetBinasiAsync(int hizmetBinasiId);
        Task<List<SiralarForTvDto>> GetSiralarForTvWithTvIdAsync(int tvId);
        Task<List<SiralarRequestDto>> GetSiralarWithHizmetBinasiAsync(int hizmetBinasiId);
        Task<List<siraCagirmaDto>> GetSiraListeAsync(string tcKimlikNo);
        Task<SiraNoBilgisiDto> GetSiraNoAsync(int kanalAltIslemId);
    }
}
