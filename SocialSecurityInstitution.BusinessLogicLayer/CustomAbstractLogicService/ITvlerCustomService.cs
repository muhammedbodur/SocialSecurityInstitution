using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface ITvlerCustomService
    {
        Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithHizmetBinasiId(int hizmetBinasiId);
        Task<List<TvlerDetailDto>> GetTvlerWithHizmetBinasiId(int hizmetBinasiId);
        Task<TvlerDetailDto> GetTvWithTvId(int tvId);
        Task<List<TvlerBankolarRequestDto>> GetTvlerBankolarWithSayiAsync(int hizmetBinasiId);
        Task<List<BankolarDto>> GetTvBankolarEslesmeyenleriGetirAsync(int tvId, int hizmetBinasiId);
        Task<List<TvBankolarRequestDto>> GetTvBankolarEslesenleriGetirAsync(int tvId);
        Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankoId(int bankoId);
        Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankolarKullaniciTcKimlikNo(string tcKimlikNo);
    }
}
