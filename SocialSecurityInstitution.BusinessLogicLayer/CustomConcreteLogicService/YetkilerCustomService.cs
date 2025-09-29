using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class YetkilerCustomService : IYetkilerCustomService
    {
        private readonly IYetkilerDal _yetkilerDal;
        private readonly ILogger<YetkilerCustomService> _logger;

        public YetkilerCustomService(IYetkilerDal yetkilerDal, ILogger<YetkilerCustomService> logger)
        {
            _yetkilerDal = yetkilerDal ?? throw new ArgumentNullException(nameof(yetkilerDal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<YetkilerDto>> GetOrtaYetkilerByAnaYetkiIdAsync(int anaYetkiId)
        {
            _logger.LogInformation("GetOrtaYetkilerByAnaYetkiIdAsync called with anaYetkiId: {AnaYetkiId}", anaYetkiId);

            try
            {
                // Validation
                if (anaYetkiId <= 0)
                {
                    _logger.LogWarning("Invalid anaYetkiId provided: {AnaYetkiId}", anaYetkiId);
                    throw new ArgumentException("Ana yetki ID geçerli bir değer olmalıdır.", nameof(anaYetkiId));
                }

                var result = await _yetkilerDal.GetOrtaYetkilerByAnaYetkiIdAsync(anaYetkiId);
                
                _logger.LogInformation("GetOrtaYetkilerByAnaYetkiIdAsync completed successfully. Found {Count} orta yetkiler for anaYetkiId: {AnaYetkiId}", 
                    result?.Count ?? 0, anaYetkiId);
                
                return result ?? new List<YetkilerDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetOrtaYetkilerByAnaYetkiIdAsync for anaYetkiId: {AnaYetkiId}", anaYetkiId);
                throw;
            }
        }

        public async Task<List<YetkilerDto>> GetAltYetkilerByOrtaYetkiIdAsync(int ortaYetkiId)
        {
            _logger.LogInformation("GetAltYetkilerByOrtaYetkiIdAsync called with ortaYetkiId: {OrtaYetkiId}", ortaYetkiId);

            try
            {
                // Validation
                if (ortaYetkiId <= 0)
                {
                    _logger.LogWarning("Invalid ortaYetkiId provided: {OrtaYetkiId}", ortaYetkiId);
                    throw new ArgumentException("Orta yetki ID geçerli bir değer olmalıdır.", nameof(ortaYetkiId));
                }

                var result = await _yetkilerDal.GetAltYetkilerByOrtaYetkiIdAsync(ortaYetkiId);
                
                _logger.LogInformation("GetAltYetkilerByOrtaYetkiIdAsync completed successfully. Found {Count} alt yetkiler for ortaYetkiId: {OrtaYetkiId}", 
                    result?.Count ?? 0, ortaYetkiId);
                
                return result ?? new List<YetkilerDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAltYetkilerByOrtaYetkiIdAsync for ortaYetkiId: {OrtaYetkiId}", ortaYetkiId);
                throw;
            }
        }

        public async Task<List<YetkilerDto>> GetAllYetkilerWithIncludesAsync()
        {
            _logger.LogInformation("GetAllYetkilerWithIncludesAsync called");

            try
            {
                var result = await _yetkilerDal.GetAllYetkilerWithIncludesAsync();
                
                _logger.LogInformation("GetAllYetkilerWithIncludesAsync completed successfully. Found {Count} ana yetkiler with hierarchical structure", 
                    result?.Count ?? 0);
                
                return result ?? new List<YetkilerDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAllYetkilerWithIncludesAsync");
                throw;
            }
        }

        public async Task<List<PersonelYetkileriDto>> GetPersonelYetkileriAsync(string tcKimlikNo)
        {
            _logger.LogInformation("GetPersonelYetkileriAsync called with tcKimlikNo: {TcKimlikNo}", tcKimlikNo);

            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid tcKimlikNo provided: {TcKimlikNo}", tcKimlikNo);
                    throw new ArgumentException("TC Kimlik No boş olamaz.", nameof(tcKimlikNo));
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("Invalid tcKimlikNo length provided: {TcKimlikNo}", tcKimlikNo);
                    throw new ArgumentException("TC Kimlik No 11 haneli olmalıdır.", nameof(tcKimlikNo));
                }

                var result = await _yetkilerDal.GetPersonelYetkileriAsync(tcKimlikNo);
                
                _logger.LogInformation("GetPersonelYetkileriAsync completed successfully. Found {Count} personel yetkiler for tcKimlikNo: {TcKimlikNo}", 
                    result?.Count ?? 0, tcKimlikNo);
                
                return result ?? new List<PersonelYetkileriDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetPersonelYetkileriAsync for tcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }
    }
}
