using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoginLogoutLogService _loginLogoutLogService;

        public LogoutController(IMapper mapper, ILoginLogoutLogService loginLogoutLogService)
        {
            _mapper = mapper;
            _loginLogoutLogService = loginLogoutLogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var TcKimlikNo = HttpContext.Session.GetString("TcKimlikNo");

            // Bilgileri kullanarak logout loglama işlemi yap
            //if (!string.IsNullOrEmpty(TcKimlikNo))
            //{
            //    var logoutLogDto = new LoginLogoutLogDto
            //    {
            //        TcKimlikNo = TcKimlikNo,
            //        LogoutTime = DateTime.Now
            //    };
                
            //    await _loginLogoutLogService.TInsertAsync(logoutLogDto);
            //}

            // Oturumu sonlandır
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Login sayfasına yönlendir
            return RedirectToAction("Index", "Login");
        }

    }
}
