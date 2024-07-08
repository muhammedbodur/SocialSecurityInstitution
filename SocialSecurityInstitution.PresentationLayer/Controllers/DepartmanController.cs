using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmanlarService _departmanlarService;
        private readonly IToastNotification _toast;

        public DepartmanController(IMapper mapper, IDepartmanlarService departmanlarService, IToastNotification toast)
        {
            _mapper = mapper;
            _departmanlarService = departmanlarService;
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
            var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);

            if (departmanlarDto != null)
            {
                var AktiflikDurum = (departmanlarDto.DepartmanAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                departmanlarDto.DepartmanAktiflik = AktiflikDurum;
                departmanlarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _departmanlarService.TUpdateAsync(departmanlarDto);

                if (updateResult)
                {
                    return Json(new { islemDurum = 1, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarılı Oldu "});
                }
                else
                {
                    return Json(new { islemDurum = 0, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Departman Bulunamadı!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(int departmanId, string departmanAdi)
        {
            var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);

            if (departmanlarDto != null)
            {
                departmanlarDto.DepartmanAdi = departmanAdi;
                departmanlarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _departmanlarService.TUpdateAsync(departmanlarDto);

                if (updateResult)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Güncelleme işlemi başarısız oldu!");
                }
            }
            else
            {
                return NotFound("Departman bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(string departmanAdi)
        {
            var departmanlarDto = new DepartmanlarDto
            {
                DepartmanAdi = departmanAdi,
                DuzenlenmeTarihi = DateTime.Now,
                DepartmanAktiflik = Aktiflik.Aktif
            };

            var insertResult = await _departmanlarService.TInsertAsync(departmanlarDto);

            if (insertResult.IsSuccess)
            {
                return Ok(insertResult.LastPrimaryKeyValue);
            }
            else
            {
                return BadRequest("Ekleme işlemi başarısız oldu!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> Sil(int departmanId)
        {
            var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);

            if (departmanlarDto != null)
            {
                
                var deleteResult = await _departmanlarService.TDeleteAsync(departmanlarDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Departmanı Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Departmanı Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Departman Bulunamadı!" });
            }
        }
    }
}
