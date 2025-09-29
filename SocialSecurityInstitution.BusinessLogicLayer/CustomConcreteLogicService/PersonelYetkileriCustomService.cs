using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelYetkileriCustomService : IPersonelYetkileriCustomService
    {
        private readonly IYetkilerDal _yetkilerDal;
        private readonly ICacheService _cacheService;
        private readonly ILogger<PersonelYetkileriCustomService> _logger;

        public PersonelYetkileriCustomService(IYetkilerDal yetkilerDal, ICacheService cacheService, ILogger<PersonelYetkileriCustomService> logger)
        {
            _yetkilerDal = yetkilerDal ?? throw new ArgumentNullException(nameof(yetkilerDal));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<YetkilerWithPersonelDto>> GetYetkilerByPersonelTcKimlikNoAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Getting yetkiler for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("GetYetkilerByPersonelTcKimlikNo failed: TcKimlikNo is null or empty");
                    return new List<YetkilerWithPersonelDto>();
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("GetYetkilerByPersonelTcKimlikNo failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return new List<YetkilerWithPersonelDto>();
                }

                // Check cache first
                string cacheKey = $"PersonelYetkileri_{tcKimlikNo}";
                if (await _cacheService.ExistsAsync(cacheKey))
                {
                    _logger.LogInformation("Yetkiler found in cache for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                    return await _cacheService.GetAsync<List<YetkilerWithPersonelDto>>(cacheKey);
                }

                // Get yetkiler through repository
                var anaYetkiler = await _yetkilerDal.GetYetkilerByPersonelTcKimlikNoAsync(tcKimlikNo);

                // Cache the result
                await _cacheService.SetAsync(cacheKey, anaYetkiler, TimeSpan.FromHours(9));

                _logger.LogInformation("Retrieved and cached {Count} ana yetkiler for TcKimlikNo: {TcKimlikNo}", 
                    anaYetkiler.Count, tcKimlikNo);

                return anaYetkiler;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting yetkiler for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task ClearPersonelYetkileriCacheAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Clearing personel yetkiler cache for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("ClearPersonelYetkileriCache failed: TcKimlikNo is null or empty");
                    return;
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("ClearPersonelYetkileriCache failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return;
                }

                // Clear cache
                string cacheKey = $"PersonelYetkileri_{tcKimlikNo}";
                await _cacheService.RemoveAsync(cacheKey);

                _logger.LogInformation("Cache cleared successfully for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while clearing cache for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }
    }
}