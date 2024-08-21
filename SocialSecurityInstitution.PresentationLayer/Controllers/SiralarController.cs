using Microsoft.AspNetCore.Mvc;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class SiralarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
