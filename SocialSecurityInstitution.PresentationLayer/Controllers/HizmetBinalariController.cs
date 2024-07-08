using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class HizmetBinalariController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly IHizmetBinalariService _hizmetBinalariService;
        private readonly IHizmetBinalariCustomService _hizmetBinalariCustomService;

        public HizmetBinalariController(IMapper mapper, IToastNotification toast, IHizmetBinalariService hizmetBinalariService, IHizmetBinalariCustomService hizmetBinalariCustomService)
        {
            _mapper = mapper;
            _toast = toast;
            _hizmetBinalariService = hizmetBinalariService;
            _hizmetBinalariCustomService = hizmetBinalariCustomService;
        }

        [HttpGet]
        public async Task<JsonResult> ListeleJson(int departmanId)
        {
            var hizmetBinalariDto = await _hizmetBinalariCustomService.GetHizmetBinalariByDepartmanIdAsync(departmanId);
            return Json(hizmetBinalariDto);
        }
    }
}
