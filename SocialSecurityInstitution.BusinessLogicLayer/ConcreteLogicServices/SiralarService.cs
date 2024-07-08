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
    public class SiralarService : ISiralarService
    {
        private readonly ISiralarDal _siralarDal;

        public SiralarService(ISiralarDal siralarDal)
        {
            _siralarDal = siralarDal;
        }

        public async Task<bool> TContainsAsync(SiralarDto dto)
        {
            return await _siralarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _siralarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(SiralarDto dto)
        {
            return await _siralarDal.DeleteAsync(dto);
        }

        public async Task<List<SiralarDto>> TGetAllAsync()
        {
            return await _siralarDal.GetAllAsync();
        }

        public async Task<SiralarDto> TGetByIdAsync(int id)
        {
            return await _siralarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(SiralarDto dto)
        {
            return await _siralarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(SiralarDto dto)
        {
            return await _siralarDal.UpdateAsync(dto);
        }
    }
}
