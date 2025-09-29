using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class DepartmanlarCustomService : IDepartmanlarCustomService
    {
        private readonly IDepartmanlarService _departmanlarService;
        private readonly ILogger<DepartmanlarCustomService> _logger;

        public DepartmanlarCustomService(
            IDepartmanlarService departmanlarService,
            ILogger<DepartmanlarCustomService> logger)
        {
            _departmanlarService = departmanlarService;
            _logger = logger;
        }

        // ✅ Business logic: Toggle departman aktiflik
        public async Task<(bool Success, string Message, string AktiflikDurum)> ToggleDepartmanAktiflikAsync(int departmanId)
        {
            try
            {
                if (departmanId <= 0)
                {
                    return (false, "Geçersiz departman ID", string.Empty);
                }

                var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);
                if (departmanlarDto == null)
                {
                    return (false, "Departman Bulunamadı!", string.Empty);
                }

                // Business logic: Toggle aktiflik durumu
                var yeniAktiflikDurum = (departmanlarDto.DepartmanAktiflik == Aktiflik.Aktif ? Aktiflik.Pasif : Aktiflik.Aktif);
                departmanlarDto.DepartmanAktiflik = yeniAktiflikDurum;
                departmanlarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _departmanlarService.TUpdateAsync(departmanlarDto);
                if (updateResult)
                {
                    _logger.LogInformation("Departman {DepartmanId} aktiflik durumu {AktiflikDurum} olarak güncellendi", departmanId, yeniAktiflikDurum);
                    return (true, $"{yeniAktiflikDurum} Etme İşlemi Başarılı Oldu", yeniAktiflikDurum.ToString());
                }
                else
                {
                    return (false, $"{yeniAktiflikDurum} Etme İşlemi Başarısız Oldu!", string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling departman aktiflik for ID: {DepartmanId}", departmanId);
                return (false, ex.Message, string.Empty);
            }
        }

        // ✅ Business logic: Update departman
        public async Task<(bool Success, string Message)> UpdateDepartmanAsync(int departmanId, string departmanAdi)
        {
            try
            {
                if (departmanId <= 0 || string.IsNullOrWhiteSpace(departmanAdi))
                {
                    return (false, "Geçersiz parametreler");
                }

                var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);
                if (departmanlarDto == null)
                {
                    return (false, "Departman bulunamadı.");
                }

                // Business logic: Update departman data
                departmanlarDto.DepartmanAdi = departmanAdi.Trim();
                departmanlarDto.DuzenlenmeTarihi = DateTime.Now;

                var updateResult = await _departmanlarService.TUpdateAsync(departmanlarDto);
                if (updateResult)
                {
                    _logger.LogInformation("Departman {DepartmanId} güncellendi: {DepartmanAdi}", departmanId, departmanAdi);
                    return (true, "Departman başarıyla güncellendi");
                }
                else
                {
                    return (false, "Güncelleme işlemi başarısız oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating departman ID: {DepartmanId}", departmanId);
                return (false, ex.Message);
            }
        }

        // ✅ Business logic: Create departman
        public async Task<(bool Success, string Message, int DepartmanId)> CreateDepartmanAsync(string departmanAdi)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(departmanAdi))
                {
                    return (false, "Departman adı boş olamaz", 0);
                }

                // Business logic: Create new departman DTO
                var departmanlarDto = new DepartmanlarDto
                {
                    DepartmanAdi = departmanAdi.Trim(),
                    DuzenlenmeTarihi = DateTime.Now,
                    DepartmanAktiflik = Aktiflik.Aktif
                };

                var insertResult = await _departmanlarService.TInsertAsync(departmanlarDto);
                if (insertResult.IsSuccess)
                {
                    var newDepartmanId = (int)insertResult.LastPrimaryKeyValue;
                    _logger.LogInformation("New departman created with ID: {DepartmanId}, Name: {DepartmanAdi}", newDepartmanId, departmanAdi);
                    return (true, "Departman başarıyla eklendi", newDepartmanId);
                }
                else
                {
                    return (false, "Ekleme işlemi başarısız oldu!", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating departman: {DepartmanAdi}", departmanAdi);
                return (false, ex.Message, 0);
            }
        }

        // ✅ Business logic: Delete departman
        public async Task<(bool Success, string Message)> DeleteDepartmanAsync(int departmanId)
        {
            try
            {
                if (departmanId <= 0)
                {
                    return (false, "Geçersiz departman ID");
                }

                var departmanlarDto = await _departmanlarService.TGetByIdAsync(departmanId);
                if (departmanlarDto == null)
                {
                    return (false, "Departman Bulunamadı!");
                }

                var deleteResult = await _departmanlarService.TDeleteAsync(departmanlarDto);
                if (deleteResult)
                {
                    _logger.LogInformation("Departman deleted with ID: {DepartmanId}", departmanId);
                    return (true, "Departmanı Silme İşlemi Başarılı Oldu");
                }
                else
                {
                    return (false, "Departmanı Silme İşlemi Başarısız Oldu!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting departman with ID: {DepartmanId}", departmanId);
                return (false, ex.Message);
            }
        }
    }
}
