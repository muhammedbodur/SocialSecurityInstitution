using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class AtanmaNedenleriController : Controller
    {
        private readonly IAtanmaNedenleriService _atanmaNedenleriService;

        public AtanmaNedenleriController(IAtanmaNedenleriService atanmaNedenleriService)
        {
            _atanmaNedenleriService = atanmaNedenleriService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AtanmaNedenleriDto> atanmaNedenleri = await _atanmaNedenleriService.TGetAllAsync();
            return Ok(atanmaNedenleri);
        }
    }
}
