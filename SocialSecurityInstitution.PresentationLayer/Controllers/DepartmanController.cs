using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmanlarService _departmanlarService;
        private readonly IDepartmanlarCustomService _departmanlarCustomService;
        private readonly IToastNotification _toast;

        public DepartmanController(IMapper mapper, IDepartmanlarService departmanlarService, IDepartmanlarCustomService departmanlarCustomService, IToastNotification toast)
        {
            _mapper = mapper;
            _departmanlarService = departmanlarService;
            _departmanlarCustomService = departmanlarCustomService;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            List<DepartmanlarDto> departmanlarDto = await _departmanlarService.TGetAllAsync();
            return View(departmanlarDto);
        }

        [HttpGet]
        public async Task<IActionResult> ListeleJson()
        {
            List<DepartmanlarDto> departmanlarDto = await _departmanlarService.TGetAllAsync();
            return Json(departmanlarDto);
        }

        [HttpGet]
        public async Task<JsonResult> Getir(int departmanId)
        {
            var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);
            return Json(departmanlarDto);
        }

        [HttpGet]
        public async Task<JsonResult> AktifPasifEt(int departmanId)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message, aktiflikDurum) = await _departmanlarCustomService.ToggleDepartmanAktiflikAsync(departmanId);
            
            return Json(new 
            { 
                islemDurum = success ? 1 : 0, 
                aktiflikDurum = aktiflikDurum, 
                mesaj = message 
            });
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(int departmanId, string departmanAdi)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message) = await _departmanlarCustomService.UpdateDepartmanAsync(departmanId, departmanAdi);
            
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(string departmanAdi)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message, departmanId) = await _departmanlarCustomService.CreateDepartmanAsync(departmanAdi);
            
            if (success)
            {
                return Ok(departmanId);
            }
            else
            {
                return BadRequest(message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> Sil(int departmanId)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message) = await _departmanlarCustomService.DeleteDepartmanAsync(departmanId);
            
            return Json(new 
            { 
                islemDurum = success ? 1 : 0, 
                mesaj = message 
            });
        }
    }
}
