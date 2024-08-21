using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoginLogoutLogService _loginLogoutLogService;
        private readonly ILoginControlService _loginControlService;
        private readonly IPersonelCustomService _personelCustomService;
        private readonly IUserContextService _userContextService;
        private readonly IHubConnectionService _hubConnectionService;
        private readonly IHubConnectionCustomService _hubConnectionCustomService;

        public LogoutController(IMapper mapper, ILoginLogoutLogService loginLogoutLogService, ILoginControlService loginControlService, IPersonelCustomService personelCustomService, IUserContextService userContextService, IHubConnectionService hubConnectionService, IHubConnectionCustomService hubConnectionCustomService)
        {
            _mapper = mapper;
            _loginLogoutLogService = loginLogoutLogService;
            _loginControlService = loginControlService;
            _personelCustomService = personelCustomService;
            _userContextService = userContextService;
            _hubConnectionService = hubConnectionService;
            _hubConnectionCustomService = hubConnectionCustomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var TcKimlikNo = _userContextService.TcKimlikNo;

            var connection = await _hubConnectionCustomService.GetHubConnectionWithTcKimlikNoAsync(TcKimlikNo);

            if (connection != null)
            {
                connection.ConnectionStatus = ConnectionStatus.offline;
                await _hubConnectionService.TUpdateAsync(connection);
            }

            await _loginControlService.LogoutPreviousSessionsAsync(TcKimlikNo);

            // Personeller tablosunda SessionID null olarak güncelliyorum
            await _personelCustomService.UpdateSessionIDAsync(TcKimlikNo, null);

            // Oturumu sonlandır
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Login sayfasına yönlendir
            return RedirectToAction("Index", "Login");
        }

    }
}
