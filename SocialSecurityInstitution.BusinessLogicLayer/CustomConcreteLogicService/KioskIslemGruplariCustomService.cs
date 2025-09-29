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
    public class KioskIslemGruplariCustomService : IKioskIslemGruplariCustomService
    {
        private readonly IKioskIslemGruplariDal _kioskIslemGruplariDal;
        private readonly IKanalAltIslemleriDal _kanalAltIslemleriDal;
        private readonly ILogger<KioskIslemGruplariCustomService> _logger;

        public KioskIslemGruplariCustomService(
            IKioskIslemGruplariDal kioskIslemGruplariDal,
            IKanalAltIslemleriDal kanalAltIslemleriDal,
            ILogger<KioskIslemGruplariCustomService> logger)
        {
            _kioskIslemGruplariDal = kioskIslemGruplariDal;
            _kanalAltIslemleriDal = kanalAltIslemleriDal;
            _logger = logger;
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                // Repository'den kiosk alt kanal işlemlerini al
                var result = await _kanalAltIslemleriDal.GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} unmatched kiosk alt kanal islemleri for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving unmatched kiosk alt kanal islemleri for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>> GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>();
                }

                // Repository'den kiosk işlem grupları alt işlemler eşleştirme sayısını al
                var result = await _kioskIslemGruplariDal.GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} kiosk islem gruplari alt islemler eslestirme sayisi for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kiosk islem gruplari alt islemler eslestirme sayisi for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<KioskIslemGruplariRequestDto>> GetKioskIslemGruplariAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KioskIslemGruplariRequestDto>();
                }

                // Repository'den kiosk işlem gruplarını al
                var result = await _kioskIslemGruplariDal.GetKioskIslemGruplariByHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} kiosk islem gruplari for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kiosk islem gruplari for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<KioskIslemGruplariRequestDto> GetKioskIslemGruplariByIdAsync(int kioskIslemGrupId)
        {
            try
            {
                // Business validation
                if (kioskIslemGrupId <= 0)
                {
                    _logger.LogWarning("Invalid kioskIslemGrupId provided: {KioskIslemGrupId}", kioskIslemGrupId);
                    return null;
                }

                // Repository'den kiosk işlem grubunu al
                var result = await _kioskIslemGruplariDal.GetKioskIslemGruplariByIdWithDetailsAsync(kioskIslemGrupId);

                if (result == null)
                {
                    _logger.LogWarning("Kiosk islem grubu not found with ID: {KioskIslemGrupId}", kioskIslemGrupId);
                }
                else
                {
                    _logger.LogInformation("Kiosk islem grubu retrieved: {KioskIslemGrupAdi} for ID: {KioskIslemGrupId}",
                                         result.KioskIslemGrupAdi, kioskIslemGrupId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kiosk islem grubu by ID: {KioskIslemGrupId}", kioskIslemGrupId);
                throw;
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskIslemGruplariKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                // Repository'den kiosk işlem grupları kanal alt işlemlerini al
                var result = await _kanalAltIslemleriDal.GetKioskIslemGruplariKanalAltIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} unmatched kiosk islem gruplari kanal alt islemleri for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving unmatched kiosk islem gruplari kanal alt islemleri for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(int kioskIslemGrupId)
        {
            try
            {
                // Business validation
                if (kioskIslemGrupId <= 0)
                {
                    _logger.LogWarning("Invalid kioskIslemGrupId provided: {KioskIslemGrupId}", kioskIslemGrupId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                // Repository'den kiosk kanal alt işlemlerini al
                var result = await _kanalAltIslemleriDal.GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(kioskIslemGrupId);

                _logger.LogInformation("Retrieved {Count} kiosk kanal alt islemleri for kiosk islem grup: {KioskIslemGrupId}",
                                     result.Count, kioskIslemGrupId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kiosk kanal alt islemleri for kiosk islem grup: {KioskIslemGrupId}", kioskIslemGrupId);
                throw;
            }
        }

        public async Task<List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>> GetActiveKioskGruplariWithSortingAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>();
                }

                // Repository'den kiosk işlem grupları alt işlemler eşleştirme sayısını al
                var result = await _kioskIslemGruplariDal.GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(hizmetBinasiId);

                // ✅ Business logic: Sadece aktif (eşleştirme sayısı > 0) olanları filtrele ve sırala
                var filteredAndSortedResult = result
                    .Where(x => x.EslestirmeSayisi > 0)
                    .OrderBy(x => x.KioskIslemGrupSira)
                    .ToList();

                _logger.LogInformation("Retrieved and filtered {Count} active kiosk gruplari (out of {TotalCount}) for hizmet binasi: {HizmetBinasiId}",
                                     filteredAndSortedResult.Count, result.Count, hizmetBinasiId);

                return filteredAndSortedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active kiosk gruplari with sorting for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }
    }
}
