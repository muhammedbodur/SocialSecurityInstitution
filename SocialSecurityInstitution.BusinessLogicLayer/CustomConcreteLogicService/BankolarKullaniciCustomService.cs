using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class BankolarKullaniciCustomService : IBankolarKullaniciCustomService
    {
        private readonly IBankolarKullaniciDal _bankolarKullaniciDal;
        private readonly ILogger<BankolarKullaniciCustomService> _logger;

        public BankolarKullaniciCustomService(
            IBankolarKullaniciDal bankolarKullaniciDal,
            ILogger<BankolarKullaniciCustomService> logger)
        {
            _bankolarKullaniciDal = bankolarKullaniciDal;
            _logger = logger;
        }

        public async Task<BankolarKullaniciDto> GetBankolarKullaniciByBankoIdAsync(int bankoId)
        {
            try
            {
                if (bankoId <= 0)
                {
                    _logger.LogWarning("Invalid bankoId provided: {BankoId}", bankoId);
                    return null;
                }

                var result = await _bankolarKullaniciDal.GetBankolarKullaniciByBankoIdAsync(bankoId);

                if (result == null)
                {
                    _logger.LogInformation("No banko kullanici found for bankoId: {BankoId}", bankoId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving banko kullanici for bankoId: {BankoId}", bankoId);
                throw;
            }
        }

        public async Task<BankolarKullaniciDto> GetBankolarKullaniciByTcKimlikNoAsync(string tcKimlikNo)
        {
            try
            {
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid TC Kimlik No format: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                var result = await _bankolarKullaniciDal.GetBankolarKullaniciWithDetailsByTcKimlikNoAsync(tcKimlikNo);

                if (result == null)
                {
                    _logger.LogInformation("No banko kullanici found for TC: {TcKimlikNo}", tcKimlikNo);
                }
                else
                {
                    _logger.LogInformation("Banko kullanici retrieved for TC: {TcKimlikNo}, BankoId: {BankoId}", 
                                         tcKimlikNo, result.BankoId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving banko kullanici for TC: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<BankolarKullaniciDto>> GetActiveBankolarKullaniciByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<BankolarKullaniciDto>();
                }

                var result = await _bankolarKullaniciDal.GetActiveBankolarKullaniciByHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} active banko kullanicilari for hizmet binasi: {HizmetBinasiId}", 
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active banko kullanicilari for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<bool> IsPersonelAssignedToBankoAsync(string tcKimlikNo)
        {
            try
            {
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid TC Kimlik No for assignment check: {TcKimlikNo}", tcKimlikNo);
                    return false;
                }

                var isAssigned = await _bankolarKullaniciDal.IsTcKimlikNoAssignedToBankoAsync(tcKimlikNo);

                _logger.LogInformation("Personel assignment check for TC: {TcKimlikNo}, IsAssigned: {IsAssigned}", 
                                     tcKimlikNo, isAssigned);

                return isAssigned;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking personel assignment for TC: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<BankolarKullaniciDto>> GetBankolarByTcKimlikNoAsync(string tcKimlikNo)
        {
            try
            {
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid TC Kimlik No for bankolar retrieval: {TcKimlikNo}", tcKimlikNo);
                    return new List<BankolarKullaniciDto>();
                }

                var result = await _bankolarKullaniciDal.GetBankolarByTcKimlikNoAsync(tcKimlikNo);

                _logger.LogInformation("Retrieved {Count} bankolar for personel TC: {TcKimlikNo}", 
                                     result.Count, tcKimlikNo);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bankolar for personel TC: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

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