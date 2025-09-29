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
    public class KanalPersonelleriCustomService : IKanalPersonelleriCustomService
    {
        private readonly IKanalPersonelleriDal _kanalPersonelleriDal;
        private readonly IKanalAltIslemleriDal _kanalAltIslemleriDal;
        private readonly IPersonellerDal _personellerDal;
        private readonly ILogger<KanalPersonelleriCustomService> _logger;

        public KanalPersonelleriCustomService(
            IKanalPersonelleriDal kanalPersonelleriDal,
            IKanalAltIslemleriDal kanalAltIslemleriDal,
            IPersonellerDal personellerDal,
            ILogger<KanalPersonelleriCustomService> logger)
        {
            _kanalPersonelleriDal = kanalPersonelleriDal;
            _kanalAltIslemleriDal = kanalAltIslemleriDal;
            _personellerDal = personellerDal;
            _logger = logger;
        }

        public async Task<List<KanalAltIslemleriDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo, int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid TC Kimlik No provided: {TcKimlikNo}", tcKimlikNo);
                    return new List<KanalAltIslemleriDto>();
                }

                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriDto>();
                }

                // Repository'den karmaşık sorguyu al
                var result = await _kanalPersonelleriDal.GetPersonelAltKanallarEslesmeyenlerAsync(tcKimlikNo, hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} unmatched kanal alt islemleri for personel: {TcKimlikNo}, hizmet binasi: {HizmetBinasiId}",
                                     result.Count, tcKimlikNo, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving unmatched kanal alt islemleri for personel: {TcKimlikNo}, hizmet binasi: {HizmetBinasiId}",
                               tcKimlikNo, hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<PersonelAltKanallariRequestDto>> GetPersonelAltKanallariAsync(string tcKimlikNo)
        {
            try
            {
                // Business validation
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid TC Kimlik No provided: {TcKimlikNo}", tcKimlikNo);
                    return new List<PersonelAltKanallariRequestDto>();
                }

                // Repository'den personel alt kanallarını al
                var result = await _kanalPersonelleriDal.GetPersonelAltKanallariAsync(tcKimlikNo);

                _logger.LogInformation("Retrieved {Count} alt kanallar for personel: {TcKimlikNo}", result.Count, tcKimlikNo);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving alt kanallar for personel: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<PersonellerAltKanallarRequestDto>> GetPersonellerAltKanallarAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<PersonellerAltKanallarRequestDto>();
                }

                // Repository'den personeller alt kanallar istatistiklerini al
                var result = await _kanalPersonelleriDal.GetPersonellerAltKanallarAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} personeller alt kanallar statistics for hizmet binasi: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personeller alt kanallar for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<List<PersonelAltKanallariRequestDto>> GetKanalPersonelleriWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            try
            {
                // Business validation
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId provided: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<PersonelAltKanallariRequestDto>();
                }

                // Repository'den kanal personellerini al
                var result = await _kanalPersonelleriDal.GetKanalPersonelleriWithHizmetBinasiIdAsync(hizmetBinasiId);

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

        // ✅ Eksik metodu implement et
        public async Task<List<KanalPersonelleriViewRequestDto>> GetKanalAltPersonelleriAsync(int kanalAltIslemId)
        {
            try
            {
                // Business validation
                if (kanalAltIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalAltIslemId provided: {KanalAltIslemId}", kanalAltIslemId);
                    return new List<KanalPersonelleriViewRequestDto>();
                }

                // Repository'den kanal alt işlemindeki personelleri al
                var result = await _kanalPersonelleriDal.GetKanalAltPersonelleriAsync(kanalAltIslemId);

                _logger.LogInformation("Retrieved {Count} personeller for kanal alt islem: {KanalAltIslemId}",
                                     result.Count, kanalAltIslemId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personeller for kanal alt islem: {KanalAltIslemId}", kanalAltIslemId);
                throw;
            }
        }

        // ✅ Private business logic method
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