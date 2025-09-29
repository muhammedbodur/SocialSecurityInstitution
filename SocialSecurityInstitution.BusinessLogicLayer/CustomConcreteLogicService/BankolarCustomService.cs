using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class BankolarCustomService : IBankolarCustomService
    {
        // ✅ Repository dependencies + required services for business logic
        private readonly IBankolarDal _bankolarDal;
        private readonly IPersonellerDal _personellerDal;
        private readonly IBankolarService _bankolarService;
        private readonly IBankolarKullaniciService _bankolarKullaniciService;
        private readonly IBankolarKullaniciCustomService _bankolarKullaniciCustomService;
        private readonly IHizmetBinalariCustomService _hizmetBinalariCustomService;
        private readonly IMapper _mapper;
        private readonly ILogger<BankolarCustomService> _logger;

        public BankolarCustomService(
            IBankolarDal bankolarDal,
            IPersonellerDal personellerDal,
            IBankolarService bankolarService,
            IBankolarKullaniciService bankolarKullaniciService,
            IBankolarKullaniciCustomService bankolarKullaniciCustomService,
            IHizmetBinalariCustomService hizmetBinalariCustomService,
            IMapper mapper,
            ILogger<BankolarCustomService> logger)
        {
            _bankolarDal = bankolarDal;
            _personellerDal = personellerDal;
            _bankolarService = bankolarService;
            _bankolarKullaniciService = bankolarKullaniciService;
            _bankolarKullaniciCustomService = bankolarKullaniciCustomService;
            _hizmetBinalariCustomService = hizmetBinalariCustomService;
            _mapper = mapper;
            _logger = logger;
        }

        // ✅ Pure business logic - validation + repository call
        public async Task<BankolarRequestDto> GetBankoByIdAsync(int bankoId)
        {
            try
            {
                // Business validation
                if (bankoId <= 0)
                {
                    _logger.LogWarning("Invalid bankoId provided: {BankoId}", bankoId);
                    return null;
                }

                // Repository'den DTO al - mapping repository'de yapılmış
                var result = await _bankolarDal.GetBankoWithDetailsByIdAsync(bankoId);

                if (result == null)
                {
                    _logger.LogWarning("Banko not found with ID: {BankoId}", bankoId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving banko with ID: {BankoId}", bankoId);
                throw;
            }
        }

        // ✅ Business logic: Active bankolar filtering + logging
        public async Task<List<BankolarRequestDto>> GetBankolarWithDetailsAsync()
        {
            try
            {
                // Repository'den hazır DTO'ları al
                var bankolar = await _bankolarDal.GetBankolarWithDetailsAsync();

                // Business logic: İstatistik loglama
                _logger.LogInformation("Retrieved {Count} active bankolar", bankolar.Count);

                // Business logic: Additional filtering (varsa)
                return bankolar.Where(b => IsValidBanko(b)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bankolar with details");
                throw;
            }
        }

        // ✅ Business logic: Personel detail retrieval + validation
        public async Task<PersonellerDto> GetBankoPersonelDetailAsync(string tcKimlikNo)
        {
            try
            {
                // Business validation
                if (string.IsNullOrWhiteSpace(tcKimlikNo) || tcKimlikNo.Length != 11)
                {
                    _logger.LogWarning("Invalid TC Kimlik No provided: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                // Repository'den personel bilgisi al
                var personel = await _personellerDal.GetPersonelWithDetailsByTcKimlikNoAsync(tcKimlikNo);

                if (personel == null)
                {
                    _logger.LogWarning("Personel not found with TC: {TcKimlikNo}", tcKimlikNo);
                }

                return personel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personel detail for TC: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        // ✅ Business delegation - Repository'e yönlendirme
        public async Task<List<DepartmanPersonelleriDto>> GetDeparmanPersonelleriAsync(int bankoId)
        {
            try
            {
                // Business validation
                if (bankoId <= 0)
                {
                    _logger.LogWarning("Invalid bankoId for departman personelleri: {BankoId}", bankoId);
                    return new List<DepartmanPersonelleriDto>();
                }

                // Repository'den direkt al - karmaşık query repository'de
                return await _bankolarDal.GetDepartmanPersonelleriByBankoIdAsync(bankoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving departman personelleri for banko: {BankoId}", bankoId);
                throw;
            }
        }

        // ✅ Business delegation - Repository'e yönlendirme  
        public async Task<List<HizmetBinasiPersonelleriDto>> GetHizmetBinasiPersonelleriAsync(int bankoId)
        {
            try
            {
                // Business validation
                if (bankoId <= 0)
                {
                    _logger.LogWarning("Invalid bankoId for hizmet binasi personelleri: {BankoId}", bankoId);
                    return new List<HizmetBinasiPersonelleriDto>();
                }

                // Repository'den direkt al
                return await _bankolarDal.GetHizmetBinasiPersonelleriByBankoIdAsync(bankoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hizmet binasi personelleri for banko: {BankoId}", bankoId);
                throw;
            }
        }

        // ✅ Business logic: Toggle banko aktiflik with complex workflow
        public async Task<(bool Success, string Message, string AktiflikDurum)> ToggleBankoAktiflikAsync(int bankoId)
        {
            try
            {
                if (bankoId <= 0)
                {
                    return (false, "Geçersiz banko ID", string.Empty);
                }

                var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);
                if (bankolarDto == null)
                {
                    return (false, "Banko bulunamadı", string.Empty);
                }

                // Business logic: Toggle aktiflik durumu
                var yeniAktiflikDurum = (bankolarDto.BankoAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                bankolarDto.BankoAktiflik = yeniAktiflikDurum;
                bankolarDto.DuzenlenmeTarihi = DateTime.Now;

                // Business logic: Pasif yapılırken kullanıcı silme workflow'u
                if (yeniAktiflikDurum == Aktiflik.Pasif)
                {
                    var bankolarKullaniciDto = await _bankolarKullaniciCustomService.GetBankolarKullaniciByBankoIdAsync(bankoId);
                    if (bankolarKullaniciDto != null)
                    {
                        var deleteResult = await _bankolarKullaniciService.TDeleteAsync(bankolarKullaniciDto);
                        if (!deleteResult)
                        {
                            return (false, "Banko kullanıcısı silinemedi", string.Empty);
                        }
                    }
                }

                var updateResult = await _bankolarService.TUpdateAsync(bankolarDto);
                if (updateResult)
                {
                    _logger.LogInformation("Banko {BankoId} aktiflik durumu {AktiflikDurum} olarak güncellendi", bankoId, yeniAktiflikDurum);
                    return (true, $"{yeniAktiflikDurum} Etme İşlemi Başarılı Oldu", yeniAktiflikDurum.ToString());
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu", string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling banko aktiflik for ID: {BankoId}", bankoId);
                return (false, ex.Message, string.Empty);
            }
        }

        // ✅ Business logic: Update banko personel with complex validation
        public async Task<(bool Success, string Message, PersonelRequestDto PersonelData)> UpdateBankoPersonelAsync(int bankoId, string tcKimlikNo)
        {
            try
            {
                if (bankoId <= 0 || string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    return (false, "Geçersiz parametreler", null);
                }

                var personelDto = await GetBankoPersonelDetailAsync(tcKimlikNo);
                var bankolarKullaniciDto = await _bankolarKullaniciCustomService.GetBankolarKullaniciByBankoIdAsync(bankoId);
                var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);

                // Business validation: Personel ve banko aktiflik kontrolü
                if (personelDto == null)
                {
                    return (false, "Personel bulunamadı", null);
                }

                if (bankolarDto?.BankoAktiflik != Aktiflik.Aktif)
                {
                    return (false, "İşlem yapılan banko pasif durumda ve pasif durumdaki bankoya personel atanamaz!", null);
                }

                // Business logic: Kullanıcı güncelleme veya ekleme workflow'u
                if (bankolarKullaniciDto != null)
                {
                    // Güncelleme
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
                        var personelRequestDto = _mapper.Map<PersonelRequestDto>(personelDto);
                        return (true, "İşlem başarıyla tamamlandı.", personelRequestDto);
                    }
                    else
                    {
                        return (false, "Güncelleme işlemi başarısız oldu", null);
                    }
                }
                else
                {
                    // Yeni ekleme
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
                        var personelRequestDto = _mapper.Map<PersonelRequestDto>(personelDto);
                        return (true, "İşlem başarıyla tamamlandı.", personelRequestDto);
                    }
                    else
                    {
                        return (false, "Ekleme işlemi başarısız oldu", null);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating banko personel for BankoId: {BankoId}, TC: {TcKimlikNo}", bankoId, tcKimlikNo);
                return (false, ex.Message, null);
            }
        }

        // ✅ Business logic: Create banko with complex DTO mapping
        public async Task<(bool Success, string Message, BankolarHizmetBinalariDepartmanlarDto BankoData)> CreateBankoAsync(int bankoNo, int hizmetBinasiId, int departmanId)
        {
            try
            {
                if (bankoNo <= 0 || hizmetBinasiId <= 0 || departmanId <= 0)
                {
                    return (false, "Geçersiz parametreler", null);
                }

                // Business validation: Hizmet binası kontrolü
                var hizmetBinalariDepartmanlarDto = await _hizmetBinalariCustomService.GetActiveHizmetBinasiAsync(hizmetBinasiId, departmanId);
                if (hizmetBinalariDepartmanlarDto == null)
                {
                    return (false, "Hizmet Binası Bulunamadı!", null);
                }

                // Business logic: Banko oluşturma
                var bankolarDto = new BankolarDto
                {
                    BankoNo = bankoNo,
                    HizmetBinasiId = hizmetBinasiId,
                    EklenmeTarihi = DateTime.Now,
                    BankoAktiflik = Aktiflik.Aktif
                };

                var insertResult = await _bankolarService.TInsertAsync(bankolarDto);
                if (insertResult.IsSuccess)
                {
                    // Business logic: Complex DTO mapping
                    var bankolarHizmetBinalariDepartmanlarDto = _mapper.Map<BankolarHizmetBinalariDepartmanlarDto>(hizmetBinalariDepartmanlarDto);
                    bankolarHizmetBinalariDepartmanlarDto.BankoId = (int)insertResult.LastPrimaryKeyValue;
                    bankolarHizmetBinalariDepartmanlarDto.BankoNo = bankoNo;
                    bankolarHizmetBinalariDepartmanlarDto.BankoAktiflik = Aktiflik.Aktif;
                    bankolarHizmetBinalariDepartmanlarDto.BankoEklenmeTarihi = DateTime.Now;
                    bankolarHizmetBinalariDepartmanlarDto.BankoDuzenlenmeTarihi = DateTime.Now;

                    _logger.LogInformation("New banko created with ID: {BankoId}, No: {BankoNo}", insertResult.LastPrimaryKeyValue, bankoNo);
                    return (true, "Banko başarıyla eklendi", bankolarHizmetBinalariDepartmanlarDto);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating banko with No: {BankoNo}, HizmetBinasiId: {HizmetBinasiId}", bankoNo, hizmetBinasiId);
                return (false, ex.Message, null);
            }
        }

        // ✅ Business logic: Delete banko
        public async Task<(bool Success, string Message)> DeleteBankoAsync(int bankoId)
        {
            try
            {
                if (bankoId <= 0)
                {
                    return (false, "Geçersiz banko ID");
                }

                var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);
                if (bankolarDto == null)
                {
                    return (false, "Banko Bulunamadı!");
                }

                var deleteResult = await _bankolarService.TDeleteAsync(bankolarDto);
                if (deleteResult)
                {
                    _logger.LogInformation("Banko deleted with ID: {BankoId}", bankoId);
                    return (true, "Banko Silme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Bankoyu Silme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting banko with ID: {BankoId}", bankoId);
                return (false, ex.Message);
            }
        }

        // ✅ Business logic: Update banko kat tipi with enum validation
        public async Task<(bool Success, string Message)> UpdateBankoKatTipiAsync(int bankoId, int katTipi)
        {
            try
            {
                if (bankoId <= 0)
                {
                    return (false, "Geçersiz banko ID");
                }

                var bankolarDto = await _bankolarService.TGetByIdAsync(bankoId);
                if (bankolarDto == null)
                {
                    return (false, "Banko Bilgisi Gelmedi!");
                }

                // Business validation: Enum kontrolü
                if (!Enum.IsDefined(typeof(KatTipi), katTipi))
                {
                    return (false, "Geçersiz Kat Tipi!");
                }

                bankolarDto.KatTipi = (KatTipi)katTipi;
                var updateResult = await _bankolarService.TUpdateAsync(bankolarDto);
                
                if (updateResult)
                {
                    _logger.LogInformation("Banko {BankoId} kat tipi {KatTipi} olarak güncellendi", bankoId, (KatTipi)katTipi);
                    return (true, "Kat tipi başarıyla güncellendi");
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating banko kat tipi for ID: {BankoId}", bankoId);
                return (false, ex.Message);
            }
        }

        // ✅ Private business logic method
        private bool IsValidBanko(BankolarRequestDto banko)
        {
            // Business rules for valid banko
            return banko.BankoAktiflik == Aktiflik.Aktif &&
                   banko.DepartmanAktiflik == Aktiflik.Aktif &&
                   banko.HizmetBinasiAktiflik == Aktiflik.Aktif;
        }
    }
}