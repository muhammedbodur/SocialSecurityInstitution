using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IBankolarCustomService
    {
        Task<List<BankolarRequestDto>> GetBankolarWithDetailsAsync();
        Task<List<DepartmanPersonelleriDto>> GetDeparmanPersonelleriAsync(int bankoId);
        Task<List<HizmetBinasiPersonelleriDto>> GetHizmetBinasiPersonelleriAsync(int bankoId);
        Task<BankolarRequestDto> GetBankoByIdAsync(int bankoId);
        Task<PersonellerDto> GetBankoPersonelDetailAsync(string tcKimlikNo);
        Task<(bool Success, string Message, string AktiflikDurum)> ToggleBankoAktiflikAsync(int bankoId);
        Task<(bool Success, string Message, PersonelRequestDto PersonelData)> UpdateBankoPersonelAsync(int bankoId, string tcKimlikNo);
        Task<(bool Success, string Message, BankolarHizmetBinalariDepartmanlarDto BankoData)> CreateBankoAsync(int bankoNo, int hizmetBinasiId, int departmanId);
        Task<(bool Success, string Message)> DeleteBankoAsync(int bankoId);
        Task<(bool Success, string Message)> UpdateBankoKatTipiAsync(int bankoId, int katTipi);
    }
}
