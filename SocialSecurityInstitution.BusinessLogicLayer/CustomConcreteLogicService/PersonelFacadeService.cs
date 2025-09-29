using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.ValidationServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelFacadeService : IPersonelFacadeService
    {
        private readonly IMapper _mapper;
        private readonly IPersonelCustomService _personelCustomService;
        private readonly IPersonellerService _personellerService;
        private readonly IDepartmanlarService _departmanlarService;
        private readonly IServislerService _servislerService;
        private readonly IUnvanlarService _unvanlarService;
        private readonly IHizmetBinalariService _hizmetBinalariService;
        private readonly IIllerService _illerService;
        private readonly IIlcelerService _ilcelerService;
        private readonly ISendikalarService _sendikalarService;
        private readonly ILogger<PersonelFacadeService> _logger;
        private readonly IAtanmaNedenleriService _atanmaNedenleriService;
        private readonly IIlcelerCustomService _ilcelerCustomService;
        private readonly IServislerCustomService _servislerCustomService;
        private readonly IPersonelValidationService _personelValidationService;

        public PersonelFacadeService(
            IMapper mapper,
            IPersonelCustomService personelCustomService,
            IPersonellerService personellerService,
            IDepartmanlarService departmanlarService,
            IServislerService servislerService,
            IUnvanlarService unvanlarService,
            IHizmetBinalariService hizmetBinalariService,
            IIllerService illerService,
            IIlcelerService ilcelerService,
            ISendikalarService sendikalarService,
            IAtanmaNedenleriService atanmaNedenleriService,
            IIlcelerCustomService ilcelerCustomService,
            IServislerCustomService servislerCustomService,
            IPersonelValidationService personelValidationService,
            ILogger<PersonelFacadeService> logger)
        {
            _mapper = mapper;
            _personelCustomService = personelCustomService;
            _personellerService = personellerService;
            _departmanlarService = departmanlarService;
            _servislerService = servislerService;
            _unvanlarService = unvanlarService;
            _hizmetBinalariService = hizmetBinalariService;
            _illerService = illerService;
            _ilcelerService = ilcelerService;
            _sendikalarService = sendikalarService;
            _atanmaNedenleriService = atanmaNedenleriService;
            _ilcelerCustomService = ilcelerCustomService;
            _servislerCustomService = servislerCustomService;
            _logger = logger;
            _personelValidationService = personelValidationService;
        }

        #region Personel CRUD Operations
        public async Task<(bool Success, string Message)> CreatePersonelAsync(PersonellerDto personellerDto)
        {
            try
            {
                if (personellerDto == null)
                {
                    return (false, "Personel bilgisi boş olamaz");
                }

                if (string.IsNullOrWhiteSpace(personellerDto.TcKimlikNo))
                {
                    return (false, "TC Kimlik No boş olamaz");
                }

                personellerDto.PassWord = personellerDto.TcKimlikNo;
                personellerDto.EklenmeTarihi = DateTime.Now;
                personellerDto.DuzenlenmeTarihi = DateTime.Now;

                var insertResult = await _personellerService.TInsertAsync(personellerDto);

                if (insertResult.IsSuccess)
                {
                    _logger.LogInformation("Personel created successfully with TC: {TcKimlikNo}", personellerDto.TcKimlikNo);
                    return (true, "Personel Ekleme İşlemi Başarılı");
                }
                else
                {
                    _logger.LogWarning("Failed to create personel with TC: {TcKimlikNo}", personellerDto.TcKimlikNo);
                    return (false, "Personel eklenirken bir hata oluştu");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating personel");
                return (false, "Hata: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> UpdatePersonelAsync(PersonellerViewDto personellerViewDto)
        {
            try
            {
                if (personellerViewDto == null)
                {
                    return (false, "Personel bilgisi boş olamaz");
                }

                if (string.IsNullOrWhiteSpace(personellerViewDto.TcKimlikNo))
                {
                    return (false, "TC Kimlik No boş olamaz");
                }

                // 1. ViewDto'yu UpdateDto'ya çevir
                var personelUpdateDto = _mapper.Map<PersonelUpdateDto>(personellerViewDto);

                // 2. Validation kontrolleri
                var validationResult = await _personelValidationService.ValidatePersonelUpdateAsync(personelUpdateDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return (false, $"Validation Error: {errorMessages}");
                }

                // 3. Mevcut entity'yi getir (Partial Update için)
                var existingPersonel = await _personelCustomService.TGetByTcKimlikNoAsync(personellerViewDto.TcKimlikNo);
                if (existingPersonel == null)
                {
                    return (false, "Güncellenmek istenen personel bulunamadı");
                }

                // 4. Partial Update: Sadece değişen alanları güncelle
                var updatedPersonelDto = await ApplyPartialUpdateAsync(existingPersonel, personelUpdateDto);

                Console.WriteLine("Güncelleme İşlemi Başlangıç - Partial Update");
                var serializedPersonel = JsonSerializer.Serialize(updatedPersonelDto, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(serializedPersonel);

                // 5. Database'e kaydet
                var updateResult = await _personellerService.TUpdateAsync(updatedPersonelDto);

                if (updateResult)
                {
                    _logger.LogInformation("Personel updated successfully with TC: {TcKimlikNo}", personellerViewDto.TcKimlikNo);
                    return (true, "Personel Güncelleme İşlemi Başarılı");
                }
                else
                {
                    _logger.LogWarning("Failed to update personel with TC: {TcKimlikNo}", personellerViewDto.TcKimlikNo);
                    return (false, "Personel Güncelleme İşlemi Başarısız");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating personel with TC: {TcKimlikNo}", personellerViewDto?.TcKimlikNo);
                return (false, "Hata: " + ex.Message);
            }
        }

        /// <summary>
        /// Partial Update: Sadece değişen alanları günceller, korumalı alanları olduğu gibi bırakır
        /// </summary>
        private async Task<PersonellerDto> ApplyPartialUpdateAsync(PersonellerDto existingPersonel, PersonelUpdateDto updateData)
        {
            // Mevcut personelin bir kopyasını oluştur
            var updatedPersonel = new PersonellerDto
            {
                // KORUNMASI GEREKEN ALANLAR - DEĞİŞTİRİLMEZ
                TcKimlikNo = existingPersonel.TcKimlikNo,
                PassWord = existingPersonel.PassWord,
                SessionID = existingPersonel.SessionID,
                EklenmeTarihi = existingPersonel.EklenmeTarihi,
                NickName = existingPersonel.NickName,
                PersonelKayitNo = existingPersonel.PersonelKayitNo,

                // Navigation property'leri koru
                BankolarKullanici = existingPersonel.BankolarKullanici,
                KanalPersonelleri = existingPersonel.KanalPersonelleri,

                // GÜNCELLENEBİLİR ALANLAR
                AdSoyad = updateData.AdSoyad,
                SicilNo = updateData.SicilNo,
                DepartmanId = updateData.DepartmanId,
                ServisId = updateData.ServisId,
                UnvanId = updateData.UnvanId,
                Gorev = updateData.Gorev,
                Uzmanlik = updateData.Uzmanlik,
                AtanmaNedeniId = updateData.AtanmaNedeniId,
                HizmetBinasiId = updateData.HizmetBinasiId,
                PersonelAktiflikDurum = updateData.PersonelAktiflikDurum,

                // İletişim bilgileri
                Email = updateData.Email,
                Dahili = updateData.Dahili,
                CepTelefonu = updateData.CepTelefonu,
                CepTelefonu2 = updateData.CepTelefonu2,
                EvTelefonu = updateData.EvTelefonu,
                Adres = updateData.Adres,
                Semt = updateData.Semt,

                // Kişisel bilgiler
                DogumTarihi = updateData.DogumTarihi,
                Cinsiyet = updateData.Cinsiyet,
                MedeniDurumu = updateData.MedeniDurumu,
                KanGrubu = updateData.KanGrubu,
                EvDurumu = updateData.EvDurumu,
                UlasimServis1 = updateData.UlasimServis1,
                UlasimServis2 = updateData.UlasimServis2,
                Tabldot = updateData.Tabldot,
                KartNo = updateData.KartNo,
                KartNoAktiflikTarihi = updateData.KartNoAktiflikTarihi ?? existingPersonel.KartNoAktiflikTarihi,

                // Kart bilgilerinden korunanlar
                KartNoDuzenlenmeTarihi = existingPersonel.KartNoDuzenlenmeTarihi,
                KartNoGonderimTarihi = existingPersonel.KartNoGonderimTarihi,
                KartGonderimIslemBasari = existingPersonel.KartGonderimIslemBasari,

                // PersonelTipi korunur (genelde değiştirilmez)
                PersonelTipi = existingPersonel.PersonelTipi,

                // Özlük bilgileri
                EmekliSicilNo = updateData.EmekliSicilNo,
                OgrenimDurumu = updateData.OgrenimDurumu,
                BitirdigiOkul = updateData.BitirdigiOkul,
                BitirdigiBolum = updateData.BitirdigiBolum,
                OgrenimSuresi = updateData.OgrenimSuresi,
                Bransi = updateData.Bransi,
                SehitYakinligi = updateData.SehitYakinligi,

                // Eş bilgileri
                EsininAdi = updateData.EsininAdi,
                EsininIsDurumu = updateData.EsininIsDurumu,
                EsininUnvani = updateData.EsininUnvani,
                EsininIsAdresi = updateData.EsininIsAdresi,
                EsininIsSemt = updateData.EsininIsSemt,

                // Diğer bilgiler
                HizmetBilgisi = updateData.HizmetBilgisi,
                EgitimBilgisi = updateData.EgitimBilgisi,
                ImzaYetkileri = updateData.ImzaYetkileri,
                CezaBilgileri = updateData.CezaBilgileri,
                EngelBilgileri = updateData.EngelBilgileri,
                Resim = updateData.Resim ?? existingPersonel.Resim,

                // Foreign Key ID'leri - Null kontrolü ile
                IlId = updateData.IlId ?? 0,
                IlceId = updateData.IlceId ?? 0,
                SendikaId = updateData.SendikaId ?? 0,
                EsininIsIlId = updateData.EsininIsIlId ?? 0,
                EsininIsIlceId = updateData.EsininIsIlceId ?? 0,

                // Navigation property'leri null olarak bırak (AutoMapper ignore eder)
                Il = null,
                Ilce = null,
                Sendika = null,
                EsininIsIl = null,
                EsininIsIlce = null,

                // Güncelleme tarihi
                DuzenlenmeTarihi = DateTime.Now
            };

            return updatedPersonel;
        }

        public async Task<(bool Success, string Message)> DeletePersonelAsync(string tcKimlikNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    return (false, "TC Kimlik No boş olamaz");
                }

                var personelDto = await _personelCustomService.TGetByTcKimlikNoAsync(tcKimlikNo);
                if (personelDto == null)
                {
                    return (false, "Personel bulunamadı");
                }

                var deleteResult = await _personellerService.TDeleteAsync(personelDto);

                if (deleteResult)
                {
                    _logger.LogInformation("Personel deleted successfully with TC: {TcKimlikNo}", tcKimlikNo);
                    return (true, "Personel Silme İşlemi Başarılı");
                }
                else
                {
                    _logger.LogWarning("Failed to delete personel with TC: {TcKimlikNo}", tcKimlikNo);
                    return (false, "Personel Silme İşlemi Başarısız");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting personel with TC: {TcKimlikNo}", tcKimlikNo);
                return (false, "Hata: " + ex.Message);
            }
        }
        #endregion

        #region Personel Retrieval with Business Logic
        public async Task<PersonellerViewDto?> GetPersonelForEditAsync(string tcKimlikNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    _logger.LogWarning("GetPersonelForEdit called with empty TcKimlikNo");
                    return null;
                }

                // Artık AutoMapper yok - direkt projection geliyor
                var viewModel = await _personelCustomService.GetPersonelViewForEditAsync(tcKimlikNo);

                if (viewModel == null)
                {
                    _logger.LogWarning("Personel not found with TC: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                // Sadece dropdown'ları doldur
                await PopulateDropdownListsAsync(viewModel);

                _logger.LogInformation("Personel retrieved for edit with TC: {TcKimlikNo}", tcKimlikNo);
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personel for edit with TC: {TcKimlikNo}", tcKimlikNo);
                return null;
            }
        }

        public async Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync()
        {
            try
            {
                var personelList = await _personelCustomService.GetPersonellerWithDetailsAsync();

                _logger.LogInformation("Retrieved {Count} personel with details", personelList.Count);
                return personelList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personeller with details");
                return new List<PersonelRequestDto>();
            }
        }
        #endregion

        #region Business Logic Methods
        public async Task<PersonellerViewDto> CreateEmptyPersonelViewDtoWithDropdownsAsync()
        {
            try
            {
                var viewModel = new PersonellerViewDto
                {
                    TcKimlikNo = string.Empty,
                    AdSoyad = string.Empty,
                    DuzenlenmeTarihi = DateTime.Now
                };

                await PopulateDropdownListsAsync(viewModel);

                _logger.LogInformation("Empty PersonellerViewDto created with dropdowns");
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating empty PersonellerViewDto with dropdowns");
                throw;
            }
        }
        #endregion

        #region Advanced Queries (Delegation to PersonelCustomService)
        public async Task<List<PersonellerDto>> GetPersonellerByDepartmanAndHizmetBinasiAsync(int departmanId, int hizmetBinasiId)
        {
            try
            {
                return await _personelCustomService.GetPersonellerDepartmanIdAndHizmetBinasiIdAsync(departmanId, hizmetBinasiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personeller by departman and hizmet binasi");
                return new List<PersonellerDto>();
            }
        }

        public async Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            try
            {
                return await _personelCustomService.GetPersonellerWithHizmetBinasiIdAsync(hizmetBinasiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personeller with hizmet binasi");
                return new List<PersonellerLiteDto>();
            }
        }

        public async Task<List<PersonellerLiteDto>> GetActivePersonelListAsync()
        {
            try
            {
                return await _personelCustomService.GetActivePersonelListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active personel list");
                return new List<PersonellerLiteDto>();
            }
        }

        public async Task<PersonellerDto> UpdateSessionIDAsync(string tcKimlikNo, string newSessionId)
        {
            try
            {
                return await _personelCustomService.UpdateSessionIDAsync(tcKimlikNo, newSessionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating session ID");
                return null;
            }
        }
        #endregion

        public async Task<List<IlcelerDto>> GetIlcelerByIlIdAsync(int ilId)
        {
            try
            {
                return await _ilcelerCustomService.GetIlcelerByIlIdAsync(ilId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ilceler for il: {IlId}", ilId);
                return new List<IlcelerDto>();
            }
        }

        public async Task<List<ServislerDto>> GetServislerByDepartmanIdAsync(int departmanId)
        {
            try
            {
                return await _servislerCustomService.GetActiveServislerByDepartmanIdAsync(departmanId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving servisler for departman: {DepartmanId}", departmanId);
                return new List<ServislerDto>();
            }
        }

        public async Task<List<AtanmaNedenleriDto>> GetAtanmaNedenleriAsync()
        {
            try
            {
                return await _atanmaNedenleriService.TGetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving atanma nedenleri");
                return new List<AtanmaNedenleriDto>();
            }
        }

        public async Task<PersonellerViewDto> PopulateDropdownListsAsync(PersonellerViewDto model)
        {
            try
            {
                // 1. Tüm veritabanı sorgularını eş zamansız olarak başlatın.
                // Bu, sorguların paralel olarak çalışmaya başlamasını sağlar.
                model.Departmanlar = await _departmanlarService.TGetAllAsync();
                model.Servisler = await _servislerService.TGetAllAsync();
                model.Unvanlar = await _unvanlarService.TGetAllAsync();
                model.Iller = await _illerService.TGetAllAsync();
                model.Ilceler = await _ilcelerService.TGetAllAsync();
                model.Sendikalar = await _sendikalarService.TGetAllAsync();
                model.AtanmaNedenleri = await _atanmaNedenleriService.TGetAllAsync();
                model.HizmetBinalari = await _hizmetBinalariService.TGetAllAsync();

                // 2. Null kontrolü yapın.
                model.Departmanlar ??= new List<DepartmanlarDto>();
                model.Servisler ??= new List<ServislerDto>();
                model.Unvanlar ??= new List<UnvanlarDto>();
                model.Iller ??= new List<IllerDto>();
                model.Ilceler ??= new List<IlcelerDto>();
                model.Sendikalar ??= new List<SendikalarDto>();
                model.AtanmaNedenleri ??= new List<AtanmaNedenleriDto>();
                model.HizmetBinalari ??= new List<HizmetBinalariDto>();

                _logger.LogInformation("Dropdown lists populated successfully. Counts: " +
                    $"Departmanlar: {model.Departmanlar.Count}, " +
                    $"Servisler: {model.Servisler.Count}, " +
                    $"Unvanlar: {model.Unvanlar.Count}, " +
                    $"Iller: {model.Iller.Count}, " +
                    $"Ilceler: {model.Ilceler.Count}, " +
                    $"Sendikalar: {model.Sendikalar.Count}, " +
                    $"AtanmaNedenleri: {model.AtanmaNedenleri.Count}, " +
                    $"HizmetBinalari: {model.HizmetBinalari.Count}");

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating dropdown lists");
                throw;
            }
        }
    }
}