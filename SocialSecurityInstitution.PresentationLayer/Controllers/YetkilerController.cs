using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class YetkilerController : Controller
    {
        private readonly IYetkilerService _yetkilerService;
        private readonly IYetkilerCustomService _yetkilerCustomService;
        private readonly IPersonellerService _personellerService;
        private readonly IPersonelCustomService _personellerCustomService;
        private readonly IPersonelYetkileriService _personelYetkileriService;
        private readonly IPersonelYetkileriCustomService _personelYetkileriCustomService;

        public YetkilerController(IYetkilerService yetkilerService, IYetkilerCustomService yetkilerCustomService, IPersonellerService personellerService, IPersonelCustomService personellerCustomService, IPersonelYetkileriService personelYetkileriService, IPersonelYetkileriCustomService personelYetkileriCustomService)
        {
            _yetkilerService = yetkilerService;
            _yetkilerCustomService = yetkilerCustomService;
            _personellerService = personellerService;
            _personellerCustomService = personellerCustomService;
            _personelYetkileriService = personelYetkileriService;
            _personelYetkileriCustomService = personelYetkileriCustomService;
        }

        public async Task<IActionResult> Index()
        {
            // Tüm Yetkileri servisten alıyoruz
            var sortedAnaYetkiler = await _yetkilerCustomService.GetAllYetkilerWithIncludesAsync();

            ViewBag.AnaYetkiler = sortedAnaYetkiler;

            var personeller = await _personellerCustomService.GetActivePersonelListAsync();

            ViewBag.Personeller = personeller;

            return View();
        }

        public async Task<JsonResult> GetYetkilerByPersonel(string tcKimlikNo)
        {
            var yetkiler = await _personelYetkileriCustomService.GetYetkilerByPersonelTcKimlikNoAsync(tcKimlikNo);

            return Json(yetkiler);
        }

        public IActionResult YetkiTanımla(string yetkiTuru)
        {
            ViewBag.YetkiTuru = yetkiTuru;

            if (yetkiTuru == "OrtaYetki" || yetkiTuru == "AltYetki")
            {
                // Controller'ı Ana Yetki olarak yükle
                var anaYetkiler = _yetkilerService.TGetAllAsync().Result.Where(x => x.YetkiTuru == YetkiTurleri.AnaYetki).ToList();
                ViewBag.AnaYetkiler = new SelectList(anaYetkiler, "YetkiId", "YetkiAdi");

                if (yetkiTuru == "AltYetki")
                {
                    // Orta Yetkiler yani Action'ları yükle
                    var ortaYetkiler = _yetkilerService.TGetAllAsync().Result.Where(x => x.YetkiTuru == YetkiTurleri.OrtaYetki).ToList();
                    ViewBag.OrtaYetkiler = new SelectList(ortaYetkiler, "YetkiId", "YetkiAdi");
                }
            }

            return PartialView("_YetkiTanımlaPartial");
        }

        public async Task<JsonResult> GetOrtaYetkiler(int anaYetkiId)
        {
            var ortaYetkiler = await _yetkilerCustomService.GetOrtaYetkilerByAnaYetkiIdAsync(anaYetkiId);
            return Json(ortaYetkiler);
        }

        public async Task<JsonResult> GetAltYetkiler(int ortaYetkiId)
        {
            var altYetkiler = await _yetkilerCustomService.GetAltYetkilerByOrtaYetkiIdAsync(ortaYetkiId);
            return Json(altYetkiler);
        }

        [HttpPost]
        public async Task<IActionResult> AddYetki(int YetkiId, string YetkiAdi, string Aciklama)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Seçilen YetkiId ile veritabanından yetkiyi alıyoruz
                    var selectedYetki = await _yetkilerService.TGetByIdAsync(YetkiId);

                    if (selectedYetki == null && YetkiId > 0)
                    {
                        ModelState.AddModelError("", "Geçersiz Yetki Seçimi.");
                        return View("Index");
                    }

                    YetkilerDto newYetkiDto = null;

                    if (selectedYetki == null && YetkiId == -1)
                    {
                        // Yeni Ana Yetki oluşturuyorum
                        newYetkiDto = new YetkilerDto
                        {
                            YetkiAdi = YetkiAdi,
                            Aciklama = Aciklama,
                            YetkiTuru = YetkiTurleri.AnaYetki,
                            ControllerAdi = YetkiAdi,
                            ActionAdi = null,
                            UstYetkiId = -1
                        };
                    }
                    // Eğer seçilen yetki bir Orta Yetki ise
                    else if (selectedYetki != null && selectedYetki.YetkiTuru == YetkiTurleri.AnaYetki)
                    {
                        newYetkiDto = new YetkilerDto
                        {
                            YetkiAdi = YetkiAdi,
                            Aciklama = Aciklama,
                            YetkiTuru = YetkiTurleri.OrtaYetki,
                            ActionAdi = YetkiAdi,
                            ControllerAdi = selectedYetki.ControllerAdi,
                            UstYetkiId = selectedYetki.YetkiId
                        };
                    }
                    else if (selectedYetki != null && selectedYetki.YetkiTuru == YetkiTurleri.OrtaYetki)
                    {
                        newYetkiDto = new YetkilerDto
                        {
                            YetkiAdi = YetkiAdi,
                            Aciklama = Aciklama,
                            YetkiTuru = YetkiTurleri.AltYetki,
                            ActionAdi = selectedYetki.ActionAdi,
                            ControllerAdi = selectedYetki.ControllerAdi,
                            UstYetkiId = selectedYetki.YetkiId
                        };
                    }
                    else
                    {
                        ModelState.AddModelError("", "Geçersiz Yetki Seçimi.");
                        return View("Index");
                    }

                    newYetkiDto.YetkiId = 0;

                    // Yetki ekleme işlemi
                    var result = await _yetkilerService.TInsertAsync(newYetkiDto);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Hata oluşursa, kullanıcıya anlamlı bir hata mesajı döndür
                    ModelState.AddModelError("", $"Yetki ekleme sırasında bir hata oluştu: {ex.Message}");
                    return View("Index");
                }
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonelYetkileri(string tcKimlikNo)
        {
            // Seçilen personelin yetkilerini veritabanından çek
            var personelYetkileri = await _yetkilerCustomService.GetPersonelYetkileriAsync(tcKimlikNo);

            // Partial View'e personelin yetkilerini gönder
            return PartialView("_PersonelYetkileriPartial", personelYetkileri);
        }

        [HttpPost]
        public async Task<IActionResult> PersonelYetkileriKaydet([FromBody] List<PersonelYetkilerRequestDto> personelYetkileriDtos)
        {
            if (personelYetkileriDtos == null || !personelYetkileriDtos.Any())
            {
                return BadRequest("Yetkiler eksik.");
            }

            try
            {
                foreach (var personelYetkileriDto in personelYetkileriDtos)
                {
                    if (string.IsNullOrEmpty(personelYetkileriDto.TcKimlikNo))
                    {
                        return BadRequest("Personel bilgisi eksik.");
                    }

                    // Gelen yetkileri kaydediyoruz
                    var yeniYetki = new PersonelYetkileriDto
                    {
                        TcKimlikNo = personelYetkileriDto.TcKimlikNo,
                        YetkiId = personelYetkileriDto.YetkiId,
                        YetkiTipleri = (YetkiTipleri)personelYetkileriDto.YetkiTipi,
                        EklenmeTarihi = DateTime.Now,
                        DuzenlenmeTarihi = DateTime.Now
                    };

                    // Yetkileri veritabanına kaydediyoruz
                    await _personelYetkileriService.TInsertAsync(yeniYetki);
                }

                return Ok("Yetkiler başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Yetki kaydetme sırasında hata: {ex.Message}");
            }
        }
    }
}
