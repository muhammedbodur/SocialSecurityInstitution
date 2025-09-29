using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class KanalFacadeService : IKanalFacadeService
    {
        // ✅ All service dependencies consolidated in Facade
        private readonly IMapper _mapper;
        private readonly IKanallarService _kanallarService;
        private readonly IKanallarAltService _kanallarAltService;
        private readonly IKanalIslemleriService _kanalIslemleriService;
        private readonly IKanalAltIslemleriService _kanalAltIslemleriService;
        private readonly IKanallarCustomService _kanallarCustomService;
        private readonly IPersonellerService _personellerService;
        private readonly IPersonelCustomService _personelCustomService;
        private readonly IKanalPersonelleriService _kanalPersonelleriService;
        private readonly IKanalPersonelleriCustomService _kanalPersonelleriCustomService;
        private readonly ILogger<KanalFacadeService> _logger;

        public KanalFacadeService(
            IMapper mapper,
            IKanallarService kanallarService,
            IKanallarAltService kanallarAltService,
            IKanalIslemleriService kanalIslemleriService,
            IKanalAltIslemleriService kanalAltIslemleriService,
            IKanallarCustomService kanallarCustomService,
            IPersonellerService personellerService,
            IPersonelCustomService personelCustomService,
            IKanalPersonelleriService kanalPersonelleriService,
            IKanalPersonelleriCustomService kanalPersonelleriCustomService,
            ILogger<KanalFacadeService> logger)
        {
            _mapper = mapper;
            _kanallarService = kanallarService;
            _kanallarAltService = kanallarAltService;
            _kanalIslemleriService = kanalIslemleriService;
            _kanalAltIslemleriService = kanalAltIslemleriService;
            _kanallarCustomService = kanallarCustomService;
            _personellerService = personellerService;
            _personelCustomService = personelCustomService;
            _kanalPersonelleriService = kanalPersonelleriService;
            _kanalPersonelleriCustomService = kanalPersonelleriCustomService;
            _logger = logger;
        }

        #region Kanal Management
        // ✅ Business logic: Update kanal
        public async Task<(bool Success, string Message)> UpdateKanalAsync(int kanalId, string kanalAdi)
        {
            try
            {
                if (kanalId <= 0 || string.IsNullOrWhiteSpace(kanalAdi))
                {
                    return (false, "Geçersiz parametreler");
                }

                var kanallarDto = await _kanallarService.TGetByIdAsync(kanalId);
                if (kanallarDto == null)
                {
                    return (false, "Kanal bulunamadı");
                }

                kanallarDto.KanalAdi = kanalAdi.Trim();
                kanallarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanallarService.TUpdateAsync(kanallarDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal {KanalId} güncellendi: {KanalAdi}", kanalId, kanalAdi);
                    return (true, "Kanal başarıyla güncellendi");
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating kanal ID: {KanalId}", kanalId);
                return (false, ex.Message);
            }
        }

        // ✅ Business logic: Create kanal
        public async Task<(bool Success, string Message, int KanalId)> CreateKanalAsync(string kanalAdi)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kanalAdi))
                {
                    return (false, "Kanal adı boş olamaz", 0);
                }

                var kanallarDto = new KanallarDto
                {
                    KanalAdi = kanalAdi.Trim(),
                    EklenmeTarihi = DateTime.Now,
                    DuzenlenmeTarihi = DateTime.Now
                };

                var insertResult = await _kanallarService.TInsertAsync(kanallarDto);
                if (insertResult.IsSuccess)
                {
                    var newKanalId = (int)insertResult.LastPrimaryKeyValue;
                    _logger.LogInformation("New kanal created with ID: {KanalId}, Name: {KanalAdi}", newKanalId, kanalAdi);
                    return (true, "Kanal başarıyla eklendi", newKanalId);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating kanal: {KanalAdi}", kanalAdi);
                return (false, ex.Message, 0);
            }
        }

        // ✅ Business logic: Delete kanal
        public async Task<(bool Success, string Message)> DeleteKanalAsync(int kanalId)
        {
            try
            {
                if (kanalId <= 0)
                {
                    return (false, "Geçersiz kanal ID");
                }

                var kanallarDto = await _kanallarService.TGetByIdAsync(kanalId);
                if (kanallarDto == null)
                {
                    return (false, "Kanal Bulunamadı!");
                }

                var deleteResult = await _kanallarService.TDeleteAsync(kanallarDto);
                if (deleteResult)
                {
                    _logger.LogInformation("Kanal deleted with ID: {KanalId}", kanalId);
                    return (true, "Kanal Silme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Kanal Silme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting kanal with ID: {KanalId}", kanalId);
                return (false, ex.Message);
            }
        }
        #endregion

        #region Kanal Alt Management
        // ✅ Business logic: Update kanal alt
        public async Task<(bool Success, string Message)> UpdateKanalAltAsync(int kanalAltId, string kanalAltAdi, int kanalId)
        {
            try
            {
                if (kanalAltId <= 0 || string.IsNullOrWhiteSpace(kanalAltAdi) || kanalId <= 0)
                {
                    return (false, "Geçersiz parametreler");
                }

                var kanallarAltDto = await _kanallarAltService.TGetByIdAsync(kanalAltId);
                if (kanallarAltDto == null)
                {
                    return (false, "Kanal Alt bulunamadı");
                }

                kanallarAltDto.KanalAltAdi = kanalAltAdi.Trim();
                kanallarAltDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanallarAltService.TUpdateAsync(kanallarAltDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal Alt {KanalAltId} güncellendi: {KanalAltAdi}", kanalAltId, kanalAltAdi);
                    return (true, "Kanal Alt başarıyla güncellendi");
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating kanal alt ID: {KanalAltId}", kanalAltId);
                return (false, ex.Message);
            }
        }

        // ✅ Business logic: Create kanal alt
        public async Task<(bool Success, string Message, int KanalAltId)> CreateKanalAltAsync(string kanalAltAdi, int kanalId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kanalAltAdi) || kanalId <= 0)
                {
                    return (false, "Geçersiz parametreler", 0);
                }

                var kanallarAltDto = new KanallarAltDto
                {
                    KanalAltAdi = kanalAltAdi.Trim(),
                    EklenmeTarihi = DateTime.Now,
                    DuzenlenmeTarihi = DateTime.Now
                };

                var insertResult = await _kanallarAltService.TInsertAsync(kanallarAltDto);
                if (insertResult.IsSuccess)
                {
                    var newKanalAltId = (int)insertResult.LastPrimaryKeyValue;
                    _logger.LogInformation("New kanal alt created with ID: {KanalAltId}, Name: {KanalAltAdi}", newKanalAltId, kanalAltAdi);
                    return (true, "Kanal Alt başarıyla eklendi", newKanalAltId);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating kanal alt: {KanalAltAdi}", kanalAltAdi);
                return (false, ex.Message, 0);
            }
        }

        // ✅ Business logic: Delete kanal alt
        public async Task<(bool Success, string Message)> DeleteKanalAltAsync(int kanalAltId)
        {
            try
            {
                if (kanalAltId <= 0)
                {
                    return (false, "Geçersiz kanal alt ID");
                }

                var kanallarAltDto = await _kanallarAltService.TGetByIdAsync(kanalAltId);
                if (kanallarAltDto == null)
                {
                    return (false, "Kanal Alt Bulunamadı!");
                }

                var deleteResult = await _kanallarAltService.TDeleteAsync(kanallarAltDto);
                if (deleteResult)
                {
                    _logger.LogInformation("Kanal Alt deleted with ID: {KanalAltId}", kanalAltId);
                    return (true, "Kanal Alt Silme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Kanal Alt Silme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting kanal alt with ID: {KanalAltId}", kanalAltId);
                return (false, ex.Message);
            }
        }
        #endregion

        #region Kanal İşlemleri Management
        // ✅ Business delegation: Get kanal işlemleri
        public async Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriAsync(int kanalId)
        {
            try
            {
                if (kanalId <= 0)
                {
                    _logger.LogWarning("Invalid kanalId for kanal işlemleri: {KanalId}", kanalId);
                    return new List<KanalIslemleriRequestDto>();
                }

                return await _kanallarCustomService.GetKanalIslemleriAsync(kanalId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal işlemleri for kanal: {KanalId}", kanalId);
                return new List<KanalIslemleriRequestDto>();
            }
        }

        public async Task<(bool Success, string Message, int KanalIslemId)> CreateKanalIslemAsync(string kanalIslemAdi, int kanalId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kanalIslemAdi) || kanalId <= 0)
                {
                    return (false, "Geçersiz parametreler", 0);
                }

                var kanalIslemleriDto = new KanalIslemleriDto
                {
                    KanalId = kanalId,
                    EklenmeTarihi = DateTime.Now,
                    DuzenlenmeTarihi = DateTime.Now,
                    KanalIslemAktiflik = Aktiflik.Aktif
                };

                var insertResult = await _kanalIslemleriService.TInsertAsync(kanalIslemleriDto);
                if (insertResult.IsSuccess)
                {
                    var newKanalIslemId = (int)insertResult.LastPrimaryKeyValue;
                    _logger.LogInformation("New kanal islem created with ID: {KanalIslemId}, Name: {KanalIslemAdi}", newKanalIslemId, kanalIslemAdi);
                    return (true, "Kanal İşlem başarıyla eklendi", newKanalIslemId);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating kanal islem: {KanalIslemAdi}", kanalIslemAdi);
                return (false, ex.Message, 0);
            }
        }

        // ✅ Business logic: Create kanal işlem with range validation
        public async Task<(bool Success, string Message, KanalIslemleriRequestDto? Data)> CreateKanalIslemWithRangeAsync(int hizmetBinasiId, int kanalId, int kanalSayiAralikBaslangic, int kanalSayiAralikBitis)
        {
            try
            {
                // Business validation: Range validation
                if (hizmetBinasiId <= 0 || kanalId <= 0 || kanalSayiAralikBaslangic <= 0 || kanalSayiAralikBitis <= 0)
                {
                    return (false, "Geçersiz parametreler", null);
                }

                if (kanalSayiAralikBaslangic >= kanalSayiAralikBitis)
                {
                    return (false, "Başlangıç numarası bitiş numarasından küçük olmalı", null);
                }

                // Business logic: Create DTO with validation
                var kanalIslemleriDto = new KanalIslemleriDto
                {
                    HizmetBinasiId = hizmetBinasiId,
                    KanalId = kanalId,
                    BaslangicNumara = kanalSayiAralikBaslangic,
                    BitisNumara = kanalSayiAralikBitis,
                    EklenmeTarihi = DateTime.Now,
                    DuzenlenmeTarihi = DateTime.Now,
                    KanalIslemAktiflik = Aktiflik.Aktif
                };

                var insertResult = await _kanalIslemleriService.TInsertAsync(kanalIslemleriDto);

                if (insertResult.IsSuccess)
                {
                    // Business logic: Get created item details
                    var createdItem = await _kanallarCustomService.GetKanalIslemleriByIdAsync((int)insertResult.LastPrimaryKeyValue);
                    _logger.LogInformation("Kanal İşlem created with ID: {KanalIslemId}, Range: {Start}-{End}", insertResult.LastPrimaryKeyValue, kanalSayiAralikBaslangic, kanalSayiAralikBitis);
                    return (true, "Kanal İşlem başarıyla eklendi", createdItem);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating kanal işlem with range {Start}-{End}", kanalSayiAralikBaslangic, kanalSayiAralikBitis);
                return (false, ex.Message, null);
            }
        }

        public async Task<(bool Success, string Message)> UpdateKanalIslemAsync(int kanalIslemId, string kanalIslemAdi)
        {
            try
            {
                if (kanalIslemId <= 0 || string.IsNullOrWhiteSpace(kanalIslemAdi))
                {
                    return (false, "Geçersiz parametreler");
                }

                var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);
                if (kanalIslemleriDto == null)
                {
                    return (false, "Kanal İşlem bulunamadı");
                }

                kanalIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalIslemleriService.TUpdateAsync(kanalIslemleriDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal İşlem {KanalIslemId} güncellendi: {KanalIslemAdi}", kanalIslemId, kanalIslemAdi);
                    return (true, "Kanal İşlem başarıyla güncellendi");
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating kanal islem ID: {KanalIslemId}", kanalIslemId);
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message, string AktiflikDurum)> ToggleKanalIslemAktiflikAsync(int kanalIslemId)
        {
            try
            {
                if (kanalIslemId <= 0)
                {
                    return (false, "Geçersiz kanal işlem ID", string.Empty);
                }

                var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);
                if (kanalIslemleriDto == null)
                {
                    return (false, "Kanal İşlem bulunamadı!", string.Empty);
                }

                var yeniAktiflikDurum = (kanalIslemleriDto.KanalIslemAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                kanalIslemleriDto.KanalIslemAktiflik = yeniAktiflikDurum;
                kanalIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalIslemleriService.TUpdateAsync(kanalIslemleriDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal İşlem {KanalIslemId} aktiflik durumu değiştirildi: {AktiflikDurum}", kanalIslemId, yeniAktiflikDurum);
                    return (true, $"{yeniAktiflikDurum} Etme İşlemi Başarılı Oldu", yeniAktiflikDurum.ToString());
                }
                else
                {
                    return (false, $"{yeniAktiflikDurum} Etme İşlemi Başarısız Oldu!", string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling kanal islem aktiflik for ID: {KanalIslemId}", kanalIslemId);
                return (false, ex.Message, string.Empty);
            }
        }

        public async Task<(bool Success, string Message)> DeleteKanalIslemAsync(int kanalIslemId)
        {
            try
            {
                if (kanalIslemId <= 0)
                {
                    return (false, "Geçersiz kanal işlem ID");
                }

                var kanalIslemleriDto = await _kanalIslemleriService.TGetByIdAsync(kanalIslemId);
                if (kanalIslemleriDto == null)
                {
                    return (false, "Kanal İşlem Bulunamadı!");
                }

                var deleteResult = await _kanalIslemleriService.TDeleteAsync(kanalIslemleriDto);
                if (deleteResult)
                {
                    _logger.LogInformation("Kanal İşlem deleted with ID: {KanalIslemId}", kanalIslemId);
                    return (true, "Kanal İşlem Silme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Kanal İşlem Silme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting kanal islem with ID: {KanalIslemId}", kanalIslemId);
                return (false, ex.Message);
            }
        }
        #endregion

        #region Kanal Alt İşlemleri Management
        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriAsync(int kanalIslemId)
        {
            try
            {
                if (kanalIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalIslemId for kanal alt işlemleri: {KanalIslemId}", kanalIslemId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                return await _kanallarCustomService.GetKanalAltIslemleriByIslemIdAsync(kanalIslemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt işlemleri for kanal: {KanalIslemId}", kanalIslemId);
                return new List<KanalAltIslemleriRequestDto>();
            }
        }

        public async Task<(bool Success, string Message, int KanalAltIslemId)> CreateKanalAltIslemAsync(string kanalAltIslemAdi, int kanalIslemId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kanalAltIslemAdi) || kanalIslemId <= 0)
                {
                    return (false, "Geçersiz parametreler", 0);
                }

                var kanalAltIslemleriDto = new KanalAltIslemleriDto
                {
                    KanalAltIslemAdi = kanalAltIslemAdi.Trim(),
                    KanalIslemId = kanalIslemId,
                    EklenmeTarihi = DateTime.Now,
                    DuzenlenmeTarihi = DateTime.Now,
                    KanalAltIslemAktiflik = Aktiflik.Aktif
                };

                var insertResult = await _kanalAltIslemleriService.TInsertAsync(kanalAltIslemleriDto);
                if (insertResult.IsSuccess)
                {
                    var newKanalAltIslemId = (int)insertResult.LastPrimaryKeyValue;
                    _logger.LogInformation("New kanal alt islem created with ID: {KanalAltIslemId}, Name: {KanalAltIslemAdi}", newKanalAltIslemId, kanalAltIslemAdi);
                    return (true, "Kanal Alt İşlem başarıyla eklendi", newKanalAltIslemId);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating kanal alt islem: {KanalAltIslemAdi}", kanalAltIslemAdi);
                return (false, ex.Message, 0);
            }
        }

        public async Task<(bool Success, string Message)> UpdateKanalAltIslemAsync(int kanalAltIslemId, string kanalAltIslemAdi)
        {
            try
            {
                if (kanalAltIslemId <= 0 || string.IsNullOrWhiteSpace(kanalAltIslemAdi))
                {
                    return (false, "Geçersiz parametreler");
                }

                var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);
                if (kanalAltIslemleriDto == null)
                {
                    return (false, "Kanal Alt İşlem bulunamadı");
                }

                kanalAltIslemleriDto.KanalAltIslemAdi = kanalAltIslemAdi.Trim();
                kanalAltIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal Alt İşlem {KanalAltIslemId} güncellendi: {KanalAltIslemAdi}", kanalAltIslemId, kanalAltIslemAdi);
                    return (true, "Kanal Alt İşlem başarıyla güncellendi");
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating kanal alt islem ID: {KanalAltIslemId}", kanalAltIslemId);
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message, string AktiflikDurum)> ToggleKanalAltIslemAktiflikAsync(int kanalAltIslemId)
        {
            try
            {
                if (kanalAltIslemId <= 0)
                {
                    return (false, "Geçersiz kanal alt işlem ID", string.Empty);
                }

                var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);
                if (kanalAltIslemleriDto == null)
                {
                    return (false, "Kanal Alt İşlem bulunamadı!", string.Empty);
                }

                var yeniAktiflikDurum = (kanalAltIslemleriDto.KanalAltIslemAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                kanalAltIslemleriDto.KanalAltIslemAktiflik = yeniAktiflikDurum;
                kanalAltIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal Alt İşlem {KanalAltIslemId} aktiflik durumu değiştirildi: {AktiflikDurum}", kanalAltIslemId, yeniAktiflikDurum);
                    return (true, $"{yeniAktiflikDurum} Etme İşlemi Başarılı Oldu", yeniAktiflikDurum.ToString());
                }
                else
                {
                    return (false, $"{yeniAktiflikDurum} Etme İşlemi Başarısız Oldu!", string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling kanal alt islem aktiflik for ID: {KanalAltIslemId}", kanalAltIslemId);
                return (false, ex.Message, string.Empty);
            }
        }

        public async Task<(bool Success, string Message)> DeleteKanalAltIslemAsync(int kanalAltIslemId)
        {
            try
            {
                if (kanalAltIslemId <= 0)
                {
                    return (false, "Geçersiz kanal alt işlem ID");
                }

                var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);
                if (kanalAltIslemleriDto == null)
                {
                    return (false, "Kanal Alt İşlem Bulunamadı!");
                }

                var deleteResult = await _kanalAltIslemleriService.TDeleteAsync(kanalAltIslemleriDto);
                if (deleteResult)
                {
                    _logger.LogInformation("Kanal Alt İşlem deleted with ID: {KanalAltIslemId}", kanalAltIslemId);
                    return (true, "Kanal Alt İşlem Silme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Kanal Alt İşlem Silme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting kanal alt islem with ID: {KanalAltIslemId}", kanalAltIslemId);
                return (false, ex.Message);
            }
        }
        #endregion

        #region Personel Eşleştirme Business Logic
        // ✅ Complex Business logic: Personel alt kanal eşleştirme yap
        public async Task<(bool Success, string Message)> PersonelAltKanalEslestirmeYapAsync(string tcKimlikNo, int kanalAltIslemId, int uzmanlikSeviye)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tcKimlikNo) || kanalAltIslemId <= 0)
                {
                    return (false, "Geçersiz parametreler");
                }

                // Business logic: Personel ve kanal alt işlem ilişkisi var mı kontrol et
                var personelAltKanallari = await _kanalPersonelleriCustomService.GetPersonelAltKanallariAsync(tcKimlikNo);
                var eslesme = personelAltKanallari.FirstOrDefault(p => p.TcKimlikNo == tcKimlikNo && p.KanalAltIslemId == kanalAltIslemId);

                // Business logic: Var ise güncelle, yoksa ekle
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
                        _logger.LogInformation("Personel {TcKimlikNo} kanal alt işlem {KanalAltIslemId} eşleştirmesi güncellendi", tcKimlikNo, kanalAltIslemId);
                        return (true, "Personel ve Alt Kanal Eşleştirildi");
                    }
                    else
                    {
                        return (false, "Personel ve Alt Kanal Eşleştirilemedi!");
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
                        _logger.LogInformation("Personel {TcKimlikNo} kanal alt işlem {KanalAltIslemId} eşleştirmesi oluşturuldu", tcKimlikNo, kanalAltIslemId);
                        return (true, "Personel ve Alt Kanal Eşleştirildi");
                    }
                    else
                    {
                        return (false, "Personel ve Alt Kanal Eşleştirilemedi!");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PersonelAltKanalEslestirmeYap for {TcKimlikNo}, {KanalAltIslemId}", tcKimlikNo, kanalAltIslemId);
                return (false, "Hata: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> PersonelAltKanalEslestirmeKaldirAsync(string tcKimlikNo, int kanalAltIslemId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tcKimlikNo) || kanalAltIslemId <= 0)
                {
                    return (false, "Geçersiz parametreler");
                }

                // Business logic: Personel ve kanal alt işlem ilişkisi var mı kontrol et
                var personelAltKanallari = await _kanalPersonelleriCustomService.GetPersonelAltKanallariAsync(tcKimlikNo);
                var eslesme = personelAltKanallari.FirstOrDefault(p => p.TcKimlikNo == tcKimlikNo && p.KanalAltIslemId == kanalAltIslemId);

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

                    var deleteResult = await _kanalPersonelleriService.TDeleteAsync(kanalPersonelDto);
                    if (deleteResult)
                    {
                        _logger.LogInformation("Personel {TcKimlikNo} kanal alt işlem {KanalAltIslemId} eşleştirmesi kaldırıldı", tcKimlikNo, kanalAltIslemId);
                        return (true, "Personel ve Alt Kanal Eşleştirilmesi Kaldırıldı");
                    }
                    else
                    {
                        return (false, "Personel ve Alt Kanal Eşleştirilmesi Kaldırılamadı!");
                    }
                }
                else
                {
                    return (false, "Personel ve Alt Kanal Eşleştirilmesi Bulunamamadı!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PersonelAltKanalEslestirmeKaldir for {TcKimlikNo}, {KanalAltIslemId}", tcKimlikNo, kanalAltIslemId);
                return (false, "Hata: " + ex.Message);
            }
        }
        #endregion

        #region Kanal Eşleştirme Business Logic
        public async Task<List<KanalAltIslemleriEslestirmeSayisiRequestDto>> GetKanallarListesiAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId for kanallar listesi: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriEslestirmeSayisiRequestDto>();
                }

                return await _kanallarCustomService.GetKanalAltIslemleriEslestirmeSayisiAsync(hizmetBinasiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanallar listesi for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                return new List<KanalAltIslemleriEslestirmeSayisiRequestDto>();
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltKanalEslestirmeleriAsync(int kanalIslemId)
        {
            try
            {
                if (kanalIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalIslemId for kanal alt kanal eşleştirmeleri: {KanalIslemId}", kanalIslemId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                return await _kanallarCustomService.GetKanalAltIslemleriByIslemIdAsync(kanalIslemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt kanal eşleştirmeleri for kanal: {KanalIslemId}", kanalIslemId);
                return new List<KanalAltIslemleriRequestDto>();
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetEslestirilmemisKanalAltKanallariAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId for eşleştirilmemiş kanallar: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                return await _kanallarCustomService.GetKanalAltIslemleriEslestirmeYapilmamisAsync(hizmetBinasiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving eşleştirilmemiş kanal alt kanalları for hizmet binasi: {HizmetBinasiId}", hizmetBinasiId);
                return new List<KanalAltIslemleriRequestDto>();
            }
        }

        public async Task<(bool Success, string Message)> KanalAltKanalEslestirmeYapAsync(int kanalIslemId, int kanalAltIslemId)
        {
            try
            {
                if (kanalIslemId <= 0 || kanalAltIslemId <= 0)
                {
                    return (false, "Geçersiz parametreler");
                }

                var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);
                if (kanalAltIslemleriDto == null)
                {
                    return (false, "Alt Kanal Bulunamadı!");
                }

                kanalAltIslemleriDto.KanalIslemId = kanalIslemId;
                kanalAltIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal alt işlem {KanalAltIslemId} kanal işlem {KanalIslemId} ile eşleştirildi", kanalAltIslemId, kanalIslemId);
                    return (true, "Alt Kanal İşlemleri Eşleştirme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Alt Kanal İşlemleri Eşleştirme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in KanalAltKanalEslestirmeYap for kanal: {KanalIslemId}, alt kanal: {KanalAltIslemId}", kanalIslemId, kanalAltIslemId);
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> KanalAltKanalEslestirmeKaldirAsync(int kanalIslemId, int kanalAltIslemId)
        {
            try
            {
                if (kanalIslemId <= 0 || kanalAltIslemId <= 0)
                {
                    return (false, "Geçersiz parametreler");
                }

                var kanalAltIslemleriDto = await _kanalAltIslemleriService.TGetByIdAsync(kanalAltIslemId);
                if (kanalAltIslemleriDto == null)
                {
                    return (false, "Alt Kanal Bulunamadı!");
                }

                if (kanalAltIslemleriDto.KanalIslemId != kanalIslemId)
                {
                    return (false, "Kanal eşleştirmesi uyumsuz!");
                }

                kanalAltIslemleriDto.KanalIslemId = null;
                kanalAltIslemleriDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _kanalAltIslemleriService.TUpdateAsync(kanalAltIslemleriDto);
                if (updateResult)
                {
                    _logger.LogInformation("Kanal alt işlem {KanalAltIslemId} kanal işlem {KanalIslemId} eşleştirmesi kaldırıldı", kanalAltIslemId, kanalIslemId);
                    return (true, "Alt Kanal İşlemleri Eşleştirme Kaldırma İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Alt Kanal İşlemleri Eşleştirme Kaldırma İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in KanalAltKanalEslestirmeKaldir for kanal: {KanalIslemId}, alt kanal: {KanalAltIslemId}", kanalIslemId, kanalAltIslemId);
                return (false, ex.Message);
            }
        }
        #endregion

        #region Helper Methods
        public async Task<List<KanalPersonelleriViewRequestDto>> GetKanalAltPersonelleriAsync(int kanalAltIslemId)
        {
            try
            {
                if (kanalAltIslemId <= 0)
                {
                    _logger.LogWarning("Invalid kanalAltIslemId for kanal alt personelleri: {KanalAltIslemId}", kanalAltIslemId);
                    return new List<KanalPersonelleriViewRequestDto>();
                }

                return await _kanalPersonelleriCustomService.GetKanalAltPersonelleriAsync(kanalAltIslemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanal alt personelleri for kanal alt: {KanalAltIslemId}", kanalAltIslemId);
                return new List<KanalPersonelleriViewRequestDto>();
            }
        }

        public async Task<List<PersonelAltKanallariRequestDto>> GetPersonelAltKanallariAsync(string tcKimlikNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid tcKimlikNo for personel alt kanalları: {TcKimlikNo}", tcKimlikNo);
                    return new List<PersonelAltKanallariRequestDto>();
                }

                return await _kanalPersonelleriCustomService.GetPersonelAltKanallariAsync(tcKimlikNo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personel alt kanalları for personel: {TcKimlikNo}", tcKimlikNo);
                return new List<PersonelAltKanallariRequestDto>();
            }
        }

        // Helper Methods (hizmetBinasiId parametreli overload ekle)
        public async Task<List<KanalAltIslemleriRequestDto>> GetPersonelAltKanallarEslesmeyenleriAsync(string tcKimlikNo, int hizmetBinasiId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid tcKimlikNo for personel alt kanal eşleşmeyenler: {TcKimlikNo}", tcKimlikNo);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId for personel alt kanal eşleşmeyenler: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<KanalAltIslemleriRequestDto>();
                }

                var eslesmeyenlerDto = await _kanalPersonelleriCustomService.GetPersonelAltKanallarEslesmeyenlerAsync(tcKimlikNo, hizmetBinasiId);

                // AutoMapper ile KanalAltIslemleriDto -> KanalAltIslemleriRequestDto dönüşümü
                var eslesmeyenlerRequestDto = _mapper.Map<List<KanalAltIslemleriRequestDto>>(eslesmeyenlerDto);

                _logger.LogInformation("Retrieved {Count} unmatched kanal alt islemleri for personel: {TcKimlikNo}, hizmet binasi: {HizmetBinasiId}",
                                     eslesmeyenlerRequestDto.Count, tcKimlikNo, hizmetBinasiId);
                return eslesmeyenlerRequestDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personel alt kanal eşleşmeyenler for personel: {TcKimlikNo}, hizmet binasi: {HizmetBinasiId}",
                               tcKimlikNo, hizmetBinasiId);
                return new List<KanalAltIslemleriRequestDto>();
            }
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetPersonelAltKanallarEslesmeyenleriAsync(string tcKimlikNo)
        {
            // Backward compatibility için parametresiz overload
            return await GetPersonelAltKanallarEslesmeyenleriAsync(tcKimlikNo, 0);
        }
        #endregion
    }
}