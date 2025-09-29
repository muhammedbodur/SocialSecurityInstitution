using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class TvlerCustomService : ITvlerCustomService
    {
        private readonly ITvlerDal _tvlerDal;
        private readonly ITvlerService _tvlerService;
        private readonly ILogger<TvlerCustomService> _logger;

        public TvlerCustomService(ITvlerDal tvlerDal, ITvlerService tvlerService, ILogger<TvlerCustomService> logger)
        {
            _tvlerDal = tvlerDal ?? throw new ArgumentNullException(nameof(tvlerDal));
            _tvlerService = tvlerService ?? throw new ArgumentNullException(nameof(tvlerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<TvBankolarRequestDto>> GetTvBankolarEslesenleriGetirAsync(int tvId)
        {
            try
            {
                _logger.LogInformation("Getting TV bankolar eslesenleri for TvId: {TvId}", tvId);

                // Business validation
                if (tvId <= 0)
                {
                    _logger.LogWarning("GetTvBankolarEslesenleriGetirAsync failed: Invalid TvId: {TvId}", tvId);
                    return new List<TvBankolarRequestDto>();
                }

                // Get TV bankolar through repository
                var bankolarList = await _tvlerDal.GetTvBankolarEslesenleriGetirAsync(tvId);

                _logger.LogInformation("Retrieved {Count} TV bankolar eslesenleri for TvId: {TvId}", 
                    bankolarList.Count, tvId);

                return bankolarList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TV bankolar eslesenleri for TvId: {TvId}", tvId);
                throw;
            }
        }

        public async Task<List<BankolarDto>> GetTvBankolarEslesmeyenleriGetirAsync(int tvId, int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting TV bankolar eslesmeyenleri for TvId: {TvId}, HizmetBinasiId: {HizmetBinasiId}", tvId, hizmetBinasiId);

                // Business validation
                if (tvId <= 0 || hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetTvBankolarEslesmeyenleriGetirAsync failed: Invalid parameters TvId: {TvId}, HizmetBinasiId: {HizmetBinasiId}", tvId, hizmetBinasiId);
                    return new List<BankolarDto>();
                }

                // Get unmatched bankolar through repository
                var bankolarList = await _tvlerDal.GetTvBankolarEslesmeyenleriGetirAsync(tvId, hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} unmatched bankolar for TvId: {TvId}, HizmetBinasiId: {HizmetBinasiId}", 
                    bankolarList.Count, tvId, hizmetBinasiId);

                return bankolarList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting unmatched bankolar for TvId: {TvId}, HizmetBinasiId: {HizmetBinasiId}", tvId, hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<TvlerBankolarRequestDto>> GetTvlerBankolarWithSayiAsync(int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting TV bankolar with sayi for HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);

                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetTvlerBankolarWithSayiAsync failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<TvlerBankolarRequestDto>();
                }

                // Get TV bankolar with count through repository
                var tvlerBankolarSayiList = await _tvlerDal.GetTvlerBankolarWithSayiAsync(hizmetBinasiId);

                // Map TvlerBankolarSayiDto to TvlerBankolarRequestDto
                var tvlerBankolarRequestList = tvlerBankolarSayiList.Select(t => new TvlerBankolarRequestDto
                {
                    TvId = t.TvId,
                    KatTipi = t.KatTipi,
                    KatTipiAdi = t.KatTipiAdi ?? "",
                    BankoEslesmeSayisi = t.BankoEslesmeSayisi
                }).ToList();

                _logger.LogInformation("Retrieved {Count} TV bankolar with sayi for HizmetBinasiId: {HizmetBinasiId}", 
                    tvlerBankolarRequestList.Count, hizmetBinasiId);

                return tvlerBankolarRequestList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TV bankolar with sayi for HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankoId(int bankoId)
        {
            try
            {
                _logger.LogInformation("Getting TV connections for BankoId: {BankoId}", bankoId);

                // Business validation
                if (bankoId <= 0)
                {
                    _logger.LogWarning("GetTvlerConnectionWithBankoId failed: Invalid BankoId: {BankoId}", bankoId);
                    return new List<TvlerWithConnectionIdDto>();
                }

                // Get TV connections through repository
                var tvlerConnectionsList = await _tvlerDal.GetTvlerConnectionWithBankoIdAsync(bankoId);

                _logger.LogInformation("Retrieved {Count} TV connections for BankoId: {BankoId}", 
                    tvlerConnectionsList.Count, bankoId);

                return tvlerConnectionsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TV connections for BankoId: {BankoId}", bankoId);
                throw;
            }
        }

        public async Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankolarKullaniciTcKimlikNo(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Getting TV connections for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("GetTvlerConnectionWithBankolarKullaniciTcKimlikNo failed: TcKimlikNo is null or empty");
                    return new List<TvlerWithConnectionIdDto>();
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("GetTvlerConnectionWithBankolarKullaniciTcKimlikNo failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return new List<TvlerWithConnectionIdDto>();
                }

                // Get TV connections through repository
                var tvlerConnectionsList = await _tvlerDal.GetTvlerConnectionWithBankolarKullaniciTcKimlikNoAsync(tcKimlikNo);

                _logger.LogInformation("Retrieved {Count} TV connections for TcKimlikNo: {TcKimlikNo}", 
                    tvlerConnectionsList.Count, tcKimlikNo);

                return tvlerConnectionsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TV connections for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithHizmetBinasiId(int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting TV connections for HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);

                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetTvlerConnectionWithHizmetBinasiId failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<TvlerWithConnectionIdDto>();
                }

                // Get TV connections through repository
                var tvlerConnectionsList = await _tvlerDal.GetTvlerConnectionWithHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} TV connections for HizmetBinasiId: {HizmetBinasiId}", 
                    tvlerConnectionsList.Count, hizmetBinasiId);

                return tvlerConnectionsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TV connections for HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<TvlerDetailDto>> GetTvlerWithHizmetBinasiId(int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting TVler details for HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);

                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetTvlerWithHizmetBinasiId failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<TvlerDetailDto>();
                }

                // Get TVler details through repository
                var tvlerDetailsList = await _tvlerDal.GetTvlerWithHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} TVler details for HizmetBinasiId: {HizmetBinasiId}", 
                    tvlerDetailsList.Count, hizmetBinasiId);

                return tvlerDetailsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TVler details for HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<TvlerDetailDto> GetTvWithTvId(int tvId)
        {
            try
            {
                _logger.LogInformation("Getting TV details for TvId: {TvId}", tvId);

                // Business validation
                if (tvId <= 0)
                {
                    _logger.LogWarning("GetTvWithTvId failed: Invalid TvId: {TvId}", tvId);
                    return null;
                }

                // Get TV details through repository
                var tvDetails = await _tvlerDal.GetTvWithTvIdAsync(tvId);

                if (tvDetails != null)
                {
                    _logger.LogInformation("Retrieved TV details for TvId: {TvId}", tvId);
                }
                else
                {
                    _logger.LogWarning("No TV found for TvId: {TvId}", tvId);
                }

                return tvDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting TV details for TvId: {TvId}", tvId);
                throw;
            }
        }
    }
}
