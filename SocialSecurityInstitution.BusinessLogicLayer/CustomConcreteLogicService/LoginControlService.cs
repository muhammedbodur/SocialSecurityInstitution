using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class LoginControlService : ILoginControlService
    {
        private readonly IPersonellerDal _personellerDal;
        private readonly ILoginLogoutLogDal _loginLogoutLogDal;
        private readonly ILogger<LoginControlService> _logger;

        public LoginControlService(IPersonellerDal personellerDal, ILoginLogoutLogDal loginLogoutLogDal, ILogger<LoginControlService> logger)
        {
            _personellerDal = personellerDal ?? throw new ArgumentNullException(nameof(personellerDal));
            _loginLogoutLogDal = loginLogoutLogDal ?? throw new ArgumentNullException(nameof(loginLogoutLogDal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<LoginDto> LoginControlAsync(string TcKimlikNo, string PassWord)
        {
            try
            {
                _logger.LogInformation("Login attempt for TcKimlikNo: {TcKimlikNo}", TcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(TcKimlikNo))
                {
                    _logger.LogWarning("Login failed: TcKimlikNo is null or empty");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(PassWord))
                {
                    _logger.LogWarning("Login failed: Password is null or empty for TcKimlikNo: {TcKimlikNo}", TcKimlikNo);
                    return null;
                }

                if (TcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("Login failed: Invalid TcKimlikNo format for: {TcKimlikNo}", TcKimlikNo);
                    return null;
                }

                // Authenticate user through repository
                var loginDto = await _personellerDal.AuthenticateUserAsync(TcKimlikNo, PassWord);

                if (loginDto != null)
                {
                    _logger.LogInformation("Login successful for TcKimlikNo: {TcKimlikNo}", TcKimlikNo);
                    return loginDto;
                }
                else
                {
                    _logger.LogWarning("Login failed: Invalid credentials for TcKimlikNo: {TcKimlikNo}", TcKimlikNo);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for TcKimlikNo: {TcKimlikNo}", TcKimlikNo);
                throw;
            }
        }

        public async Task<LoginLogoutLogDto> FindBySessionIdAsync(string sessionId)
        {
            try
            {
                _logger.LogInformation("Finding session by SessionId: {SessionId}", sessionId);

                // Business validation
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    _logger.LogWarning("FindBySessionId failed: SessionId is null or empty");
                    return null;
                }

                // Find session through repository
                var loginLogoutLogDto = await _loginLogoutLogDal.FindBySessionIdAsync(sessionId);

                if (loginLogoutLogDto != null)
                {
                    _logger.LogInformation("Session found for SessionId: {SessionId}", sessionId);
                }
                else
                {
                    _logger.LogWarning("Session not found for SessionId: {SessionId}", sessionId);
                }

                return loginLogoutLogDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding session by SessionId: {SessionId}", sessionId);
                throw;
            }
        }

        public async Task LogoutPreviousSessionsAsync(string tcKimlikNo)
        {
            try
            {
                _logger.LogInformation("Logging out previous sessions for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);

                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("LogoutPreviousSessions failed: TcKimlikNo is null or empty");
                    return;
                }

                if (tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("LogoutPreviousSessions failed: Invalid TcKimlikNo format for: {TcKimlikNo}", tcKimlikNo);
                    return;
                }

                // Logout previous sessions through repository
                await _loginLogoutLogDal.LogoutPreviousSessionsAsync(tcKimlikNo);

                _logger.LogInformation("Previous sessions logged out successfully for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging out previous sessions for TcKimlikNo: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }
    }
}
