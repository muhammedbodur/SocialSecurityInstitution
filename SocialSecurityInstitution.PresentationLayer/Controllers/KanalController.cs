using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
using SocialSecurityInstitution.PresentationLayer.Services.AbstractPresentationServices;
using System.Threading.Tasks;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.PresentationLayer.Models.Kanal;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class KanalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly IKanallarService _kanallarService;
        private readonly IKanallarAltService _kanallarAltService;
        private readonly IKanalIslemleriService _kanalIslemleriService;
        private readonly IKanalAltIslemleriService _kanalAltIslemleriService;
        private readonly IKanallarCustomService _kanallarCustomService;
        private readonly IPersonellerService _personellerService;
        private readonly IPersonelCustomService _personelCustomService;
        private readonly IKanalPersonelleriService _kanalPersonelleriService;
        private readonly IKanalPersonelleriCustomService _kanalPersonelleriCustomService;
        private readonly IUserInfoService _userInfoService;

        public KanalController(IMapper mapper, IToastNotification toast, IKanallarService kanallarService, IKanallarAltService kanallarAltService, IKanalIslemleriService kanalIslemleriService, IKanalAltIslemleriService kanalAltIslemleriService, IKanallarCustomService kanallarCustomService, IPersonellerService personellerService, IPersonelCustomService personelCustomService, IKanalPersonelleriService kanalPersonelleriService, IKanalPersonelleriCustomService kanalPersonelleriCustomService, IUserInfoService userInfoService)
        {
            _mapper = mapper;
            _toast = toast;
            _kanallarService = kanallarService;
            _kanallarAltService = kanallarAltService;
            _kanalIslemleriService = kanalIslemleriService;
            _kanalAltIslemleriService = kanalAltIslemleriService;
            _kanallarCustomService = kanallarCustomService;
            _personellerService = personellerService;
            _personelCustomService = personelCustomService;
            _kanalPersonelleriService = kanalPersonelleriService;
            _kanalPersonelleriCustomService = kanalPersonelleriCustomService;
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> KanallarListele()
        {
            List<KanallarDto> kanallarDto = await _kanallarService.TGetAllAsync();
            return View(kanallarDto);
        }

        [HttpGet]
        public async Task<JsonResult> KanallarGetir(int kanalId)
        {
            var kanallarDto = await _kanallarService.TGetByIdAsync(kanalId);
            return Json(kanallarDto);
        }

        [HttpPost]
        public async Task<IActionResult> KanallarGuncelle(int kanalId, string kanalAdi)
        {
            var kanallarDto = await _kanallarService.TGetByIdAsync(kanalId);

            if (kanallarDto != null && kanalAdi != null)
            {
                kanallarDto.KanalAdi = kanalAdi;
                kanallarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanallarService.TUpdateAsync(kanallarDto);

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
                return NotFound("Kanal bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> KanallarEkle(string kanalAdi)
        {
            var kanallarDto = new KanallarDto
            {
                KanalAdi = kanalAdi,
                DuzenlenmeTarihi= DateTime.Now,
                EklenmeTarihi= DateTime.Now
            };

            var insertResult = await _kanallarService.TInsertAsync(kanallarDto);

            if (insertResult.IsSuccess)
            {
                KanallarDto kanalDto = await _kanallarService.TGetByIdAsync(Convert.ToInt32(insertResult.LastPrimaryKeyValue));

                return Ok(kanalDto);
            }
            else
            {
                return BadRequest("Ekleme işlemi başarısız oldu!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanallarSil(int kanalId)
        {
            var kanallarDto = await _kanallarService.TGetByIdAsync(kanalId);

            if (kanallarDto != null)
            {

                var deleteResult = await _kanallarService.TDeleteAsync(kanallarDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Kanal Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Kanal Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> KanallarAltListele()
        {
            List<KanallarAltDto> kanallarAltDto = await _kanallarAltService.TGetAllAsync();
            return View(kanallarAltDto);
        }

        [HttpGet]
        public async Task<JsonResult> KanallarAltGetir(int kanalAltId)
        {
            var kanallarAltDto = await _kanallarAltService.TGetByIdAsync(kanalAltId);
            return Json(kanallarAltDto);
        }

        [HttpPost]
        public async Task<IActionResult> KanallarAltGuncelle(int kanalAltId, string kanalAltAdi)
        {
            var kanallarAltDto = await _kanallarAltService.TGetByIdAsync(kanalAltId);

            if (kanallarAltDto != null && kanalAltAdi != null)
            {
                kanallarAltDto.KanalAltAdi = kanalAltAdi;
                kanallarAltDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanallarAltService.TUpdateAsync(kanallarAltDto);

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
                return NotFound("Kanal bulunamadı.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> KanallarAltEkle(string kanalAltAdi)
        {
            var kanallarAltDto = new KanallarAltDto
            {
                KanalAltAdi = kanalAltAdi,
                DuzenlenmeTarihi = DateTime.Now,
                EklenmeTarihi = DateTime.Now
            };

            var insertResult = await _kanallarAltService.TInsertAsync(kanallarAltDto);

            if (insertResult.IsSuccess)
            {
                KanallarAltDto kanalAltDto = await _kanallarAltService.TGetByIdAsync(Convert.ToInt32(insertResult.LastPrimaryKeyValue));

                return Ok(kanalAltDto);
            }
            else
            {
                return BadRequest("Ekleme işlemi başarısız oldu!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanallarAltSil(int kanalAltId)
        {
            var kanallarAltDto = await _kanallarAltService.TGetByIdAsync(kanalAltId);

            if (kanallarAltDto != null)
            {

                var deleteResult = await _kanallarAltService.TDeleteAsync(kanallarAltDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Alt Kanal Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Alt Kanal Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Alt Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> KanalIslemleriListele(int hizmetBinasiId)
        {
            List<KanalIslemleriRequestDto> kanalIslemleriRequestDto = await _kanallarCustomService.GetKanalIslemleriAsync(hizmetBinasiId);
            List<KanallarDto> kanallarDto = await _kanallarService.TGetAllAsync();

            var viewModel = new KanalListViewModel
            {
                KanalIslemleri = kanalIslemleriRequestDto,
                Kanallar = kanallarDto
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KanalIslemleriEkle(int hizmetBinasiId, int kanalId, int kanalSayiAralikBaslangic, int kanalSayiAralikBitis)
        {
            //verilen sıralama ilk ID 1 ile başlamalı, ardından gelen ise bir öncekinin bitişinden 1 tane fazla olmalı

            var kanalIslemleriDto = new KanalIslemleriDto
            {
                HizmetBinasiId = hizmetBinasiId,
                KanalId = kanalId,
                BaslangicNumara = kanalSayiAralikBaslangic,
                BitisNumara = kanalSayiAralikBitis,
                EklenmeTarihi= DateTime.Now,
                DuzenlenmeTarihi = DateTime.Now,
                KanalIslemAktiflik = Aktiflik.Aktif
            };

            var insertResult = await _kanalIslemleriService.TInsertAsync(kanalIslemleriDto);

            if (insertResult.IsSuccess)
            {
                KanalIslemleriRequestDto kanalDto = await _kanallarCustomService.GetKanalIslemleriByIdAsync(Convert.ToInt32(insertResult.LastPrimaryKeyValue));

                return Ok(kanalDto);
            }
            else
            {
                return BadRequest("Ekleme işlemi başarısız oldu!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanalIslemleriGetir(int kanalIslemId)
        {
            var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);
            return Json(kanalIslemleriDto);
        }

        [HttpPost]
        public async Task<IActionResult> KanalIslemleriGuncelle(int kanalIslemId, int kanalId, int kanalSayiAralikBaslangic, int kanalSayiAralikBitis)
        {
            var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);

            if (kanalIslemleriDto != null && kanalSayiAralikBaslangic > 0 && kanalSayiAralikBitis > 0)
            {
                kanalIslemleriDto.KanalId = kanalId;
                kanalIslemleriDto.BaslangicNumara = kanalSayiAralikBaslangic;
                kanalIslemleriDto.BitisNumara = kanalSayiAralikBitis;
                kanalIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalIslemleriService.TUpdateAsync(kanalIslemleriDto);

                if (updateResult)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Güncelleme işlemi başarısız oldu!");
                }
            }
            else if (kanalSayiAralikBaslangic < 1 || kanalSayiAralikBitis < 1)
            {
                return BadRequest("Başlangıç veya Bitiş Sayısı Geçersiz!");
            }
            else
            {
                return NotFound("Kanal bulunamadı.");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanalIslemleriAktifPasifEt(int kanalIslemId)
        {
            var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);

            if (kanalIslemleriDto != null)
            {
                var AktiflikDurum = (kanalIslemleriDto.KanalIslemAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                kanalIslemleriDto.KanalIslemAktiflik = AktiflikDurum;
                kanalIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalIslemleriService.TUpdateAsync(kanalIslemleriDto);

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
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanalIslemleriSil(int kanalIslemId)
        {
            var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);

            if (kanalIslemleriDto != null)
            {

                var deleteResult = await _kanalIslemleriService.TDeleteAsync(kanalIslemleriDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Kanal İşlemleri Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Kanal İşlemleri Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> KanalAltIslemleriListele(int hizmetBinasiId)
        {
            List<KanalAltIslemleriRequestDto> kanalAltIslemleriRequestDto = await _kanallarCustomService.GetKanalAltIslemleriAsync(hizmetBinasiId);
            List<KanallarAltDto> kanallarAltDto = await _kanallarAltService.TGetAllAsync();

            var filteredKanallarAltDto = kanallarAltDto
                .Where(k => !kanalAltIslemleriRequestDto.Any(r => r.KanalAltId == k.KanalAltId)) // KanalIslemId eşleşmeyenleri seç
                .ToList();

            var viewModel = new KanalAltListViewModel
            {
                KanalAltIslemleri = kanalAltIslemleriRequestDto,
                KanallarAlt = filteredKanallarAltDto
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KanalAltIslemleriEkle(int hizmetBinasiId, int kanalAltId)
        {
            var kanalAltIslemleriDto = new KanalAltIslemleriDto
            {
                HizmetBinasiId = hizmetBinasiId,
                KanalAltId = kanalAltId,
                EklenmeTarihi = DateTime.Now,
                DuzenlenmeTarihi = DateTime.Now,
                KanalAltIslemAktiflik = Aktiflik.Aktif
            };

            var insertResult = await _kanalAltIslemleriService.TInsertAsync(kanalAltIslemleriDto);

            if (insertResult.IsSuccess)
            {
                KanalAltIslemleriRequestDto kanalDto = await _kanallarCustomService.GetKanalAltIslemleriByIdAsync(Convert.ToInt32(insertResult.LastPrimaryKeyValue));

                return Ok(kanalDto);
            }
            else
            {
                return BadRequest("Ekleme işlemi başarısız oldu!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltIslemleriGetir(int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);
            return Json(kanalAltIslemleriDto);
        }

        [HttpPost]
        public async Task<IActionResult> KanalAltIslemleriGuncelle(int kanalAltIslemId, int kanalAltId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null)
            {
                kanalAltIslemleriDto.KanalAltId = kanalAltId;
                kanalAltIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);

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
                return NotFound("Kanal bulunamadı.");
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltIslemleriAktifPasifEt(int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null)
            {
                var AktiflikDurum = (kanalAltIslemleriDto.KanalAltIslemAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                kanalAltIslemleriDto.KanalAltIslemAktiflik = AktiflikDurum;
                kanalAltIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);

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
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Alt Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltIslemleriSil(int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null)
            {

                var deleteResult = await _kanalAltIslemleriService.TDeleteAsync(kanalAltIslemleriDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Alt Kanal İşlemleri Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Alt Kanal İşlemleri Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Alt Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> KanalPersonelleri(int hizmetBinasiId)
        {
            var TcKimlikNo = _userInfoService.GetTcKimlikNo();

            var personelDto = await _personelCustomService.TGetByTcKimlikNoAsync(TcKimlikNo);

            List<KanalIslemleriRequestDto> kanalIslemleriRequestDto = await _kanallarCustomService.GetKanalIslemleriAsync(hizmetBinasiId);

            ViewBag.KanalIslemleri = kanalIslemleriRequestDto;

            //ViewBag.KanalPersonelleri = kanalPersonelleriDto;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PersonelAltKanalEslestir()
        {
            ///*
            //    personeller getirilecek,
            //    kanalAltIslemleri getirilecek
            //*/

            //var personellerDto = await _personelCustomService.GetPersonellerDepartmanIdAndHizmetBinasiIdAsync(departmanId, hizmetBinasiId);

            //var kanalAltIslemleriDto = await _kanallarCustomService.GetKanalAltIslemleriAsync(hizmetBinasiId);

            //var viewModel = new AltKanallarPersonellerViewModel
            //{
            //    PersonellerList = personellerDto,
            //    KanalAltIslemleriList = kanalAltIslemleriDto
            //};

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> KanalAltPersonelleriGetir(int hizmetBinasiId)
        {
            var personellerDto = await _kanalPersonelleriCustomService.GetPersonellerAltKanallarAsync(hizmetBinasiId);

            return Json(personellerDto);
        }

        [HttpGet]
        public async Task<JsonResult> PersonelAltKanallariGetir(string tcKimlikNo)
        {
            var personelKanallariDto = await _kanalPersonelleriCustomService.GetPersonelAltKanallariAsync(tcKimlikNo);

            return Json(personelKanallariDto);
        }
        
        [HttpGet]
        public async Task<JsonResult> PersonelAltKanallarEslesmeyenleriGetir(string tcKimlikNo, int hizmetBinasiId)
        {
            var kanallarAltDto = await _kanalPersonelleriCustomService.GetPersonelAltKanallarEslesmeyenlerAsync(tcKimlikNo, hizmetBinasiId);

            return Json(kanallarAltDto);
        }

        [HttpPost]
        public async Task<JsonResult> PersonelAltKanalEslestirmeYap(string tcKimlikNo, int kanalAltIslemId, int uzmanlikSeviye)
        {
            try
            {
                // Personel ve kanal alt işlem ilişkisi var mı kontrol et
                var personelAltKanallari = await _kanalPersonelleriCustomService.GetPersonelAltKanallariAsync(tcKimlikNo);
                var eslesme = personelAltKanallari.FirstOrDefault(p => p.TcKimlikNo == tcKimlikNo && p.KanalAltIslemId == kanalAltIslemId);

                // Var ise güncelle
                if (eslesme != null)
                {
                    var kanalPersonelDto = new KanalPersonelleriDto
                    {
                        KanalPersonelId = eslesme.KanalPersonelId,
                        KanalAltIslemId = kanalAltIslemId,
                        TcKimlikNo = eslesme.TcKimlikNo,
                        Uzmanlik = (PersonelUzmanlik)uzmanlikSeviye,
                        DuzenlenmeTarihi = DateTime.Now
                    };
                    var updateResult = await _kanalPersonelleriService.TUpdateAsync(kanalPersonelDto);

                    if (updateResult)
                    {
                        return Json(new { islemDurum = 1, mesaj = "Personel ve Alt Kanal Eşleştirildi" });
                    }
                    else
                    {
                        return Json(new { islemDurum = 0, mesaj = "Personel ve Alt Kanal Eşleştirilemedi!" });
                    }
                }
                else
                {
                    var kanalPersonelDto = new KanalPersonelleriDto
                    {
                        KanalAltIslemId = kanalAltIslemId,
                        TcKimlikNo = tcKimlikNo,
                        Uzmanlik = (PersonelUzmanlik)uzmanlikSeviye,
                        DuzenlenmeTarihi = DateTime.Now,
                        EklenmeTarihi = DateTime.Now
                    };
                    var insertResult = await _kanalPersonelleriService.TInsertAsync(kanalPersonelDto);

                    if (insertResult.IsSuccess)
                    {
                        return Json(new { islemDurum = 1, mesaj = "Personel ve Alt Kanal Eşleştirildi" });
                    }
                    else
                    {
                        return Json(new { islemDurum = 0, mesaj = "Personel ve Alt Kanal Eşleştirilemedi!" });
                    }
                }

                return Json(eslesme);
            }
            catch (Exception ex)
            {
                return Json(new { islemDurum = 0, mesaj = "Hata: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> PersonelAltKanalEslestirmeKaldir(string tcKimlikNo, int kanalAltIslemId)
        {
            try
            {
                // Personel ve kanal alt işlem ilişkisi var mı kontrol et
                var personelAltKanallari = await _kanalPersonelleriCustomService.GetPersonelAltKanallariAsync(tcKimlikNo);
                var eslesme = personelAltKanallari.FirstOrDefault(p => p.TcKimlikNo == tcKimlikNo && p.KanalAltIslemId == kanalAltIslemId);

                // Var ise sil
                if (eslesme != null)
                {
                    var kanalPersonelDto = new KanalPersonelleriDto
                    {
                        KanalPersonelId = eslesme.KanalPersonelId,
                        KanalAltIslemId = eslesme.KanalAltIslemId,
                        TcKimlikNo = eslesme.TcKimlikNo,
                        Uzmanlik = eslesme.Uzmanlik,
                        DuzenlenmeTarihi = DateTime.Now
                    };
                    var updateResult = await _kanalPersonelleriService.TDeleteAsync(kanalPersonelDto);

                    if (updateResult)
                    {
                        return Json(new { islemDurum = 1, mesaj = "Personel ve Alt Kanal Eşleştirilmesi Kaldırıldı" });
                    }
                    else
                    {
                        return Json(new { islemDurum = 0, mesaj = "Personel ve Alt Kanal Eşleştirilmesi Kaldırılamadı!" });
                    }
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Personel ve Alt Kanal Eşleştirilmesi Bulunamamadı!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { islemDurum = 0, mesaj = "Hata: " + ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> KanalPersonelleriDemo(int departmanId, int hizmetBinasiId)
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> KanalPersonelleriDemo2(int hizmetBinasiId)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> KanalEslestir(int hizmetBinasiId)
        {
            var kanalAltIslemleriRequestDto = await _kanallarCustomService.GetKanalAltIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);
            var kanalAltIslemleriEslestirmeSayisiDto = await _kanallarCustomService.GetKanalAltIslemleriEslestirmeSayisiAsync(hizmetBinasiId);
            
            var viewModel = new KanalIslemleriEslestirViewModel
            {
                KanalAltIslemleri = kanalAltIslemleriRequestDto,
                KanalAltIslemleriEslestirmeSayisi = kanalAltIslemleriEslestirmeSayisiDto
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> KanallarListesi(int hizmetBinasiId)
        {
            var kanalAltIslemleriEslestirmeSayisiDto = await _kanallarCustomService.GetKanalAltIslemleriEslestirmeSayisiAsync(hizmetBinasiId);

            return Json(kanalAltIslemleriEslestirmeSayisiDto);
        }

        [HttpGet]
        public async Task<JsonResult> kanalAltKanalEslestirmeleriGetir(int kanalIslemId)
        {
            var kanalAltIslemleri = await _kanallarCustomService.GetKanalAltIslemleriByIslemIdAsync(kanalIslemId);
            return Json(kanalAltIslemleri);
        }

        [HttpGet]
        public async Task<JsonResult> eslestirilmemisKanalAltKanallariGetir(int hizmetBinasiId)
        {
            var eslestirilmemisAltKanallar = await _kanallarCustomService.GetKanalAltIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);
            return Json(eslestirilmemisAltKanallar);
        }

        [HttpPost]
        public async Task<JsonResult> kanalAltKanalEslestirmeYap(int kanalIslemId, int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null)
            {
                kanalAltIslemleriDto.KanalIslemId = kanalIslemId;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);

                if (updateResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Alt Kanal İşlemleri Eşleştirme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Alt Kanal İşlemleri Eşleştirme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Alt Kanal Bulunamadı!" });
            }
        }
        
        [HttpPost]
        public async Task<JsonResult> kanalAltKanalEslestirmeKaldir(int kanalIslemId, int kanalAltIslemId)
        {
            var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);

            if (kanalAltIslemleriDto != null && kanalAltIslemleriDto.KanalIslemId == kanalIslemId)
            {
                kanalAltIslemleriDto.KanalIslemId = null;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);

                if (updateResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Alt Kanal İşlemleri Eşleştirme Kaldırma İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Alt Kanal İşlemleri Eşleştirme Kaldırma İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Alt Kanal Bulunamadı!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DragAndDrop()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DragAndDropDemo()
        {
            return View();
        }

    }
}
