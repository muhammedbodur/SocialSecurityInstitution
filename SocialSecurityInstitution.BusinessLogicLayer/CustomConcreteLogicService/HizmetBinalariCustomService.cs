using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class HizmetBinalariCustomService : IHizmetBinalariCustomService
    {
        private readonly IHizmetBinalariDal _hizmetBinalariDal;
        private readonly IDepartmanlarDal _departmanlarDal;
        private readonly ILogger<HizmetBinalariCustomService> _logger;

        public HizmetBinalariCustomService(
            IHizmetBinalariDal hizmetBinalariDal,
            IDepartmanlarDal departmanlarDal,
            ILogger<HizmetBinalariCustomService> logger)
        {
            _hizmetBinalariDal = hizmetBinalariDal;
            _departmanlarDal = departmanlarDal;
            _logger = logger;
        }

        public async Task<List<HizmetBinalariDto>> GetHizmetBinalariByDepartmanIdAsync(int departmanId)
        {
            try
            {
                if (departmanId <= 0)
                {
                    _logger.LogWarning("Invalid departmanId provided: {DepartmanId}", departmanId);
                    return new List<HizmetBinalariDto>();
                }

                var departman = await _departmanlarDal.GetByIdAsync(departmanId);
                if (departman == null)
                {
                    _logger.LogWarning("Departman not found with ID: {DepartmanId}", departmanId);
                    return new List<HizmetBinalariDto>();
                }

                var result = await _hizmetBinalariDal.GetHizmetBinalariByDepartmanIdAsync(departmanId);

                _logger.LogInformation("Retrieved {Count} hizmet binaları for departman: {DepartmanId}",
                                     result.Count, departmanId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hizmet binaları for departman: {DepartmanId}", departmanId);
                throw;
            }
        }

        public async Task<HizmetBinalariDepartmanlarDto> GetActiveHizmetBinasiAsync(int hizmetBinasiId, int departmanId)
        {
            try
            {
                if (hizmetBinasiId <= 0 || departmanId <= 0)
                {
                    _logger.LogWarning("Invalid parameters - HizmetBinasiId: {HizmetBinasiId}, DepartmanId: {DepartmanId}",
                                     hizmetBinasiId, departmanId);
                    return null;
                }

                var result = await _hizmetBinalariDal.GetActiveHizmetBinasiWithDepartmanAsync(hizmetBinasiId, departmanId);

                if (result == null)
                {
                    _logger.LogInformation("No active hizmet binası found - HizmetBinasiId: {HizmetBinasiId}, DepartmanId: {DepartmanId}",
                                         hizmetBinasiId, departmanId);
                }
                else
                {
                    _logger.LogInformation("Active hizmet binası retrieved: {HizmetBinasiAdi} - {DepartmanAdi}",
                                         result.HizmetBinasiAdi, result.DepartmanAdi);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active hizmet binası - HizmetBinasiId: {HizmetBinasiId}, DepartmanId: {DepartmanId}",
                               hizmetBinasiId, departmanId);
                throw;
            }
        }

        public async Task<HizmetBinalariDepartmanlarDto> GetDepartmanHizmetBinasiAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return null;
                }

                var result = await _hizmetBinalariDal.GetHizmetBinasiWithDepartmanByIdAsync(hizmetBinasiId);

                if (result == null)
                {
                    _logger.LogWarning("Hizmet binası not found with ID: {HizmetBinasiId}", hizmetBinasiId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hizmet binası: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<HizmetBinalariDto>> GetAllActiveHizmetBinalariAsync()
        {
            try
            {
                var result = await _hizmetBinalariDal.GetAllActiveHizmetBinalariAsync();

                _logger.LogInformation("Retrieved {Count} active hizmet binaları", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active hizmet binaları");
                throw;
            }
        }

        public async Task<List<HizmetBinalariDepartmanlarDto>> GetHizmetBinalariWithDepartmanDetailsAsync()
        {
            try
            {
                var result = await _hizmetBinalariDal.GetHizmetBinalariWithDepartmanDetailsAsync();

                var filteredResult = result.Where(hb => IsValidHizmetBinasi(hb)).ToList();

                _logger.LogInformation("Retrieved {Count} hizmet binaları with departman details (filtered: {FilteredCount})",
                                     result.Count, filteredResult.Count);

                return filteredResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hizmet binaları with departman details");
                throw;
            }
        }

        public async Task<bool> ValidateHizmetBinasiAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    return false;
                }

                var isActive = await _hizmetBinalariDal.IsHizmetBinasiActiveAsync(hizmetBinasiId);

                _logger.LogInformation("Hizmet binası validation - ID: {HizmetBinasiId}, IsActive: {IsActive}",
                                     hizmetBinasiId, isActive);

                return isActive;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating hizmet binası: {HizmetBinasiId}", hizmetBinasiId);
                return false;
            }
        }

        private bool IsValidHizmetBinasi(HizmetBinalariDepartmanlarDto hizmetBinasi)
        {
            return hizmetBinasi.HizmetBinasiAktiflik == Enums.Aktiflik.Aktif &&
                   hizmetBinasi.DepartmanAktiflik == Enums.Aktiflik.Aktif &&
                   !string.IsNullOrWhiteSpace(hizmetBinasi.HizmetBinasiAdi) &&
                   !string.IsNullOrWhiteSpace(hizmetBinasi.DepartmanAdi);
        }
    }
}