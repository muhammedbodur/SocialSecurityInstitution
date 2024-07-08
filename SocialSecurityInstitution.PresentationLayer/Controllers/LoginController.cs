using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoginControlService _loginControlService;
        private readonly ILoginLogoutLogService _loginLogoutLogService;

        public LoginController(IMapper mapper, ILoginControlService loginControlService, ILoginLogoutLogService loginLogoutLogService)
        {
            _mapper = mapper;
            _loginControlService = loginControlService;
            _loginLogoutLogService = loginLogoutLogService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity != null && User.Identity != null && User.Identity.IsAuthenticated && claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string TcKimlikNo, string PassWord, string returnUrl)
        {
            var loginDto = await _loginControlService.LoginControlAsync(TcKimlikNo, PassWord);
            
            if (loginDto != null)
            {
                LoginLogoutLogDto loginLogDto = new LoginLogoutLogDto();

                loginLogDto.TcKimlikNo = loginDto.TcKimlikNo;
                loginLogDto.LoginTime = DateTime.Now;
                loginLogDto.LogoutTime = DateTime.MinValue;

                await _loginLogoutLogService.TInsertAsync(loginLogDto);

                var claims = new[]
                {
                    new Claim("TcKimlikNo", loginDto.TcKimlikNo),
                    new Claim("AdSoyad", loginDto.AdSoyad),
                    new Claim("Resim", loginDto.Resim),
                    new Claim(ClaimTypes.Name, loginDto.AdSoyad),
                    new Claim(ClaimTypes.Email, loginDto.Email),
                    // Diğer talepler burada eklenebilir, örneğin yetkiler
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("AdSoyad", loginDto.AdSoyad);
                HttpContext.Session.SetString("Email", loginDto.Email);
                HttpContext.Session.SetString("TcKimlikNo", loginDto.TcKimlikNo);

                return LocalRedirect(returnUrl ?? "/");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı giriş! Lütfen TC kimlik numaranızı ve şifrenizi kontrol edin.";
                return View();
            }
        }

    }
}
