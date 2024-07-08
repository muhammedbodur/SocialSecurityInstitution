using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IKioskGruplariService _kioskGruplariService;
        private readonly IKioskIslemGruplariService _kioskIslemGruplariService;
        private readonly IKioskIslemGruplariCustomService _kioskIslemGruplariCustomService;
        private readonly IKanalAltIslemleriService _kanalAltIslemleriService;
        private readonly IHizmetBinalariCustomService _hizmetBinalariCustomService;

        public ApiController(IMapper mapper, IKioskGruplariService kioskGruplariService, IKioskIslemGruplariService kioskIslemGruplariService, IKioskIslemGruplariCustomService kioskIslemGruplariCustomService, IKanalAltIslemleriService kanalAltIslemleriService, IHizmetBinalariCustomService hizmetBinalariCustomService)
        {
            _mapper = mapper;
            _kioskGruplariService = kioskGruplariService;
            _kioskIslemGruplariService = kioskIslemGruplariService;
            _kioskIslemGruplariCustomService = kioskIslemGruplariCustomService;
            _kanalAltIslemleriService = kanalAltIslemleriService;
            _hizmetBinalariCustomService = hizmetBinalariCustomService;
        }

        [HttpGet]
        public async Task<JsonResult> DepartmanHizmetBinasi([FromQuery] int hizmetBinasiId)
        {
            var hizmetBinalariDepartmanlarDto = await _hizmetBinalariCustomService.GetDepartmanHizmetBinasiAsync(hizmetBinasiId);
            return Json(hizmetBinalariDepartmanlarDto);
        }

        [HttpGet]
        public async Task<JsonResult> KioskGruplari([FromQuery] int hizmetBinasiId)
        {
            List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto> kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(hizmetBinasiId);

            kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto = kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto.OrderBy(x => x.KioskIslemGrupSira).ToList();

            return Json(kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto);
        }

        [HttpGet]
        public async Task<JsonResult> KioskAltKanallar([FromQuery] int kioskIslemGrupId)
        {
            List<KanalAltIslemleriRequestDto> kanalAltIslemleriRequestDto = await _kioskIslemGruplariCustomService.GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(kioskIslemGrupId);

            return Json(kanalAltIslemleriRequestDto);
        }
    }
}
