using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        private readonly IPersonelFacadeService _personelFacadeService;
        private readonly IToastNotification _toast;

        public PersonelController(
            IPersonelFacadeService personelFacadeService,
            IToastNotification toast)
        {
            _personelFacadeService = personelFacadeService;
            _toast = toast;
        }

        #region Personel CRUD Operations
        [HttpGet]
        public async Task<IActionResult> Ekle()
        {
            var viewModel = await _personelFacadeService.CreateEmptyPersonelViewDtoWithDropdownsAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Kaydet(PersonellerDto personellerDto)
        {
            var result = await _personelFacadeService.CreatePersonelAsync(personellerDto);

            if (result.Success)
            {
                _toast.AddSuccessToastMessage(result.Message);
                return RedirectToAction("Listele");
            }
            else
            {
                _toast.AddErrorToastMessage(result.Message);
                return View("Ekle");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Duzenle(string TcKimlikNo)
        {
            if (string.IsNullOrEmpty(TcKimlikNo))
            {
                _toast.AddErrorToastMessage("Geçersiz TC Kimlik No parametresi");
                return RedirectToAction("Listele");
            }

            var viewModel = await _personelFacadeService.GetPersonelForEditAsync(TcKimlikNo);

            if (viewModel == null)
            {
                _toast.AddErrorToastMessage("Personel bulunamadı");
                return RedirectToAction("Listele");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(PersonellerViewDto model)
        {
            try
            {
                // Model validation (Data Annotations)
                if (!ModelState.IsValid)
                {
                    // Validation hatalarını logla
                    foreach (var modelError in ModelState)
                    {
                        var key = modelError.Key;
                        var errors = modelError.Value.Errors;

                        foreach (var error in errors)
                        {
                            Console.WriteLine($"ModelState Error - Key: {key}, Error: {error.ErrorMessage}");
                            _toast.AddErrorToastMessage($"{key}: {error.ErrorMessage}");
                        }
                    }

                    // Dropdownları tekrar doldur ve view'e geri dön
                    await _personelFacadeService.PopulateDropdownListsAsync(model);
                    return View("Duzenle", model);
                }

                // Business validation ve güncelleme
                var result = await _personelFacadeService.UpdatePersonelAsync(model);

                if (result.Success)
                {
                    _toast.AddSuccessToastMessage(result.Message);
                    return RedirectToAction("Listele");
                }
                else
                {
                    // Business validation hatalarını toast olarak göster
                    _toast.AddErrorToastMessage(result.Message);

                    // Validation hatalarını ModelState'e ekle (form'da göstermek için)
                    if (result.Message.Contains("Validation Error:"))
                    {
                        var errors = result.Message.Replace("Validation Error: ", "").Split(", ");
                        foreach (var error in errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    await _personelFacadeService.PopulateDropdownListsAsync(model);
                    return View("Duzenle", model);
                }
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage($"Beklenmeyen bir hata oluştu: {ex.Message}");
                await _personelFacadeService.PopulateDropdownListsAsync(model);
                return View("Duzenle", model);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ValidatePersonelUpdate([FromBody] PersonellerViewDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    return Json(new { success = false, errors = errors });
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            var personelRequests = await _personelFacadeService.GetPersonellerWithDetailsAsync();
            return View("Listele", personelRequests);
        }

        [HttpGet]
        public async Task<JsonResult> AktifPasifEt(string TcKimlikNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TcKimlikNo))
                {
                    return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Geçersiz TC Kimlik No" });
                }

                var viewModel = await _personelFacadeService.GetPersonelForEditAsync(TcKimlikNo);
                if (viewModel == null)
                {
                    return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Personel Bulunamadı!" });
                }

                // Toggle only between Aktif and Pasif. If current is other (e.g., Emekli), block the toggle.
                var current = viewModel.PersonelAktiflikDurum;
                var aktifType = SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums.PersonelAktiflikDurum.Aktif;
                var pasifType = SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums.PersonelAktiflikDurum.Pasif;

                if (current != aktifType && current != pasifType)
                {
                    return Json(new { islemDurum = 0, aktiflikDurum = current.ToString(), mesaj = "Bu aktiflik durumunda değişiklik yapılamaz" });
                }

                var yeniDurum = (current == aktifType) ? pasifType : aktifType;
                viewModel.PersonelAktiflikDurum = yeniDurum;

                var updateResult = await _personelFacadeService.UpdatePersonelAsync(viewModel);
                if (updateResult.Success)
                {
                    return Json(new { islemDurum = 1, aktiflikDurum = yeniDurum.ToString(), mesaj = $"{yeniDurum} Etme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, aktiflikDurum = current.ToString(), mesaj = updateResult.Message ?? "Güncelleme işlemi başarısız oldu!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Sil(string TcKimlikNo)
        {
            if (string.IsNullOrEmpty(TcKimlikNo))
            {
                _toast.AddErrorToastMessage("Geçersiz TC Kimlik No parametresi");
                return RedirectToAction("Listele");
            }

            var result = await _personelFacadeService.DeletePersonelAsync(TcKimlikNo);

            if (result.Success)
            {
                _toast.AddSuccessToastMessage(result.Message);
            }
            else
            {
                _toast.AddErrorToastMessage(result.Message);
            }

            return RedirectToAction("Listele");
        }
        #endregion

        #region Advanced Queries (Optional - Facade'e delegate edildi)
        [HttpGet]
        public async Task<JsonResult> GetPersonellerByDepartman(int departmanId, int hizmetBinasiId)
        {
            var personeller = await _personelFacadeService.GetPersonellerByDepartmanAndHizmetBinasiAsync(departmanId, hizmetBinasiId);
            return Json(personeller);
        }

        [HttpGet]
        public async Task<JsonResult> GetActivePersonelList()
        {
            var activePersoneller = await _personelFacadeService.GetActivePersonelListAsync();
            return Json(activePersoneller);
        }
        #endregion

        [HttpGet]
        public async Task<JsonResult> GetIlcelerByIlId(int ilId)
        {
            try
            {
                if (ilId <= 0)
                {
                    return Json(new List<object>());
                }

                var ilceler = await _personelFacadeService.GetIlcelerByIlIdAsync(ilId);
                var result = ilceler.Select(x => new {
                    ilceId = x.IlceId,
                    ilceAdi = x.IlceAdi
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = "İlçeler yüklenirken hata oluştu" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetServislerByDepartmanId(int departmanId)
        {
            try
            {
                if (departmanId <= 0)
                {
                    return Json(new List<object>());
                }

                var servisler = await _personelFacadeService.GetServislerByDepartmanIdAsync(departmanId);
                var result = servisler.Select(x => new {
                    id = x.ServisId,
                    servisAdi = x.ServisAdi
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Servisler yüklenirken hata oluştu" });
            }
        }
    }
}