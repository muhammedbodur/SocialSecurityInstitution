using Microsoft.AspNetCore.Mvc;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class TvController : Controller
    {
        public IActionResult Listele()
        {
            return View();
        }

        public IActionResult SiraAc()
        { 
            return View(); 
        }

        public IActionResult Siralar()
        {
            return View();
        }
    }
}
