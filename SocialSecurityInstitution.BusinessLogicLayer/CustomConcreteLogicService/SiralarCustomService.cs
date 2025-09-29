using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class SiralarCustomService : ISiralarCustomService
    {
        private readonly ISiralarDal _siralarDal;
        private readonly ILogger<SiralarCustomService> _logger;

        public SiralarCustomService(ISiralarDal siralarDal, ILogger<SiralarCustomService> logger)
        {
            _siralarDal = siralarDal ?? throw new ArgumentNullException(nameof(siralarDal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<siraCagirmaDto?> GetSiraCagirmaAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Getting sira cagirma for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("GetSiraCagirma failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                // Get sira cagirma through repository
                var siraCagirma = await _siralarDal.GetSiraCagirmaAsync(tcKimlikNo);

                if (siraCagirma != null)
                {
                    _logger.LogInformation("Sira cagirma successful for TcKimlikNo: {TcKimlikNo}, SiraNo: {SiraNo}",
                        tcKimlikNo, siraCagirma.SiraNo);
                }
                else
                {
                    _logger.LogWarning("No sira found for calling for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }

                return siraCagirma;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sira cagirma for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<SiralarTvListeDto>> GetSiralarForTvWithHizmetBinasi(int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting siralar for TV with HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);

                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetSiralarForTvWithHizmetBinasi failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<SiralarTvListeDto>();
                }

                // Get siralar through repository
                var siralarForTvList = await _siralarDal.GetSiralarForTvWithHizmetBinasiAsync(hizmetBinasiId);

                // ✅ Map SiralarForTvDto to SiralarTvListeDto
                var siralarTvList = siralarForTvList.Select(s => new SiralarTvListeDto
                {
                    SiraId = s.SiraId,
                    SiraNo = s.SiraNo,
                    HizmetBinasiId = s.HizmetBinasiId,
                    SiraAlisZamani = s.SiraAlisZamani,
                    IslemBaslamaZamani = s.IslemBaslamaZamani,
                    BeklemeDurum = s.BeklemeDurum,
                    TcKimlikNo = s.TcKimlikNo,
                    AdSoyad = s.AdSoyad,
                    BankoId = s.BankoId,
                    BankoNo = s.BankoNo,
                    BankoTipi = s.BankoTipi,
                    KatTipi = s.KatTipi,
                    TvId = s.TvId,
                    ConnectionId = s.ConnectionId ?? ""
                }).ToList();

                _logger.LogInformation("Retrieved {Count} siralar for TV with HizmetBinasiId: {HizmetBinasiId}",
                    siralarTvList.Count, hizmetBinasiId);

                return siralarTvList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting siralar for TV with HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<SiralarTvListeDto>> GetSiralarForTvWithTvId(int tvId)
        {
            try
            {
                _logger.LogInformation("Getting siralar for TV with TvId: {TvId}", tvId);

                // Business validation
                if (tvId <= 0)
                {
                    _logger.LogWarning("GetSiralarForTvWithTvId failed: Invalid TvId: {TvId}", tvId);
                    return new List<SiralarTvListeDto>();
                }

                var siralarForTvList = await _siralarDal.GetSiralarForTvWithTvIdAsync(tvId);

                var siralarTvList = siralarForTvList.Select(s => new SiralarTvListeDto
                {
                    SiraId = s.SiraId,
                    SiraNo = s.SiraNo,
                    HizmetBinasiId = s.HizmetBinasiId,
                    SiraAlisZamani = s.SiraAlisZamani,
                    IslemBaslamaZamani = s.IslemBaslamaZamani,
                    BeklemeDurum = s.BeklemeDurum,
                    TcKimlikNo = s.TcKimlikNo,
                    AdSoyad = s.AdSoyad,
                    BankoId = s.BankoId,
                    BankoNo = s.BankoNo,
                    BankoTipi = s.BankoTipi,
                    KatTipi = s.KatTipi,
                    TvId = s.TvId,
                    ConnectionId = s.ConnectionId ?? ""
                }).ToList();

                _logger.LogInformation("Retrieved {Count} siralar for TV with TvId: {TvId}",
                    siralarTvList.Count, tvId);

                return siralarTvList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting siralar for TV with TvId: {TvId}", tvId);
                throw;
            }
        }

        public async Task<List<SiralarDto>> GetSiralarWithHizmetBinasiAsync(int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting siralar with HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);

                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetSiralarWithHizmetBinasiAsync failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<SiralarDto>();
                }

                // Get siralar through repository
                var siralarRequestList = await _siralarDal.GetSiralarWithHizmetBinasiAsync(hizmetBinasiId);

                // ✅ Map SiralarRequestDto to SiralarDto
                var siralarList = siralarRequestList.Select(s => new SiralarDto
                {
                    SiraId = s.SiraId,
                    SiraNo = s.SiraNo,
                    KanalAltIslemId = s.KanalAltIslemId,
                    KanalAltAdi = s.KanalAltAdi,
                    HizmetBinasiId = s.HizmetBinasiId,
                    TcKimlikNo = s.TcKimlikNo,
                    SiraAlisZamani = s.SiraAlisZamani,
                    IslemBaslamaZamani = s.IslemBaslamaZamani,
                    IslemBitisZamani = s.IslemBitisZamani,
                    BeklemeDurum = s.BeklemeDurum
                }).ToList();

                _logger.LogInformation("Retrieved {Count} siralar with HizmetBinasiId: {HizmetBinasiId}",
                    siralarList.Count, hizmetBinasiId);

                return siralarList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting siralar with HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<siraCagirmaDto>> GetSiraListeAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Getting sira liste for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("GetSiraListeAsync failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return new List<siraCagirmaDto>();
                }

                // ✅ Get sira liste through repository - düzeltildi
                var siralarList = await _siralarDal.GetSiraListeAsync(tcKimlikNo);

                _logger.LogInformation("Retrieved {Count} siralar for TcKimlikNo: {TcKimlikNo}",
                    siralarList.Count, tcKimlikNo);

                return siralarList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sira liste for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                return new List<siraCagirmaDto>();
            }
        }

        public async Task<SiraNoBilgisiDto> GetSiraNoAsync(int kanalAltIslemId)
        {
            try
            {
                _logger.LogInformation("Getting sira no for KanalAltIslemId: {KanalAltIslemId}", kanalAltIslemId);

                // Business validation
                if (kanalAltIslemId <= 0)
                {
                    _logger.LogWarning("GetSiraNoAsync failed: Invalid KanalAltIslemId: {KanalAltIslemId}", kanalAltIslemId);
                    return new SiraNoBilgisiDto
                    {
                        SiraNo = 0,
                        HizmetBinasiId = 0,
                        HizmetBinasiAdi = "Invalid KanalAltIslemId!",
                        KanalAltAdi = "Invalid KanalAltIslemId!"
                    };
                }

                // ✅ Get sira no through repository - tek parametre
                var siraNoBilgisi = await _siralarDal.GetSiraNoAsync(kanalAltIslemId);

                if (siraNoBilgisi != null && siraNoBilgisi.SiraNo > 0)
                {
                    _logger.LogInformation("Generated sira no {SiraNo} for KanalAltIslemId: {KanalAltIslemId}",
                        siraNoBilgisi.SiraNo, kanalAltIslemId);
                }
                else
                {
                    _logger.LogWarning("Failed to generate sira no for KanalAltIslemId: {KanalAltIslemId}", kanalAltIslemId);
                }

                return siraNoBilgisi ?? new SiraNoBilgisiDto
                {
                    SiraNo = 0,
                    HizmetBinasiId = 0,
                    HizmetBinasiAdi = "SiraNo generation failed!",
                    KanalAltAdi = "SiraNo generation failed!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting sira no for KanalAltIslemId: {KanalAltIslemId}", kanalAltIslemId);
                return new SiraNoBilgisiDto
                {
                    SiraNo = 0,
                    HizmetBinasiId = 0,
                    HizmetBinasiAdi = "Error occurred!",
                    KanalAltAdi = "Error occurred!"
                };
            }
        }

        // ✅ Private business validation method
        private bool IsValidTcKimlikNo(string tcKimlikNo)
        {
            return !string.IsNullOrWhiteSpace(tcKimlikNo) &&
                   tcKimlikNo.Length == 11 &&
                   tcKimlikNo.All(char.IsDigit) &&
                   tcKimlikNo != "00000000000" &&
                   tcKimlikNo != "11111111111";
        }
    }
}