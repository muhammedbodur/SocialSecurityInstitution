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
    public class IlcelerCustomService : IIlcelerCustomService
    {
        private readonly IIlcelerDal _ilcelerDal;
        private readonly IMapper _mapper;
        private readonly ILogger<IlcelerCustomService> _logger;

        public IlcelerCustomService(
            IIlcelerDal ilcelerDal,
            IMapper mapper,
            ILogger<IlcelerCustomService> logger)
        {
            _ilcelerDal = ilcelerDal;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<IlcelerDto>> GetIlcelerByIlIdAsync(int ilId)
        {
            try
            {
                if (ilId <= 0)
                {
                    _logger.LogWarning("Invalid ilId provided: {IlId}", ilId);
                    return new List<IlcelerDto>();
                }

                // Tüm ilçeleri al ve filtrele
                var allIlceler = await _ilcelerDal.GetAllAsync();
                var filteredIlceler = allIlceler.Where(x => x.Iller?.IlId == ilId).ToList();

                _logger.LogInformation("Retrieved {Count} ilceler for il: {IlId}", filteredIlceler.Count, ilId);

                return filteredIlceler;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ilceler for il: {IlId}", ilId);
                return new List<IlcelerDto>();
            }
        }

        public async Task<List<IlcelerDto>> GetActiveIlcelerByIlIdAsync(int ilId)
        {
            try
            {
                var ilceler = await GetIlcelerByIlIdAsync(ilId);
                // Aktif olanları filtrele (eğer bir aktiflik durumu varsa)
                return ilceler; // Şimdilik hepsini döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active ilceler for il: {IlId}", ilId);
                return new List<IlcelerDto>();
            }
        }
    }
}