using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface ITvlerDal : IGenericDal<TvlerDto>
    {
        Task<List<TvBankolarRequestDto>> GetTvBankolarEslesenleriGetirAsync(int tvId);
        Task<List<BankolarDto>> GetTvBankolarEslesmeyenleriGetirAsync(int tvId, int hizmetBinasiId);
        Task<List<TvlerBankolarSayiDto>> GetTvlerBankolarWithSayiAsync(int hizmetBinasiId);
        Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankoIdAsync(int bankoId);
        Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankolarKullaniciTcKimlikNoAsync(string tcKimlikNo);
        Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<TvlerDetailDto>> GetTvlerWithHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<TvlerDetailDto> GetTvWithTvIdAsync(int tvId);
    }
}
