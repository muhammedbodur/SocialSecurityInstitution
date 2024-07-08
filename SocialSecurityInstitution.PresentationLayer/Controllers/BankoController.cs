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
        public async Task<JsonResult> Getir(int bankoId)
        {
            var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);
            return Json(bankolarDto);
        }

        [HttpGet]
        public async Task<JsonResult> AktifPasifEt(int bankoId)
        {
            var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);

            if (bankolarDto != null)
            {
                var AktiflikDurum = (bankolarDto.BankoAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                bankolarDto.BankoAktiflik = AktiflikDurum;
                bankolarDto.DuzenlenmeTarihi = DateTime.Now;

                if (AktiflikDurum == Aktiflik.Pasif)
                {
                    var bankolarKullaniciDto = await _bankolarKullaniciCustomService.GetBankolarKullaniciByBankoIdAsync(bankoId);
                    var deleteResult = true;
                    if (bankolarKullaniciDto != null)
                    {
                        deleteResult = await _bankolarKullaniciService.TDeleteAsync(bankolarKullaniciDto);
                    }
                    
                    if(deleteResult)
                    {
                        var updateResult = await _bankolarService.TUpdateAsync(bankolarDto);

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
                        return Json(new { islemDurum = 0, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarısız Oldu!" });
                    }
                }
                else
                {
                    var updateResult = await _bankolarService.TUpdateAsync(bankolarDto);

                    if (updateResult)
                    {
                        return Json(new { islemDurum = 1, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarılı Oldu " });
                    }
                    else
                    {
                        return Json(new { islemDurum = 0, aktiflikDurum = AktiflikDurum.ToString(), mesaj = AktiflikDurum + " Etme İşlemi Başarısız Oldu!" });
                    }
                }
            }
            else
            {
                return Json(new { islemDurum = 0, aktiflikDurum = "", mesaj = "Banko Bulunamadı!" });
            }
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
            try
            {
                var personelDto = await _bankolarCustomService.GetBankoPersonelDetailAsync(tcKimlikNo);
                var bankolarKullaniciDto = await _bankolarKullaniciCustomService.GetBankolarKullaniciByBankoIdAsync(bankoId);
                var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);

                if (personelDto != null && bankolarDto.BankoAktiflik == Aktiflik.Aktif)
                {
                    if (bankolarKullaniciDto != null)
                    {
                        // Daha önce bankoya ait kullanıcı var ve güncelleme yapılacak
                        var yeniKullanici = new BankolarKullaniciDto
                        {
                            BankoKullaniciId = bankolarKullaniciDto.BankoKullaniciId,
                            BankoId = bankoId,
                            TcKimlikNo = tcKimlikNo,
                            EklenmeTarihi = bankolarKullaniciDto.EklenmeTarihi,
                            DuzenlenmeTarihi = DateTime.Now
                        };

                        var updateResult = await _bankolarKullaniciService.TUpdateAsync(yeniKullanici);

                        if (updateResult)
                        {
                            return Json(new { success = "İşlem başarıyla tamamlandı.", data = _mapper.Map<PersonelRequestDto>(personelDto) });
                        }
                        else
                        {
                            return Json(new { error = "Güncelleme işlemi başarısız oldu!" , data = false});
                        }
                    }
                    else
                    {
                        // Daha önce bankoya ait bir kullanıcı yok, ekleme yapılacak
                        var yeniKullanici = new BankolarKullaniciDto
                        {
                            BankoId = bankoId,
                            TcKimlikNo = tcKimlikNo,
                            EklenmeTarihi = DateTime.Now,
                            DuzenlenmeTarihi = DateTime.Now
                        };

                        var insertResult = await _bankolarKullaniciService.TInsertAsync(yeniKullanici);

                        if (insertResult.IsSuccess)
                        {
                            return Json(new { success = "İşlem başarıyla tamamlandı." , data = _mapper.Map<PersonelRequestDto>(personelDto)});
                        }
                        else
                        {
                            return Json(new { error = "Ekleme işlemi başarısız oldu!", data = false });
                        }
                    }
                }
                else if (bankolarDto != null && bankolarDto.BankoAktiflik == Aktiflik.Pasif)
                {
                    return Json(new { error = "İşlem yapılan banko pasif durumda ve pasif durumdaki bankoya personel atanamaz!", data = false });
                }
                else
                {
                    return Json(new { error = "Banko bulunamadı.", data = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message, data = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(int bankoNo, int hizmetBinasiId, int departmanId)
        {
            var bankolarDto = new BankolarDto
            {
                BankoNo = bankoNo,
                HizmetBinasiId = hizmetBinasiId,
                EklenmeTarihi = DateTime.Now,
                BankoAktiflik = Aktiflik.Aktif
            };

            var hizmetBinalariDepartmanlarDto = await _hizmetBinalariCustomService.GetActiveHizmetBinasiAsync(hizmetBinasiId, departmanId);

            if (hizmetBinalariDepartmanlarDto != null)
            {
                var insertResult = await _bankolarService.TInsertAsync(bankolarDto);

                if (insertResult.IsSuccess)
                {
                    var bankolarHizmetBinalariDepartmanlarDto = _mapper.Map<BankolarHizmetBinalariDepartmanlarDto>(hizmetBinalariDepartmanlarDto);
                    bankolarHizmetBinalariDepartmanlarDto.BankoId = (int)insertResult.LastPrimaryKeyValue;
                    bankolarHizmetBinalariDepartmanlarDto.BankoNo = bankoNo;
                    bankolarHizmetBinalariDepartmanlarDto.BankoAktiflik = Aktiflik.Aktif;
                    bankolarHizmetBinalariDepartmanlarDto.BankoEklenmeTarihi = DateTime.Now;
                    bankolarHizmetBinalariDepartmanlarDto.BankoDuzenlenmeTarihi = DateTime.Now;

                    return Ok(bankolarHizmetBinalariDepartmanlarDto);
                }
                else
                {
                    return BadRequest("Ekleme işlemi başarısız oldu!");
                }
            }
            else
            {
                return BadRequest("Hizmet Binası Bulunamadı!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> Sil(int bankoId)
        {
            var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);

            if (bankolarDto != null)
            {

                var deleteResult = await _bankolarService.TDeleteAsync(bankolarDto);

                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Banko Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Bankoyu Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Banko Bulunamadı!" });
            }
        }
    }
}
