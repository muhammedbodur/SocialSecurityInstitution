using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ValidationServices
{
    public class PersonelValidationService : IPersonelValidationService
    {
        private readonly IDepartmanlarService _departmanlarService;
        private readonly IServislerService _servislerService;
        private readonly IUnvanlarService _unvanlarService;
        private readonly IAtanmaNedenleriService _atanmaNedenleriService;
        private readonly IHizmetBinalariService _hizmetBinalariService;
        private readonly IIllerService _illerService;
        private readonly IIlcelerService _ilcelerService;
        private readonly ISendikalarService _sendikalarService;

        public PersonelValidationService(
            IDepartmanlarService departmanlarService,
            IServislerService servislerService,
            IUnvanlarService unvanlarService,
            IAtanmaNedenleriService atanmaNedenleriService,
            IHizmetBinalariService hizmetBinalariService,
            IIllerService illerService,
            IIlcelerService ilcelerService,
            ISendikalarService sendikalarService)
        {
            _departmanlarService = departmanlarService;
            _servislerService = servislerService;
            _unvanlarService = unvanlarService;
            _atanmaNedenleriService = atanmaNedenleriService;
            _hizmetBinalariService = hizmetBinalariService;
            _illerService = illerService;
            _ilcelerService = ilcelerService;
            _sendikalarService = sendikalarService;
        }

        public async Task<ValidationResult> ValidatePersonelUpdateAsync(PersonelUpdateDto personelUpdateDto)
        {
            var result = new ValidationResult();

            // 1. Business Rules Validation
            await ValidateBusinessRulesAsync(personelUpdateDto, result);

            // 2. Foreign Key Validation
            var foreignKeyResult = await ValidateForeignKeysAsync(personelUpdateDto);
            result.Errors.AddRange(foreignKeyResult.Errors);

            // 3. Data Integrity Validation
            await ValidateDataIntegrityAsync(personelUpdateDto, result);

            return result;
        }

        public async Task<ValidationResult> ValidateForeignKeysAsync(PersonelUpdateDto personelUpdateDto)
        {
            var result = new ValidationResult();

            // Departman kontrolü
            if (personelUpdateDto.DepartmanId > 0)
            {
                var departman = await _departmanlarService.TGetByIdAsync(personelUpdateDto.DepartmanId);
                if (departman == null)
                {
                    result.Errors.Add(new ValidationError("DepartmanId", "Seçilen departman bulunamadı"));
                }
            }
            else
            {
                result.Errors.Add(new ValidationError("DepartmanId", "Departman seçimi zorunludur"));
            }

            // Servis kontrolü
            if (personelUpdateDto.ServisId > 0)
            {
                var servis = await _servislerService.TGetByIdAsync(personelUpdateDto.ServisId);
                if (servis == null)
                {
                    result.Errors.Add(new ValidationError("ServisId", "Seçilen servis bulunamadı"));
                }
            }
            else
            {
                result.Errors.Add(new ValidationError("ServisId", "Servis seçimi zorunludur"));
            }

            // Ünvan kontrolü
            if (personelUpdateDto.UnvanId > 0)
            {
                var unvan = await _unvanlarService.TGetByIdAsync(personelUpdateDto.UnvanId);
                if (unvan == null)
                {
                    result.Errors.Add(new ValidationError("UnvanId", "Seçilen ünvan bulunamadı"));
                }
            }
            else
            {
                result.Errors.Add(new ValidationError("UnvanId", "Ünvan seçimi zorunludur"));
            }

            // Atanma Nedeni kontrolü
            if (personelUpdateDto.AtanmaNedeniId > 0)
            {
                var atanmaNedeni = await _atanmaNedenleriService.TGetByIdAsync(personelUpdateDto.AtanmaNedeniId);
                if (atanmaNedeni == null)
                {
                    result.Errors.Add(new ValidationError("AtanmaNedeniId", "Seçilen atanma nedeni bulunamadı"));
                }
            }

            // Hizmet Binası kontrolü
            if (personelUpdateDto.HizmetBinasiId > 0)
            {
                var hizmetBinasi = await _hizmetBinalariService.TGetByIdAsync(personelUpdateDto.HizmetBinasiId);
                if (hizmetBinasi == null)
                {
                    result.Errors.Add(new ValidationError("HizmetBinasiId", "Seçilen hizmet binası bulunamadı"));
                }
            }

            // İl-İlçe cascade kontrolü
            if (personelUpdateDto.IlId.HasValue && personelUpdateDto.IlId.Value > 0)
            {
                var il = await _illerService.TGetByIdAsync(personelUpdateDto.IlId.Value);
                if (il == null)
                {
                    result.Errors.Add(new ValidationError("IlId", "Seçilen il bulunamadı"));
                }
                else if (personelUpdateDto.IlceId.HasValue && personelUpdateDto.IlceId.Value > 0)
                {
                    var ilce = await _ilcelerService.TGetByIdAsync(personelUpdateDto.IlceId.Value);
                    if (ilce == null)
                    {
                        result.Errors.Add(new ValidationError("IlceId", "Seçilen ilçe bulunamadı"));
                    }
                    // İlçe'nin seçilen ile ait olup olmadığını kontrol et
                    // Bu kontrol için IlcelerService'e ek metot gerekebilir
                }
            }

            // Eşinin İl-İlçe cascade kontrolü
            if (personelUpdateDto.EsininIsIlId.HasValue && personelUpdateDto.EsininIsIlId.Value > 0)
            {
                var esIl = await _illerService.TGetByIdAsync(personelUpdateDto.EsininIsIlId.Value);
                if (esIl == null)
                {
                    result.Errors.Add(new ValidationError("EsininIsIlId", "Seçilen eş iş ili bulunamadı"));
                }
                else if (personelUpdateDto.EsininIsIlceId.HasValue && personelUpdateDto.EsininIsIlceId.Value > 0)
                {
                    var esIlce = await _ilcelerService.TGetByIdAsync(personelUpdateDto.EsininIsIlceId.Value);
                    if (esIlce == null)
                    {
                        result.Errors.Add(new ValidationError("EsininIsIlceId", "Seçilen eş iş ilçesi bulunamadı"));
                    }
                }
            }

            // Sendika kontrolü (optional)
            if (personelUpdateDto.SendikaId.HasValue && personelUpdateDto.SendikaId.Value > 0)
            {
                var sendika = await _sendikalarService.TGetByIdAsync(personelUpdateDto.SendikaId.Value);
                if (sendika == null)
                {
                    result.Errors.Add(new ValidationError("SendikaId", "Seçilen sendika bulunamadı"));
                }
            }

            return result;
        }

        private async Task ValidateBusinessRulesAsync(PersonelUpdateDto personelUpdateDto, ValidationResult result)
        {
            // Business rule: Doğum tarihi kontrolü
            if (personelUpdateDto.DogumTarihi > DateTime.Now.AddYears(-18))
            {
                result.Errors.Add(new ValidationError("DogumTarihi", "Personel en az 18 yaşında olmalıdır"));
            }

            if (personelUpdateDto.DogumTarihi < DateTime.Now.AddYears(-70))
            {
                result.Errors.Add(new ValidationError("DogumTarihi", "Geçersiz doğum tarihi"));
            }

            // Business rule: Kart aktiflik tarihi kontrolü
            if (personelUpdateDto.KartNoAktiflikTarihi.HasValue)
            {
                if (personelUpdateDto.KartNoAktiflikTarihi.Value > DateTime.Now)
                {
                    result.Errors.Add(new ValidationError("KartNoAktiflikTarihi", "Kart aktiflik tarihi gelecek tarih olamaz"));
                }
            }

            // Business rule: Öğrenim süresi kontrolü
            if (personelUpdateDto.OgrenimSuresi > 15)
            {
                result.Errors.Add(new ValidationError("OgrenimSuresi", "Öğrenim süresi 15 yıldan fazla olamaz"));
            }

            // İleride başka business rules eklenebilir
        }

        private async Task ValidateDataIntegrityAsync(PersonelUpdateDto personelUpdateDto, ValidationResult result)
        {
            // Data integrity: İl seçilmişse ilçe de seçilmeli
            if (personelUpdateDto.IlId.HasValue && personelUpdateDto.IlId.Value > 0)
            {
                if (!personelUpdateDto.IlceId.HasValue || personelUpdateDto.IlceId.Value <= 0)
                {
                    result.Errors.Add(new ValidationError("IlceId", "İl seçildikten sonra ilçe seçimi zorunludur"));
                }
            }

            // Data integrity: Eşinin ili seçilmişse ilçesi de seçilmeli
            if (personelUpdateDto.EsininIsIlId.HasValue && personelUpdateDto.EsininIsIlId.Value > 0)
            {
                if (!personelUpdateDto.EsininIsIlceId.HasValue || personelUpdateDto.EsininIsIlceId.Value <= 0)
                {
                    result.Errors.Add(new ValidationError("EsininIsIlceId", "Eş iş ili seçildikten sonra ilçe seçimi zorunludur"));
                }
            }

            // Data integrity: Medeni durumu evli ise eş bilgileri kontrolü
            if (personelUpdateDto.MedeniDurumu == SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums.MedeniDurumu.evli)
            {
                if (string.IsNullOrWhiteSpace(personelUpdateDto.EsininAdi))
                {
                    result.Errors.Add(new ValidationError("EsininAdi", "Medeni durumu evli olan personelin eş adı zorunludur"));
                }
            }
        }
    }
}
