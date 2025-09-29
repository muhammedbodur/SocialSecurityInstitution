using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.PresentationLayer.Models.Kanal;
using SocialSecurityInstitution.PresentationLayer.Services.AbstractPresentationServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class KanalController : Controller
    {
        private readonly IKanalFacadeService _kanalFacadeService;
        private readonly IUserInfoService _userInfoService;
        private readonly IToastNotification _toastNotification;

        public KanalController(
            IKanalFacadeService kanalFacadeService,
            IUserInfoService userInfoService,
            IToastNotification toastNotification)
        {
            _kanalFacadeService = kanalFacadeService;
            _userInfoService = userInfoService;
            _toastNotification = toastNotification;
        }

        #region Kanal Management
        [HttpGet]
        public async Task<IActionResult> KanallarListele()
        {
            // TODO: Facade'e eklenecek
            return View(new List<KanallarDto>());
        }

        [HttpGet]
        public async Task<JsonResult> KanallarGetir(int kanalId)
        {
            // TODO: Facade'e eklenecek
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> KanallarGuncelle(int kanalId, string kanalAdi)
        {
            var result = await _kanalFacadeService.UpdateKanalAsync(kanalId, kanalAdi);
            return result.Success ? Ok() : BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> KanallarEkle(string kanalAdi)
        {
            var result = await _kanalFacadeService.CreateKanalAsync(kanalAdi);
            return result.Success ? Ok(new { KanalId = result.KanalId, KanalAdi = kanalAdi }) : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<JsonResult> KanallarSil(int kanalId)
        {
            var result = await _kanalFacadeService.DeleteKanalAsync(kanalId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }
        #endregion

        #region Kanal Alt Management
        [HttpGet]
        public async Task<IActionResult> KanallarAltListele()
        {
            // TODO: Facade'e eklenecek
            return View(new List<KanallarAltDto>());
        }

        [HttpGet]
        public async Task<JsonResult> KanallarAltGetir(int kanalAltId)
        {
            // TODO: Facade'e eklenecek
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> KanallarAltGuncelle(int kanalAltId, string kanalAltAdi, int kanalId)
        {
            var result = await _kanalFacadeService.UpdateKanalAltAsync(kanalAltId, kanalAltAdi, kanalId);
            return result.Success ? Ok() : BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> KanallarAltEkle(string kanalAltAdi, int kanalId)
        {
            var result = await _kanalFacadeService.CreateKanalAltAsync(kanalAltAdi, kanalId);
            return result.Success ? Ok(new { KanalAltId = result.KanalAltId, KanalAltAdi = kanalAltAdi, KanalId = kanalId }) : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<JsonResult> KanallarAltSil(int kanalAltId)
        {
            var result = await _kanalFacadeService.DeleteKanalAltAsync(kanalAltId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }
        #endregion

        #region Kanal İşlemleri Management
        [HttpGet]
        public async Task<IActionResult> KanalIslemleriListele(int hizmetBinasiId)
        {
            var kanalIslemleri = await _kanalFacadeService.GetKanalIslemleriAsync(hizmetBinasiId);
            // TODO: Facade'den kanallar da gelecek
            var viewModel = new KanalListViewModel
            {
                KanalIslemleri = kanalIslemleri,
                Kanallar = new List<KanallarDto>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KanalIslemleriEkle(int hizmetBinasiId, int kanalId, int kanalSayiAralikBaslangic, int kanalSayiAralikBitis)
        {
            var result = await _kanalFacadeService.CreateKanalIslemWithRangeAsync(hizmetBinasiId, kanalId, kanalSayiAralikBaslangic, kanalSayiAralikBitis);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<JsonResult> KanalIslemleriGetir(int kanalIslemId)
        {
            // TODO: Facade'e eklenecek
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> KanalIslemleriGuncelle(int kanalIslemId, int kanalId, int kanalSayiAralikBaslangic, int kanalSayiAralikBitis)
        {
            // TODO: Facade'e eklenecek
            return BadRequest("Not implemented yet");
        }

        [HttpGet]
        public async Task<JsonResult> KanalIslemleriAktifPasifEt(int kanalIslemId)
        {
            var result = await _kanalFacadeService.ToggleKanalIslemAktiflikAsync(kanalIslemId);
            return Json(new
            {
                islemDurum = result.Success ? 1 : 0,
                aktiflikDurum = result.AktiflikDurum,
                mesaj = result.Message
            });
        }

        [HttpGet]
        public async Task<JsonResult> KanalIslemleriSil(int kanalIslemId)
        {
            var result = await _kanalFacadeService.DeleteKanalIslemAsync(kanalIslemId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }
        #endregion

        #region Kanal Alt İşlemleri Management
        [HttpGet]
        public async Task<IActionResult> KanalAltIslemleriListele(int hizmetBinasiId)
        {
            // TODO: Facade'e taşınacak
            var viewModel = new KanalAltListViewModel
            {
                KanalAltIslemleri = new List<KanalAltIslemleriRequestDto>(),
                KanallarAlt = new List<KanallarAltDto>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KanalAltIslemleriEkle(int hizmetBinasiId, int kanalAltId)
        {
            // TODO: Facade'e CreateKanalAltIslemAsync eklenecek
            return BadRequest("Not implemented yet");
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltIslemleriGetir(int kanalAltIslemId)
        {
            // TODO: Facade'e eklenecek
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> KanalAltIslemleriGuncelle(int kanalAltIslemId, int kanalAltId)
        {
            var result = await _kanalFacadeService.UpdateKanalAltIslemAsync(kanalAltIslemId, kanalAltId.ToString());
            return result.Success ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltIslemleriAktifPasifEt(int kanalAltIslemId)
        {
            var result = await _kanalFacadeService.ToggleKanalAltIslemAktiflikAsync(kanalAltIslemId);
            return Json(new
            {
                islemDurum = result.Success ? 1 : 0,
                aktiflikDurum = result.AktiflikDurum,
                mesaj = result.Message
            });
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltIslemleriSil(int kanalAltIslemId)
        {
            var result = await _kanalFacadeService.DeleteKanalAltIslemAsync(kanalAltIslemId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }
        #endregion

        #region Personel Eşleştirme
        [HttpGet]
        public async Task<IActionResult> KanalPersonelleri(int hizmetBinasiId)
        {
            var tcKimlikNo = _userInfoService.GetTcKimlikNo();
            var kanalIslemleri = await _kanalFacadeService.GetKanalIslemleriAsync(hizmetBinasiId);
            ViewBag.KanalIslemleri = kanalIslemleri;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PersonelAltKanalEslestir()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltPersonelleriGetir(int kanalAltIslemId)
        {
            var personeller = await _kanalFacadeService.GetKanalAltPersonelleriAsync(kanalAltIslemId);
            return Json(personeller);
        }

        [HttpGet]
        public async Task<JsonResult> PersonelAltKanallariGetir(string tcKimlikNo)
        {
            var kanallar = await _kanalFacadeService.GetPersonelAltKanallariAsync(tcKimlikNo);
            return Json(kanallar);
        }

        [HttpGet]
        public async Task<JsonResult> PersonelAltKanallarEslesmeyenleriGetir(string tcKimlikNo)
        {
            var kanallar = await _kanalFacadeService.GetPersonelAltKanallarEslesmeyenleriAsync(tcKimlikNo);
            return Json(kanallar);
        }

        [HttpPost]
        public async Task<JsonResult> PersonelAltKanalEslestirmeYap(string tcKimlikNo, int kanalAltIslemId, int uzmanlikSeviye)
        {
            var result = await _kanalFacadeService.PersonelAltKanalEslestirmeYapAsync(tcKimlikNo, kanalAltIslemId, uzmanlikSeviye);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }

        [HttpPost]
        public async Task<JsonResult> PersonelAltKanalEslestirmeKaldir(string tcKimlikNo, int kanalAltIslemId)
        {
            var result = await _kanalFacadeService.PersonelAltKanalEslestirmeKaldirAsync(tcKimlikNo, kanalAltIslemId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }
        #endregion

        #region Kanal Eşleştirme
        [HttpGet]
        public async Task<IActionResult> KanalEslestir(int hizmetBinasiId)
        {
            var eslestirilmemisKanallar = await _kanalFacadeService.GetEslestirilmemisKanalAltKanallariAsync(hizmetBinasiId);
            var kanallarListesi = await _kanalFacadeService.GetKanallarListesiAsync(hizmetBinasiId);

            var viewModel = new KanalIslemleriEslestirViewModel
            {
                KanalAltIslemleri = eslestirilmemisKanallar,
                KanalAltIslemleriEslestirmeSayisi = kanallarListesi
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> KanallarListesi(int hizmetBinasiId)
        {
            var kanallar = await _kanalFacadeService.GetKanallarListesiAsync(hizmetBinasiId);
            return Json(kanallar);
        }

        [HttpGet]
        public async Task<JsonResult> kanalAltKanalEslestirmeleriGetir(int kanalIslemId)
        {
            var eslestirmeler = await _kanalFacadeService.GetKanalAltKanalEslestirmeleriAsync(kanalIslemId);
            return Json(eslestirmeler);
        }

        [HttpGet]
        public async Task<JsonResult> eslestirilmemisKanalAltKanallariGetir(int hizmetBinasiId)
        {
            var eslestirilmemisler = await _kanalFacadeService.GetEslestirilmemisKanalAltKanallariAsync(hizmetBinasiId);
            return Json(eslestirilmemisler);
        }

        [HttpPost]
        public async Task<JsonResult> kanalAltKanalEslestirmeYap(int kanalIslemId, int kanalAltIslemId)
        {
            var result = await _kanalFacadeService.KanalAltKanalEslestirmeYapAsync(kanalIslemId, kanalAltIslemId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }

        [HttpPost]
        public async Task<JsonResult> kanalAltKanalEslestirmeKaldir(int kanalIslemId, int kanalAltIslemId)
        {
            var result = await _kanalFacadeService.KanalAltKanalEslestirmeKaldirAsync(kanalIslemId, kanalAltIslemId);
            return Json(new { islemDurum = result.Success ? 1 : 0, mesaj = result.Message });
        }
        #endregion

        #region Demo Pages
        [HttpGet]
        public async Task<IActionResult> DragAndDropDemo()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DragAndDropCloneDemo()
        {
            return View();
        }
        #endregion
    }
}