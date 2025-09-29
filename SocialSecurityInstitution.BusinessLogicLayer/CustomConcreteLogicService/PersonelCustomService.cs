using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelCustomService : IPersonelCustomService
    {
        private readonly IPersonellerDal _personellerDal;
        private readonly ILogger<PersonelCustomService> _logger;

        public PersonelCustomService(IPersonellerDal personellerDal, ILogger<PersonelCustomService> logger)
        {
            _personellerDal = personellerDal ?? throw new ArgumentNullException(nameof(personellerDal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<PersonellerDto>> GetPersonellerDepartmanIdAndHizmetBinasiIdAsync(int departmanId, int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting personeller by DepartmanId: {DepartmanId} and HizmetBinasiId: {HizmetBinasiId}", departmanId, hizmetBinasiId);

                // Business validation
                if (departmanId <= 0)
                {
                    _logger.LogWarning("GetPersonellerDepartmanIdAndHizmetBinasiId failed: Invalid DepartmanId: {DepartmanId}", departmanId);
                    return new List<PersonellerDto>();
                }

                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetPersonellerDepartmanIdAndHizmetBinasiId failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<PersonellerDto>();
                }

                // Get personeller through repository
                var personellerList = await _personellerDal.GetPersonellerByDepartmanAndHizmetBinasiAsync(departmanId, hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} personeller for DepartmanId: {DepartmanId} and HizmetBinasiId: {HizmetBinasiId}", 
                    personellerList.Count, departmanId, hizmetBinasiId);

                return personellerList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting personeller by DepartmanId: {DepartmanId} and HizmetBinasiId: {HizmetBinasiId}", 
                    departmanId, hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            try
            {
                _logger.LogInformation("Getting personeller with HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);

                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("GetPersonellerWithHizmetBinasiId failed: Invalid HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<PersonellerLiteDto>();
                }

                // Get personeller through repository
                var personellerList = await _personellerDal.GetPersonellerWithHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} personeller with HizmetBinasiId: {HizmetBinasiId}", 
                    personellerList.Count, hizmetBinasiId);

                return personellerList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting personeller with HizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync()
        {
            try
            {
                _logger.LogInformation("Getting personeller with full details");

                var personellerList = await _personellerDal.GetPersonellerWithDetailsAsync();

                _logger.LogInformation("Retrieved {Count} personeller with full details", personellerList.Count);

                return personellerList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting personeller with full details");
                throw;
            }
        }

        public async Task<PersonellerDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Getting personel by TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("TGetByTcKimlikNo failed: TcKimlikNo is null or empty");
                    return null;
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("TGetByTcKimlikNo failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                // Get personel through repository
                var personelDto = await _personellerDal.GetByTcKimlikNoAsync(tcKimlikNo);

                if (personelDto != null)
                {
                    _logger.LogInformation("Retrieved personel for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }
                else
                {
                    _logger.LogWarning("Personel not found for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }

                return personelDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting personel by TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<PersonellerDto> UpdateSessionIDAsync(string tcKimlikNo, string newSessionId)
        {
            try
            {
                _logger.LogInformation("Updating SessionID for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("UpdateSessionID failed: TcKimlikNo is null or empty");
                    return null;
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("UpdateSessionID failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                if (string.IsNullOrWhiteSpace(newSessionId))
                {
                    _logger.LogWarning("UpdateSessionID failed: NewSessionId is null or empty for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                // Update session through repository
                var updatedPersonelDto = await _personellerDal.UpdateSessionIdAsync(tcKimlikNo, newSessionId);

                if (updatedPersonelDto != null)
                {
                    _logger.LogInformation("SessionID updated successfully for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }
                else
                {
                    _logger.LogWarning("Failed to update SessionID - Personel not found for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }

                return updatedPersonelDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating SessionID for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<PersonellerLiteDto>> GetActivePersonelListAsync()
        {
            try
            {
                _logger.LogInformation("Getting active personel list");

                // Get active personeller through repository
                var activePersonellerList = await _personellerDal.GetActivePersonelListAsync();

                _logger.LogInformation("Retrieved {Count} active personeller", activePersonellerList.Count);

                return activePersonellerList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting active personel list");
                throw;
            }
        }

        public async Task<PersonellerViewDto> GetPersonelViewForEditAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Getting personel view for edit by TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("GetPersonelViewForEdit failed: TcKimlikNo is null or empty");
                    return null;
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("GetPersonelViewForEdit failed: Invalid TcKimlikNo format: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                var personelViewDto = await _personellerDal.GetPersonelViewForEditAsync(tcKimlikNo);

                if (personelViewDto != null)
                {
                    _logger.LogInformation("Retrieved personel view for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }
                else
                {
                    _logger.LogWarning("Personel not found for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                }

                return personelViewDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting personel view for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }
    }
}
