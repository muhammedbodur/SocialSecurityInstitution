using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class UnvanController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnvanlarService _unvanlarService;
        private readonly IToastNotification _toast;

        public UnvanController(IMapper mapper, IUnvanlarService unvanlarService, IToastNotification toast)
        {
            _mapper = mapper;
            _unvanlarService = unvanlarService;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            List<UnvanlarDto> unvanlarDto = await _unvanlarService.TGetAllAsync();
            return View(unvanlarDto);
        }

        [HttpGet]
        public async Task<JsonResult> Getir(int unvanId)
        {
            var unvanlarDto = await _unvanlarService.TGetByIdAsync(unvanId);
            return Json(unvanlarDto);
        }

        [HttpGet]
        public async Task<JsonResult> AktifPasifEt(int unvanId)
        {
            var unvanlarDto = await _unvanlarService.TGetByIdAsync(unvanId);

            if (unvanlarDto != null)
            {
                var AktiflikDurum = (unvanlarDto.UnvanAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                unvanlarDto.UnvanAktiflik = AktiflikDurum;
                unvanlarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _unvanlarService.TUpdateAsync(unvanlarDto);

                if (updateResult)
                {
                    return Json(new { islemDurum = 1, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Unvan Bulunamadı!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(int unvanId, string unvanAdi)
        {
            var unvanlarDto = await _unvanlarService.TGetByIdAsync(unvanId);

            if (unvanlarDto != null)
            {
                unvanlarDto.UnvanAdi = unvanAdi;
                unvanlarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _unvanlarService.TUpdateAsync(unvanlarDto);

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
                return NotFound("Unvan bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(string unvanAdi)
        {
            var unvanlarDto = new UnvanlarDto
            {
                UnvanAdi = unvanAdi,
                DuzenlenmeTarihi = DateTime.Now,
                UnvanAktiflik = Aktiflik.Aktif
            };

            var insertResult = await _unvanlarService.TInsertAsync(unvanlarDto);

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
        public async Task<JsonResult> Sil(int unvanId)
        {
            var unvanlarDto = await _unvanlarService.TGetByIdAsync(unvanId);

            if (unvanlarDto != null)
            {

                var deleteResult = await _unvanlarService.TDeleteAsync(unvanlarDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Unvanı Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Unvanı Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Unvan Bulunamadı!" });
            }
        }
    }
}
