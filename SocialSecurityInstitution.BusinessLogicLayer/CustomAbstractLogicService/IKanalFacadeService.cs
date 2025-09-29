using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IKanalFacadeService
    {
        // Kanal Management
        Task<(bool Success, string Message)> UpdateKanalAsync(int kanalId, string kanalAdi);
        Task<(bool Success, string Message, int KanalId)> CreateKanalAsync(string kanalAdi);
        Task<(bool Success, string Message)> DeleteKanalAsync(int kanalId);

        // Kanal Alt Management
        Task<(bool Success, string Message)> UpdateKanalAltAsync(int kanalAltId, string kanalAltAdi, int kanalId);
        Task<(bool Success, string Message, int KanalAltId)> CreateKanalAltAsync(string kanalAltAdi, int kanalId);
        Task<(bool Success, string Message)> DeleteKanalAltAsync(int kanalAltId);

        // Kanal İşlemleri Management
        Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriAsync(int kanalId);
        Task<(bool Success, string Message, int KanalIslemId)> CreateKanalIslemAsync(string kanalIslemAdi, int kanalId);
        Task<(bool Success, string Message, KanalIslemleriRequestDto? Data)> CreateKanalIslemWithRangeAsync(int hizmetBinasiId, int kanalId, int kanalSayiAralikBaslangic, int kanalSayiAralikBitis);
        Task<(bool Success, string Message)> UpdateKanalIslemAsync(int kanalIslemId, string kanalIslemAdi);
        Task<(bool Success, string Message, string AktiflikDurum)> ToggleKanalIslemAktiflikAsync(int kanalIslemId);
        Task<(bool Success, string Message)> DeleteKanalIslemAsync(int kanalIslemId);

        // Kanal Alt İşlemleri Management
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriAsync(int kanalIslemId);
        Task<(bool Success, string Message, int KanalAltIslemId)> CreateKanalAltIslemAsync(string kanalAltIslemAdi, int kanalIslemId);
        Task<(bool Success, string Message)> UpdateKanalAltIslemAsync(int kanalAltIslemId, string kanalAltIslemAdi);
        Task<(bool Success, string Message, string AktiflikDurum)> ToggleKanalAltIslemAktiflikAsync(int kanalAltIslemId);
        Task<(bool Success, string Message)> DeleteKanalAltIslemAsync(int kanalAltIslemId);

        // Personel Eşleştirme Business Logic
        Task<(bool Success, string Message)> PersonelAltKanalEslestirmeYapAsync(string tcKimlikNo, int kanalAltIslemId, int uzmanlikSeviye);
        Task<(bool Success, string Message)> PersonelAltKanalEslestirmeKaldirAsync(string tcKimlikNo, int kanalAltIslemId);

        // Kanal Eşleştirme Business Logic
        Task<List<KanalAltIslemleriEslestirmeSayisiRequestDto>> GetKanallarListesiAsync(int hizmetBinasiId);
        Task<List<KanalAltIslemleriRequestDto>> GetKanalAltKanalEslestirmeleriAsync(int kanalIslemId);
        Task<List<KanalAltIslemleriRequestDto>> GetEslestirilmemisKanalAltKanallariAsync(int hizmetBinasiId);
        Task<(bool Success, string Message)> KanalAltKanalEslestirmeYapAsync(int kanalIslemId, int kanalAltIslemId);
        Task<(bool Success, string Message)> KanalAltKanalEslestirmeKaldirAsync(int kanalIslemId, int kanalAltIslemId);

        // Helper Methods
        Task<List<KanalPersonelleriViewRequestDto>> GetKanalAltPersonelleriAsync(int kanalAltIslemId);
        Task<List<PersonelAltKanallariRequestDto>> GetPersonelAltKanallariAsync(string tcKimlikNo);
        Task<List<KanalAltIslemleriRequestDto>> GetPersonelAltKanallarEslesmeyenleriAsync(string tcKimlikNo);
    }
}
