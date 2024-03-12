using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class LogoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }
    }
}
