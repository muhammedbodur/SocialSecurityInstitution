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
    public class KanallarCustomService : IKanallarCustomService
    {
        private readonly IKanalAltIslemleriDal _kanalAltIslemleriDal;
        private readonly IKanalIslemleriDal _kanalIslemleriDal;
        private readonly IPersonellerDal _personellerDal;
        private readonly ILogger<KanallarCustomService> _logger;

        public KanallarCustomService(
            IKanalAltIslemleriDal kanalAltIslemleriDal,
            IKanalIslemleriDal kanalIslemleriDal,
            IPersonellerDal personellerDal,
            ILogger<KanallarCustomService> logger)
        {
            _kanalAltIslemleriDal = kanalAltIslemleriDal;
            _kanalIslemleriDal = kanalIslemleriDal;
            _personellerDal = personellerDal;
            _logger = logger;
        }

        public async Task<List<KanalAltIslemleriDto>> GetKanalAltIslemleriAsync()
        {
            try
            {
                // Repository'den tüm kanal alt işlemlerini al
                var result = await _kanalAltIslemleriDal.GetAllKanalAltIslemleriAsync();

                _logger.LogInformation("Retrieved {Count} kanal alt islemleri", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all kanal alt islemleri");
                throw;
            }
        }

        public async Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalIslemleriRequestDto>();
                }

                // Repository'den kanal işlemlerini al
                var result = await _kanalIslemleriDal.GetKanalIslemleriByHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} kanal islemleri for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal islemleri for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<KanalIslemleriRequestDto> GetKanalIslemleriByIdAsync(int kanalIslemId)
        {
            try
            {
                // Business validation
                if (kanalIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalIslemId provided: {KanalIslemId}", kanalIslemId);
                    return null;
                }

                // Repository'den kanal işlemini al
                var result = await _kanalIslemleriDal.GetKanalIslemleriByIdWithDetailsAsync(kanalIslemId);

                if (result == null)
                {
                    _logger.LogWarning("Kanal islemi not found with ID: {KanalIslemId}", kanalIslemId);
                }
                else
                {
                    _logger.LogInformation("Kanal islemi retrieved: {KanalIslemAdi} for ID: {KanalIslemId}",
                                         result.KanalIslemAdi, kanalIslemId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal islemi by ID: {KanalIslemId}", kanalIslemId);
                throw;
            }
        }

        public async Task<List<KanalPersonelleriViewDto>> GetKanalPersonelleriAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalPersonelleriViewDto>();
                }

                var requestResult = await _personellerDal.GetKanalPersonelleriViewByHizmetBinasiIdAsync(hizmetBinasiId);

                var result = requestResult.Select(r => new KanalPersonelleriViewDto
                {
                    TcKimlikNo = r.TcKimlikNo,
                    SicilNo = r.SicilNo,
                    AdSoyad = r.AdSoyad,
                    DepartmanId = r.DepartmanId,
                    DepartmanAdi = r.DepartmanAdi,
                    ServisId = r.ServisId,
                    ServisAdi = r.ServisAdi,
                    UnvanId = r.UnvanId,
                    UnvanAdi = r.UnvanAdi
                }).ToList();

                _logger.LogInformation("Retrieved {Count} kanal personelleri for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal personelleri for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                // Repository'den kanal alt işlemlerini al
                var result = await _kanalAltIslemleriDal.GetKanalAltIslemleriByHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} kanal alt islemleri for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt islemleri for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<KanalAltIslemleriRequestDto> GetKanalAltIslemleriByIdAsync(int kanalAltIslemId)
        {
            try
            {
                // Business validation
                if (kanalAltIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalAltIslemId provided: {KanalAltIslemId}", kanalAltIslemId);
                    return null;
                }

                // Repository'den kanal alt işlemini al
                var result = await _kanalAltIslemleriDal.GetKanalAltIslemleriByIdWithDetailsAsync(kanalAltIslemId);

                if (result == null)
                {
                    _logger.LogWarning("Kanal alt islemi not found with ID: {KanalAltIslemId}", kanalAltIslemId);
                }
                else
                {
                    _logger.LogInformation("Kanal alt islemi retrieved: {KanalAltIslemAdi} for ID: {KanalAltIslemId}",
                                         result.KanalAltIslemAdi, kanalAltIslemId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt islemi by ID: {KanalAltIslemId}", kanalAltIslemId);
                throw;
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByIslemIdAsync(int kanalIslemId)
        {
            try
            {
                // Business validation
                if (kanalIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalIslemId provided: {KanalIslemId}", kanalIslemId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                // Repository'den kanal alt işlemlerini al
                var result = await _kanalAltIslemleriDal.GetKanalAltIslemleriByIslemIdAsync(kanalIslemId);

                _logger.LogInformation("Retrieved {Count} kanal alt islemleri for kanal islem: {KanalIslemId}",
                                     result.Count, kanalIslemId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt islemleri for kanal islem: {KanalIslemId}", kanalIslemId);
                throw;
            }
        }

        public async Task<List<KanalAltIslemleriEslestirmeSayisiRequestDto>> GetKanalAltIslemleriEslestirmeSayisiAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriEslestirmeSayisiRequestDto>();
                }

                // Repository'den eşleştirme sayılarını al
                var result = await _kanalAltIslemleriDal.GetKanalAltIslemleriEslestirmeSayisiAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} kanal alt islemleri eslestirme sayisi for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt islemleri eslestirme sayisi for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                // Repository'den eşleştirilmemiş kanal alt işlemlerini al
                var result = await _kanalAltIslemleriDal.GetKanalAltIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} unmatched kanal alt islemleri for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving unmatched kanal alt islemleri for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }
    }
}