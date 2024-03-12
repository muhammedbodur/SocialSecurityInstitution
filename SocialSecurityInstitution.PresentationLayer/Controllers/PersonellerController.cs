using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class PersonellerController : Controller
    {
        private readonly IPersonelRequestService _personelRequestService;

        public PersonellerController(IPersonelRequestService personelRequestService)
        {
            _personelRequestService = personelRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<PersonelRequestDto> personelRequests = await _personelRequestService.GetPersonellerWithDetailsAsync();
            return Ok(personelRequests);
        }
    }
}
