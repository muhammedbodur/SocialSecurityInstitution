using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class HubTvConnectionService : IHubTvConnectionService
    {
        private readonly IHubTvConnectionDal _hubTvConnectionDal;

        public HubTvConnectionService(IHubTvConnectionDal hubTvConnectionDal)
        {
            _hubTvConnectionDal = hubTvConnectionDal;
        }

        public async Task<bool> TContainsAsync(HubTvConnectionDto dto)
        {
            return await _hubTvConnectionDal.UpdateAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _hubTvConnectionDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(HubTvConnectionDto dto)
        {
            return await _hubTvConnectionDal.DeleteAsync(dto);
        }

        public async Task<List<HubTvConnectionDto>> TGetAllAsync()
        {
            return await _hubTvConnectionDal.GetAllAsync();
        }

        public async Task<HubTvConnectionDto> TGetByIdAsync(int id)
        {
            return await _hubTvConnectionDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(HubTvConnectionDto dto)
        {
            return await _hubTvConnectionDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(HubTvConnectionDto dto)
        {
            return await _hubTvConnectionDal.UpdateAsync(dto);
        }
    }
}
