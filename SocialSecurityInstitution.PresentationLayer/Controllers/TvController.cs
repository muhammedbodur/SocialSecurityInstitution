using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using SocialSecurityInstitution.PresentationLayer.Models.Kiosk;
using SocialSecurityInstitution.PresentationLayer.Models.Tv;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    public class TvController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly ISiralarCustomService _siralarCustomService;
        private readonly IHizmetBinalariService _hizmetBinalariService;
        private readonly ITvlerService _tvlerService;
        private readonly ITvlerCustomService _tvlerCustomService;
        private readonly ITvBankolariService _tvlerBankolariService;

        public TvController(IMapper mapper, IToastNotification toast, ISiralarCustomService siralarCustomService, IHizmetBinalariService hizmetBinalariService, ITvlerService tvlerService, ITvlerCustomService tvlerCustomService, ITvBankolariService tvlerBankolariService)
        {
            _mapper = mapper;
            _toast = toast;
            _siralarCustomService = siralarCustomService;
            _hizmetBinalariService = hizmetBinalariService;
            _tvlerService = tvlerService;
            _tvlerCustomService = tvlerCustomService;
            _tvlerBankolariService = tvlerBankolariService;
        }

        [HttpGet]
        public async Task<IActionResult> Siralar(int TvId, int Code)
        {
            // TvId'ye göre TV bilgilerini alıyoruz
            var tvBilgisi = await _tvlerService.TGetByIdAsync(TvId);


            if (Code < 0 || Code != 837175)
            {
                //Code bilgisi gelmez ise, hata mesajı döndürülür
                return NotFound("Code bilgisi yanlış ve/veya eksik!");
            }
            else if (tvBilgisi == null)
            {
                // Eğer TV bulunamazsa, hata mesajı döndürülür
                return NotFound("TV bulunamadı.");
            }

            int hizmetBinasiId = tvBilgisi.HizmetBinasiId;

            var siraListesi = await _siralarCustomService.GetSiralarForTvWithTvId(TvId) ?? new List<SiralarTvListeDto>();
            var hizmetBinasi = await _hizmetBinalariService.TGetByIdAsync(hizmetBinasiId) ?? new HizmetBinalariDto
            {
                HizmetBinasiAdi = "Belirlenmemiş",
            };

            var viewModel = new TvListViewModel
            {
                SiralarTvListe = siraListesi,
                HizmetBinasi = hizmetBinasi,
                Tvler = tvBilgisi
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DragDropDemo()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TvIslemleriListele(int hizmetBinasiId)
        {
            List<TvlerDetailDto> TvlerDetailDto = await _tvlerCustomService.GetTvlerWithHizmetBinasiId(hizmetBinasiId);
            return View(TvlerDetailDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> TvSil(int tvId)
        {
            var tv = await _tvlerService.TGetByIdAsync(tvId);

            if (tv != null)
            {
                var deleteResult = await _tvlerService.TDeleteAsync(tv);
                if (deleteResult)
                {
                    return Json(new { islemDurum = 1, mesaj = "Tv Silme İşlemi Başarılı Oldu " });
                }
                else
                {
                    return Json(new { islemDurum = 0, mesaj = "Tv Silme İşlemi Başarısız Oldu!" });
                }
            }
            else
            {
                return Json(new { islemDurum = 0, mesaj = "Tv Bulunamadı!" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TvEkle(int hizmetBinasiId)
        {
            try
            {
                var hizmetBinasi = await _hizmetBinalariService.TGetByIdAsync(hizmetBinasiId);
                if (hizmetBinasi == null)
                {
                    return BadRequest("Geçersiz Hizmet Binasi ID'si.");
                }

                var tvler = new TvlerDto
                {
                    HizmetBinasiId = hizmetBinasi.HizmetBinasiId,
                    KatTipi = BusinessObjectLayer.CommonEntities.Enums.KatTipi.zemin,
                    Aciklama = null,
                    IslemZamani = DateTime.Now,
                };

                var insertResult = await _tvlerService.TInsertAsync(tvler);

                if (insertResult.IsSuccess)
                {
                    var tv = await _tvlerCustomService.GetTvWithTvId(Convert.ToInt32(insertResult.LastPrimaryKeyValue));

                    return Ok(tv);
                }
                else
                {
                    return BadRequest("Ekleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Veritabanı hatası: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> KatTipiGuncelle(int tvId, int katTipi)
        {
            var tv = await _tvlerService.TGetByIdAsync(tvId);

            if (tv != null)
            {
                if (Enum.IsDefined(typeof(KatTipi), katTipi))
                {
                    tv.KatTipi = (KatTipi)katTipi;
                    var updateResult = await _tvlerService.TUpdateAsync(tv);
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
                    return BadRequest("Geçersiz Kat Tipi!");
                }
            }
            else
            {
                return BadRequest("Tv Bilgisi Gelmedi!");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AciklamaGuncelle(int tvId, string aciklama)
        {
            var tv = await _tvlerService.TGetByIdAsync(tvId);

            if (tv == null)
            {
                return BadRequest("TV bulunamadı.");
            }

            tv.Aciklama = aciklama;

            var updateResult = await _tvlerService.TUpdateAsync(tv);

            if (updateResult)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Açıklama güncelleme işlemi başarısız oldu.");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TvEslestir()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> TvleriGetir(int hizmetBinasiId)
        {
            List<TvlerBankolarRequestDto> tvlerBankolarRequestDto = await _tvlerCustomService.GetTvlerBankolarWithSayiAsync(hizmetBinasiId);

            return Json(tvlerBankolarRequestDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> TvBankolarEslesmeyenleriGetir(int tvId, int hizmetBinasiId)
        {
            List<BankolarDto> bankolarDto = await _tvlerCustomService.GetTvBankolarEslesmeyenleriGetirAsync(tvId, hizmetBinasiId);
            return Json(bankolarDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> TvBankolariEslesmisleriGetir(int tvId)
        {
            List<TvBankolarRequestDto> tvBankolarRequestDto = await _tvlerCustomService.GetTvBankolarEslesenleriGetirAsync(tvId);
            return Json(tvBankolarRequestDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> TvBankoEslestirmeYap(int tvId, int bankoId)
        {
            try 
            { 
                var tvBankolariDto = await _tvlerCustomService.GetTvBankolarEslesenleriGetirAsync(tvId);
                var eslesme = tvBankolariDto.FirstOrDefault(p => p.BankoId == bankoId);


                if (eslesme != null)
                {
                    // Var ise güncelle

                    var tvBankolari = new TvBankolariDto
                    {
                        TvBankoId = eslesme.TvBankoId,
                        BankoId = bankoId,
                        TvId = tvId,
                        EklenmeTarihi = eslesme.EklenmeTarihi,
                        DuzenlenmeTarihi = DateTime.Now
                    };

                    var updateResult = await _tvlerBankolariService.TUpdateAsync(tvBankolari);

                    if (updateResult)
                    {
                        return Json(new { islemDurum = 1, mesaj = "Tv ve Banko Eşleştirildi" });
                    }
                    else
                    {
                        return Json(new { islemDurum = 0, mesaj = "Tv ve Banko Eşleştirilemedi!" });
                    }
                }
                else
                {
                    // yok ise kaydet
                    var tvBankolari = new TvBankolariDto
                    {
                        BankoId = bankoId,
                        TvId = tvId,
                        EklenmeTarihi = DateTime.Now,
                        DuzenlenmeTarihi = DateTime.Now
                    };
                    var insertResult = await _tvlerBankolariService.TInsertAsync(tvBankolari);

                    if (insertResult.IsSuccess)
                    {
                        return Json(new { islemDurum = 1, mesaj = "Tv ve Banko Eşleştirildi" });
                    }
                    else
                    {
                        return Json(new { islemDurum = 0, mesaj = "Tv ve Banko Eşleştirilemedi!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { islemDurum = 0, mesaj = "Hata: " + ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> TvBankoEslestirmeKaldir(int tvId, int bankoId)
        {
            try
            {
                var tvBankolarRequestDto = await _tvlerCustomService.GetTvBankolarEslesenleriGetirAsync(tvId);
                var eslesme = tvBankolarRequestDto.FirstOrDefault(p => p.BankoId == bankoId);

                // Var ise sil
                if (eslesme != null)
                {
                    var tvBankolariToDelete = await _tvlerBankolariService.TGetByIdAsync(eslesme.TvBankoId);

                    if (tvBankolariToDelete != null)
                    {
                        var deleteResult = await _tvlerBankolariService.TDeleteAsync(tvBankolariToDelete);

                        if (deleteResult)
                        {
                            return Json(new { islemDurum = 1, mesaj = "Tv ve Banko Eşleştirilmesi Kaldırıldı" });
                        }
                        else
                        {
                            return Json(new { islemDurum = 0, mesaj = "Tv ve Banko Eşleştirilmesi Kaldırılamadı!" });
                        }
                    }
                }

                return Json(new { islemDurum = 0, mesaj = "Tv ve Banko Eşleştirilmesi Bulunamamadı!" });
            }
            catch (Exception ex)
            {
                return Json(new { islemDurum = 0, mesaj = "Hata: " + ex.Message });
            }
        }
    }
}
