using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelCocuklariCustomService : IPersonelCocuklariCustomService
    {
        private readonly IPersonelCocuklariDal _personelCocuklariDal;
        private readonly ILogger<PersonelCocuklariCustomService> _logger;

        public PersonelCocuklariCustomService(
            IPersonelCocuklariDal personelCocuklariDal,
            ILogger<PersonelCocuklariCustomService> logger)
        {
            _personelCocuklariDal = personelCocuklariDal ?? throw new ArgumentNullException(nameof(personelCocuklariDal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PersonelCocuklariDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            _logger.LogInformation("TGetByTcKimlikNoAsync called with tcKimlikNo: {TcKimlikNo}", tcKimlikNo);

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

                var result = await _personelCocuklariDal.TGetByTcKimlikNoAsync(tcKimlikNo);
                
                _logger.LogInformation("TGetByTcKimlikNoAsync completed successfully for tcKimlikNo: {TcKimlikNo}. Found: {Found}", 
                    tcKimlikNo, result != null);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in TGetByTcKimlikNoAsync for tcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }
    }
}
