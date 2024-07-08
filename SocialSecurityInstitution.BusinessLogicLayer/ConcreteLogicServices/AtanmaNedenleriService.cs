using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class AtanmaNedenleriService : IAtanmaNedenleriService
    {
        private readonly IAtanmaNedenleriDal _atanmaNedenleriDal;

        public AtanmaNedenleriService(IAtanmaNedenleriDal atanmaNedenleriDal)
        {
            _atanmaNedenleriDal = atanmaNedenleriDal;
        }

        public async Task<bool> TContainsAsync(AtanmaNedenleriDto dto)
        {
            return await _atanmaNedenleriDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _atanmaNedenleriDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(AtanmaNedenleriDto dto)
        {
            return await _atanmaNedenleriDal.DeleteAsync(dto);
        }

        public async Task<List<AtanmaNedenleriDto>> TGetAllAsync()
        {
            return await _atanmaNedenleriDal.GetAllAsync();
        }

        public async Task<AtanmaNedenleriDto> TGetByIdAsync(int id)
        {
            return await _atanmaNedenleriDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(AtanmaNedenleriDto dto)
        {
            return await _atanmaNedenleriDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(AtanmaNedenleriDto dto)
        {
            return await _atanmaNedenleriDal.UpdateAsync(dto);
        }
    }
}
