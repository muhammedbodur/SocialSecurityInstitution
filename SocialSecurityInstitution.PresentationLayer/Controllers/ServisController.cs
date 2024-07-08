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
    public class ServisController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServislerService _servislerService;
        private readonly IToastNotification _toast;

        public ServisController(IMapper mapper, IServislerService servislerService, IToastNotification toast)
        {
            _mapper = mapper;
            _servislerService = servislerService;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            List<ServislerDto> servislerDto = await _servislerService.TGetAllAsync();
            return View(servislerDto);
        }

        [HttpGet]
        public async Task<JsonResult> Getir(int servisId)
        {
            var servislerDto = await _servislerService.TGetByIdAsync(servisId);
            return Json(servislerDto);
        }

        [HttpGet]
        public async Task<JsonResult> AktifPasifEt(int servisId)
        {
            var servislerDto = await _servislerService.TGetByIdAsync(servisId);

            if (servislerDto != null)
            {
                var AktiflikDurum = (servislerDto.ServisAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                servislerDto.ServisAktiflik = AktiflikDurum;
                servislerDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _servislerService.TUpdateAsync(servislerDto);

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
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Servis Bulunamadı!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(int servisId, string servisAdi)
        {
            var servislerDto = await _servislerService.TGetByIdAsync(servisId);

            if (servislerDto != null)
            {
                servislerDto.ServisAdi = servisAdi;
                servislerDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _servislerService.TUpdateAsync(servislerDto);

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
                return NotFound("Servis bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(string servisAdi)
        {
            var servislerDto = new ServislerDto
            {
                ServisAdi = servisAdi,
                DuzenlenmeTarihi = DateTime.Now,
                ServisAktiflik = Aktiflik.Aktif
            };

            var insertResult = await _servislerService.TInsertAsync(servislerDto);

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
        public async Task<JsonResult> Sil(int servisId)
        {
            var servislerDto = await _servislerService.TGetByIdAsync(servisId);

            if (servislerDto != null)
            {

                var deleteResult = await _servislerService.TDeleteAsync(servislerDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Servisi Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Servisi Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Servis Bulunamadı!" });
            }
        }
    }
}
