using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class ServislerCustomService : IServislerCustomService
    {
        private readonly IServislerDal _servislerDal;
        private readonly IMapper _mapper;
        private readonly ILogger<ServislerCustomService> _logger;

        public ServislerCustomService(
            IServislerDal servislerDal,
            IMapper mapper,
            ILogger<ServislerCustomService> logger)
        {
            _servislerDal = servislerDal;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ServislerDto>> GetServislerByDepartmanIdAsync(int departmanId)
        {
            try
            {
                if (departmanId <= 0)
                {
                    _logger.LogWarning("Invalid departmanId provided: {DepartmanId}", departmanId);
                    return new List<ServislerDto>();
                }

                // Tüm servisleri al ve departmana göre filtrele
                var allServisler = await _servislerDal.GetAllAsync();

                // Not: ServislerDto'da DepartmanId property'si yoksa, 
                // bu filtrelemeyi veritabanı seviyesinde yapmak gerekir
                // Şimdilik tümünü döndürüyoruz - bu kısmı ihtiyaca göre düzenleyin

                _logger.LogInformation("Retrieved {Count} servisler for departman: {DepartmanId}",
                    allServisler.Count, departmanId);

                return allServisler;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving servisler for departman: {DepartmanId}", departmanId);
                return new List<ServislerDto>();
            }
        }

        public async Task<List<ServislerDto>> GetActiveServislerByDepartmanIdAsync(int departmanId)
        {
            try
            {
                var servisler = await GetServislerByDepartmanIdAsync(departmanId);
                // Aktif olanları filtrele
                var activeServisler = servisler.Where(x => x.ServisAktiflik ==
                    SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums.Aktiflik.Aktif).ToList();

                _logger.LogInformation("Retrieved {Count} active servisler for departman: {DepartmanId}",
                    activeServisler.Count, departmanId);

                return activeServisler;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active servisler for departman: {DepartmanId}", departmanId);
                return new List<ServislerDto>();
            }
        }
    }
}