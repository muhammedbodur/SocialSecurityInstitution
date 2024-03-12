using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Security.Claims;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginControlService _loginControlService;

        public LoginController(ILoginControlService loginControlService)
        {
            _loginControlService = loginControlService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
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
            var loginData = await _loginControlService.LoginControlAsync(TcKimlikNo, PassWord);
            
            if (loginData != null)
            {
                var claims = new[]
                {
                    new Claim("TcKimlikNo", loginData.TcKimlikNo),
                    new Claim(ClaimTypes.Name, loginData.AdSoyad),
                    new Claim(ClaimTypes.Email, loginData.Email),
                    // Diğer talepler burada eklenebilir, örneğin yetkiler
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
