using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class HubConnectionService : IHubConnectionService
    {
        private readonly IHubConnectionDal _hubConnectionDal;
        private readonly ILogger<HubConnectionService> _logger;

        public HubConnectionService(IHubConnectionDal hubConnectionDal, ILogger<HubConnectionService> logger)
        {
            _hubConnectionDal = hubConnectionDal;
            _logger = logger;
        }

        public async Task<bool> TContainsAsync(HubConnectionDto dto)
        {
            return await _hubConnectionDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _hubConnectionDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(HubConnectionDto dto)
        {
            return await _hubConnectionDal.DeleteAsync(dto);
        }

        public async Task<List<HubConnectionDto>> TGetAllAsync()
        {
            return await _hubConnectionDal.GetAllAsync();
        }

        public async Task<HubConnectionDto> TGetByIdAsync(int id)
        {
            return await _hubConnectionDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(HubConnectionDto dto)
        {
            return await _hubConnectionDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(HubConnectionDto dto)
        {
            return await _hubConnectionDal.UpdateAsync(dto);
        }
    }
}
