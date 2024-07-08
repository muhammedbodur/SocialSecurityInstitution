using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using SocialSecurityInstitution.PresentationLayer.Models.Kanal;
using SocialSecurityInstitution.PresentationLayer.Models.Kiosk;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using Font = System.Drawing.Font;
using System.Drawing.Imaging;


namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class KioskController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly IKioskGruplariService _kioskGruplariService;
        private readonly IKioskIslemGruplariService _kioskIslemGruplariService;
        private readonly IKioskIslemGruplariCustomService _kioskIslemGruplariCustomService;
        private readonly IKanalAltIslemleriService _kanalAltIslemleriService;
        private readonly PrintService _printService;

        public KioskController(IMapper mapper, IToastNotification toast, IKioskGruplariService kioskGruplariService, IKioskIslemGruplariService kioskIslemGruplariService, IKioskIslemGruplariCustomService kioskIslemGruplariCustomService, IKanalAltIslemleriService kanalAltIslemleriService, PrintService printService)
        {
            _mapper = mapper;
            _toast = toast;
            _kioskGruplariService = kioskGruplariService;
            _kioskIslemGruplariService = kioskIslemGruplariService;
            _kioskIslemGruplariCustomService = kioskIslemGruplariCustomService;
            _kanalAltIslemleriService = kanalAltIslemleriService;
            _printService = printService;
        }

        [HttpGet]
        public async Task<IActionResult> AnaSayfa(int hizmetBinasiId)
        {
            List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto> kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(hizmetBinasiId);

            kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto = kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto.OrderBy(x => x.KioskIslemGrupSira).ToList();

            return View(kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto);

        }

        [HttpGet]
        public async Task<IActionResult> KioskAltKanallar(int kioskIslemGrupId)
        {
            List<KanalAltIslemleriRequestDto> kanalAltIslemleriRequestDto = await _kioskIslemGruplariCustomService.GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(kioskIslemGrupId);

            return View(kanalAltIslemleriRequestDto);
        }

        [HttpGet]
        public async Task<IActionResult> Yazdir(int kanalAltIslemId)
        {
            int sayi = 1234;
            string sgmAdi = "BORNOVA NACİ ŞAHİN SOSYAL GÜVENLİK MERKEZİ";

            string htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Print</title>
                <style>
                    .print-area {{
                        width: 275px;
                        height: 200px;
                        border: 6px solid black;
                        padding: 10px;
                    }}
                </style>
            </head>
            <body>
                <div class='print-area'>
                    <img src='/img/logos/sgk_logo_siyah.bmp' width='90' height='50' style='float:left; margin-right:10px;'>
                    <div style='font-size:12px; font-weight:bold; text-align:center;'>{sgmAdi}</div>
                    <hr style='border: 1px solid black; margin-top:10px;'>
                    <div style='font-size:60px; font-weight:bold; text-align:center; margin-top:10px;'>{sayi}</div>
                    <hr style='border: 1px solid black; margin-top:10px;'>
                    <div style='font-size:12px; font-weight:bold; text-align:center; margin-top:10px;'>{DateTime.Now:dd-MM-yyyy HH:mm:ss dddd}</div>
                </div>
                <script>
                    window.print();
                </script>
            </body>
            </html>";

            return Content(htmlContent, "text/html");
        }

        [HttpGet]
        public async Task<JsonResult> KioskYazdir(int kanalAltIslemId)
        {
            int sayi = 1234; 
            string sgmAdi = "BORNOVA NACİ ŞAHİN SOSYAL GÜVENLİK MERKEZİ";

            try
            {
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logos", "sgk_logo_siyah.bmp");
                byte[] imageBytes = _printService.GenerateImage(sgmAdi, sayi, logoPath);
                _printService.Print(imageBytes);

                var kioskSiraGosterViewModel = new KioskSiraGosterViewModel
                {
                    SiraNo = sayi,
                    SgmAdi = sgmAdi,
                    IslemDurum = 1,
                    Mesaj = "Sıra Yazdırma İşlemi Başarılı",
                    Error = null
                };

                return Json(kioskSiraGosterViewModel);
            }
            catch (Exception ex)
            {
                var kioskSiraGosterViewModel = new KioskSiraGosterViewModel
                {
                    SiraNo = sayi,
                    SgmAdi = sgmAdi,
                    Mesaj = "Sıra Yazdırma İşlemi Başarısız!",
                    IslemDurum = 0,
                    Error = ex.ToString()
                };
                return Json(kioskSiraGosterViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> KioskListele()
        {
            List<KioskGruplariDto> kioskGruplariDto = await _kioskGruplariService.TGetAllAsync();
            return View(kioskGruplariDto);
        }

        [HttpGet]
        public async Task<IActionResult> KioskListeleJson()
        {
            List<KioskGruplariDto> kioskGruplariDto = await _kioskGruplariService.TGetAllAsync();
            return Json(kioskGruplariDto);
        }

        [HttpGet]
        public async Task<JsonResult> KioskGrupGetir(int kioskGrupId)
        {
            var kioskGruplariDto = await _kioskGruplariService.TGetByIdAsync(kioskGrupId);
            return Json(kioskGruplariDto);
        }

        [HttpPost]
        public async Task<IActionResult> KioskGrupGuncelle(int kioskGrupId, string kioskGrupAdi)
        {
            var kioskGruplariDto = await _kioskGruplariService.TGetByIdAsync(kioskGrupId);

            if (kioskGruplariDto != null)
            {
                kioskGruplariDto.KioskGrupAdi = kioskGrupAdi;
                kioskGruplariDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kioskGruplariService.TUpdateAsync(kioskGruplariDto);

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
                return NotFound("Kiosk Grup bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> KioskGrupEkle(string kioskGrupAdi)
        {
            var kioskGruplariDto = new KioskGruplariDto
            {
                KioskGrupAdi = kioskGrupAdi,
                DuzenlenmeTarihi = DateTime.Now,
                EklenmeTarihi = DateTime.Now,
            };

            var insertResult = await _kioskGruplariService.TInsertAsync(kioskGruplariDto);

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
        public async Task<JsonResult> KioskGrupSil(int kioskGrupId)
        {
            var kioskGruplariDto = await _kioskGruplariService.TGetByIdAsync(kioskGrupId);

            if (kioskGruplariDto != null)
            {

                var deleteResult = await _kioskGruplariService.TDeleteAsync(kioskGruplariDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Kiosk Grubu Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Kiosk Grubu Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Kiosk Grup Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> KioskIslemleriListele(int hizmetBinasiId)
        {
            List<KioskIslemGruplariRequestDto> kioskIslemGruplariRequestDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariAsync(hizmetBinasiId);
            List<KioskGruplariDto> kioskGruplariDto = await _kioskGruplariService.TGetAllAsync();

            var viewModel = new KioskListViewModel
            {
                KioskIslemGruplari = kioskIslemGruplariRequestDto,
                KioskGruplari = kioskGruplariDto
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KioskIslemGruplariEkle(int hizmetBinasiId, int kioskGrupId)
        {
            //verilen sıralama ilk ID 1 ile başlamalı, ardından gelen ise bir öncekinin bitişinden 1 tane fazla olmalı

            var kioskIslemGruplariDto = new KioskIslemGruplariDto
            {
                KioskGrupId = kioskGrupId,
                HizmetBinasiId = hizmetBinasiId,
                KioskIslemGrupAdi = "",
                EklenmeTarihi = DateTime.Now,
                DuzenlenmeTarihi = DateTime.Now,
                KioskIslemGrupAktiflik = Aktiflik.Aktif
            };

            var insertResult = await _kioskIslemGruplariService.TInsertAsync(kioskIslemGruplariDto);

            if (insertResult.IsSuccess)
            {
                KioskIslemGruplariRequestDto kanalDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariByIdAsync(Convert.ToInt32(insertResult.LastPrimaryKeyValue));

                return Ok(kanalDto);
            }
            else
            {
                return BadRequest("Ekleme işlemi başarısız oldu!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KioskIslemGruplariGetir(int kioskIslemGrupId)
        {
            var kioskIslemGruplariDto = await _kioskIslemGruplariService.TGetByIdAsync(kioskIslemGrupId);
            return Json(kioskIslemGruplariDto);
        }

        [HttpPost]
        public async Task<IActionResult> KioskIslemGruplariGuncelle(int kioskIslemGrupId, int kioskGrupId)
        {
            var kioskIslemGruplariDto = await _kioskIslemGruplariService.TGetByIdAsync(kioskIslemGrupId);

            if (kioskIslemGruplariDto != null)
            {
                kioskIslemGruplariDto.KioskGrupId = kioskGrupId;
                kioskIslemGruplariDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kioskIslemGruplariService.TUpdateAsync(kioskIslemGruplariDto);

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
                return NotFound("Kiosk bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> KioskIslemGruplariSiraVer(int kioskIslemGrupId, int kioskIslemGrupSira)
        {
            var kioskIslemGruplariDto = await _kioskIslemGruplariService.TGetByIdAsync(kioskIslemGrupId);

            if (kioskIslemGruplariDto != null)
            {
                kioskIslemGruplariDto.KioskIslemGrupSira = kioskIslemGrupSira;
                kioskIslemGruplariDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kioskIslemGruplariService.TUpdateAsync(kioskIslemGruplariDto);

                if (updateResult)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Sıra Güncelleme işlemi başarısız oldu!");
                }
            }
            else
            {
                return NotFound("Kiosk bulunamadı.");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KioskIslemGruplariAktifPasifEt(int kioskIslemGrupId)
        {
            var kioskIslemGruplariDto = await _kioskIslemGruplariService.TGetByIdAsync(kioskIslemGrupId);

            if (kioskIslemGruplariDto != null)
            {
                var AktiflikDurum = (kioskIslemGruplariDto.KioskIslemGrupAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                kioskIslemGruplariDto.KioskIslemGrupAktiflik = AktiflikDurum;
                kioskIslemGruplariDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kioskIslemGruplariService.TUpdateAsync(kioskIslemGruplariDto);

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
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Kiosk Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> KioskIslemGruplariSil(int kioskIslemGrupId)
        {
            var kioskIslemGruplariDto = await _kioskIslemGruplariService.TGetByIdAsync(kioskIslemGrupId);

            if (kioskIslemGruplariDto != null)
            {

                var deleteResult = await _kioskIslemGruplariService.TDeleteAsync(kioskIslemGruplariDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Kiosk İşlemleri Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Kiosk İşlemleri Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Kiosk Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> KioskEslestir(int hizmetBinasiId)
        {
            var kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(hizmetBinasiId);
            var kioskIslemGruplariKanalAltIslemleriRequestDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariKanalAltIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);

            var viewModel = new KioskIslemGruplariEslestirViewModel
            {
                KioskIslemGruplariKanalAltIslemleri = kioskIslemGruplariKanalAltIslemleriRequestDto,
                KioskIslemGruplariAltIslemlerEslestirmeSayisi = kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> KioskIslemGruplariListesi(int hizmetBinasiId)
        {
            var kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto = await _kioskIslemGruplariCustomService.GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(hizmetBinasiId);
            return Json(kioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto);
        }

        [HttpGet]
        public async Task<JsonResult> kioskIslemGruplariAltKanalEslestirmeleriGetir(int kioskIslemGrupId)
        {
            var kioskKanalAltIslemleri = await _kioskIslemGruplariCustomService.GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(kioskIslemGrupId);
            return Json(kioskKanalAltIslemleri);
        }

        [HttpGet]
        public async Task<JsonResult> EslestirilmemisKioskAltKanallariGetir(int hizmetBinasiId)
        {
            var eslestirilmemisAltKanallar = await _kioskIslemGruplariCustomService.GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);
            return Json(eslestirilmemisAltKanallar);
        }

        [HttpPost]
        public async Task<JsonResult> KioskAltKanalEslestirmeYap(int kioskIslemGrupId, int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null)
            {
                kanalAltIslemleriDto.KioskIslemGrupId = kioskIslemGrupId;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);

                if (updateResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Kiosk / Alt Kanal Eşleştirmesi İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Kiosk / Alt Kanal Eşleştirmesi İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Alt Kanal Bulunamadı!" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> KioskAltKanalEslestirmeKaldir(int kioskIslemGrupId, int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null && kanalAltIslemleriDto.KioskIslemGrupId == kioskIslemGrupId)
            {
                kanalAltIslemleriDto.KioskIslemGrupId = null;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);

                if (updateResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Kiosk / Alt Kanal Eşleştirmesi Kaldırma İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Kiosk / Alt Kanal Eşleştirmesi Kaldırma İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Alt Kanal Bulunamadı!" });
            }
        }

    }
}
