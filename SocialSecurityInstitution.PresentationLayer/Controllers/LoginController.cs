using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using NToastNotify;
using Microsoft.AspNetCore.SignalR;
using SocialSecurityInstitution.BusinessLogicLayer.Hubs;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoginControlService _loginControlService;
        private readonly ILoginLogoutLogService _loginLogoutLogService;
        private readonly IPersonelCustomService _personelCustomService;
        private readonly IToastNotification _toastNotification;
        private readonly IHubConnectionCustomService _hubConnectionCustomService;
        private readonly IHubContext<DashboardHub> _hubContext;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginController(
            IMapper mapper,
            ILoginControlService loginControlService,
            ILoginLogoutLogService loginLogoutLogService,
            IPersonelCustomService personelCustomService,
            IToastNotification toastNotification,
            IHubConnectionCustomService hubConnectionCustomService,
            IHubContext<DashboardHub> hubContext,
            IJwtTokenService jwtTokenService)
        {
            _mapper = mapper;
            _loginControlService = loginControlService;
            _loginLogoutLogService = loginLogoutLogService;
            _personelCustomService = personelCustomService;
            _toastNotification = toastNotification;
            _hubConnectionCustomService = hubConnectionCustomService;
            _hubContext = hubContext;
            _jwtTokenService = jwtTokenService;
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

            if (HttpContext.Session.GetString("LoginErrorMessage") != null)
            {
                string errorMessage = HttpContext.Session.GetString("LoginErrorMessage");
                ViewBag.ErrorMessage = errorMessage;
                HttpContext.Session.Remove("LoginErrorMessage");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string TcKimlikNo, string PassWord, string returnUrl)
        {
            var loginDto = await _loginControlService.LoginControlAsync(TcKimlikNo, PassWord);

            if (loginDto != null)
            {
                await _loginControlService.LogoutPreviousSessionsAsync(TcKimlikNo);

                var sessionId = Guid.NewGuid().ToString();

                await _personelCustomService.UpdateSessionIDAsync(TcKimlikNo, sessionId);

                var loginLogDto = new LoginLogoutLogDto
                {
                    TcKimlikNo = loginDto.TcKimlikNo,
                    LoginTime = DateTime.Now,
                    LogoutTime = null,
                    SessionID = sessionId
                };

                await _loginLogoutLogService.TInsertAsync(loginLogDto);

                var claims = new[]
                {
                    new Claim("TcKimlikNo", loginDto.TcKimlikNo),
                    new Claim("AdSoyad", loginDto.AdSoyad),
                    new Claim("Resim", loginDto.Resim),
                    new Claim("HizmetBinasiId", loginDto.HizmetBinasiId.ToString()),
                    new Claim(ClaimTypes.Name, loginDto.AdSoyad),
                    new Claim(ClaimTypes.Email, loginDto.Email),
                    new Claim("SessionID", sessionId)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                var jwtToken = _jwtTokenService.GenerateToken(loginDto.TcKimlikNo, loginDto.AdSoyad, loginDto.Email, sessionId, loginDto.HizmetBinasiId);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false, // Eğer JavaScript üzerinden erişim gerekiyorsa false olarak ayarlayoruz
                    SameSite = SameSiteMode.Lax, // Çerezin gönderileceği durumlar için Lax veya None seçilebilir
                    Secure = false,
                    Expires = DateTime.UtcNow.AddMinutes(120)
                };

                Response.Cookies.Append("JwtToken", jwtToken, cookieOptions);

                // Diğer kullanıcı bilgilerini çerez olarak ekliyoruz
                Response.Cookies.Append("AdSoyad", loginDto.AdSoyad, cookieOptions);
                Response.Cookies.Append("Email", loginDto.Email, cookieOptions);
                Response.Cookies.Append("TcKimlikNo", loginDto.TcKimlikNo, cookieOptions);
                Response.Cookies.Append("HizmetBinasiId", loginDto.HizmetBinasiId.ToString(), cookieOptions);
                Response.Cookies.Append("SessionID", sessionId, cookieOptions);


                var existingConnection = await _hubConnectionCustomService.GetHubConnectionWithTcKimlikNoAsync(TcKimlikNo);

                if (existingConnection != null)
                {
                    await _hubContext.Clients.AllExcept(existingConnection.ConnectionId).SendAsync("ReceiveNotification", loginDto.AdSoyad + " Giriş Yaptı!", "Giriş Mesajı");
                }

                return LocalRedirect(returnUrl ?? "/");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı giriş! Lütfen TC kimlik numaranızı ve/veya şifrenizi kontrol ediniz!";
                return View();
            }
        }
    }
}
