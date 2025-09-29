using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using NToastNotify.Helpers;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class BankoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBankolarService _bankolarService;
        private readonly IBankolarKullaniciService _bankolarKullaniciService;
        private readonly IBankolarKullaniciCustomService _bankolarKullaniciCustomService;
        private readonly IBankolarCustomService _bankolarCustomService;
        private readonly IHizmetBinalariCustomService _hizmetBinalariCustomService;
        private readonly IToastNotification _toast;

        public BankoController(IMapper mapper, IBankolarService bankolarService, IBankolarKullaniciService bankolarKullaniciService, IBankolarKullaniciCustomService bankolarKullaniciCustomService, IBankolarCustomService bankolarCustomService, IHizmetBinalariCustomService hizmetBinalariCustomService, IToastNotification toast)
        {
            _mapper = mapper;
            _bankolarService = bankolarService;
            _bankolarKullaniciService = bankolarKullaniciService;
            _bankolarKullaniciCustomService = bankolarKullaniciCustomService;
            _bankolarCustomService = bankolarCustomService;
            _hizmetBinalariCustomService = hizmetBinalariCustomService;
            _toast = toast;
        }

        public async Task<IActionResult> Listele()
        {
            List<BankolarRequestDto> bankolarDto = await _bankolarCustomService.GetBankolarWithDetailsAsync();
            return View(bankolarDto);
        }

        [HttpGet]
        public async Task<JsonResult> DepartmanPersonelListeleJson(int bankoId)
        {
            List<DepartmanPersonelleriDto> departmanPersonelleriDto = await _bankolarCustomService.GetDeparmanPersonelleriAsync(bankoId);
            return Json(departmanPersonelleriDto);
        }

        [HttpGet]
        public async Task<JsonResult> HizmetBinasiPersonelListeleJson(int bankoId)
        {
            List<HizmetBinasiPersonelleriDto> hizmetBinasiPersonelleriDto = await _bankolarCustomService.GetHizmetBinasiPersonelleriAsync(bankoId);
            return Json(hizmetBinasiPersonelleriDto);
        }

        [HttpGet]
        public async Task<JsonResult> Getir(int bankoId)
        {
            var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);
            return Json(bankolarDto);
        }

        [HttpGet]
        public async Task<JsonResult> AktifPasifEt(int bankoId)
        {
            // Business logic service katmanına taşındı
            var (success, message, aktiflikDurum) = await _bankolarCustomService.ToggleBankoAktiflikAsync(bankoId);
            
            return Json(new 
            { 
                islemDurum = success ? 1 : 0, 
                aktiflikDurum = aktiflikDurum, 
                mesaj = message 
            });
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(int bankoId, int bankoNo)
        {
            var bankolarCustomDto = await _bankolarCustomService.GetBankoByIdAsync(bankoId);

            if (bankolarCustomDto != null)
            {
                bankolarCustomDto.BankoNo = bankoNo;
                bankolarCustomDto.BankoAktiflik = Aktiflik.Aktif;
                bankolarCustomDto.BankoDuzenlenmeTarihi = DateTime.Now;

                var bankolarDto = _mapper.Map<BankolarDto>(bankolarCustomDto);

                var updateResult = await _bankolarService.TUpdateAsync(bankolarDto);

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
                return NotFound("Banko bulunamadı.");
            }
        }

        [HttpPost]
        [HttpGet]
        public async Task<JsonResult> BankoPersonelGuncelle(int bankoId, string tcKimlikNo)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message, personelData) = await _bankolarCustomService.UpdateBankoPersonelAsync(bankoId, tcKimlikNo);
            
            if (success)
            {
                return Json(new { success = message, data = personelData });
            }
            else
            {
                return Json(new { error = message, data = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(int bankoNo, int hizmetBinasiId, int departmanId)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message, bankoData) = await _bankolarCustomService.CreateBankoAsync(bankoNo, hizmetBinasiId, departmanId);
            
            if (success)
            {
                return Ok(bankoData);
            }
            else
            {
                return BadRequest(message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> Sil(int bankoId)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message) = await _bankolarCustomService.DeleteBankoAsync(bankoId);
            
            return Json(new 
            { 
                islemDurum = success ? 1 : 0, 
                mesaj = message 
            });
        }

        [HttpPost]
        public async Task<IActionResult> KatTipiGuncelle(int bankoId, int katTipi)
        {
            // ✅ Business logic service katmanına taşındı
            var (success, message) = await _bankolarCustomService.UpdateBankoKatTipiAsync(bankoId, katTipi);
            
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(message);
            }
        }
    }
}
