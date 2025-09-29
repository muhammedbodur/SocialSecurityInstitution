using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IDepartmanlarCustomService
    {
        Task<(bool Success, string Message, string AktiflikDurum)> ToggleDepartmanAktiflikAsync(int departmanId);
        Task<(bool Success, string Message)> UpdateDepartmanAsync(int departmanId, string departmanAdi);
        Task<(bool Success, string Message, int DepartmanId)> CreateDepartmanAsync(string departmanAdi);
        Task<(bool Success, string Message)> DeleteDepartmanAsync(int departmanId);
    }
}
