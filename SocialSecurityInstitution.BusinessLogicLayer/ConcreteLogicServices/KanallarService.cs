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
    public class KanallarService : IKanallarService
    {
        private readonly IKanallarDal _kanallarDal;

        public KanallarService(IKanallarDal kanallarDal)
        {
            _kanallarDal = kanallarDal;
        }

        public async Task<bool> TContainsAsync(KanallarDto dto)
        {
            return await _kanallarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kanallarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KanallarDto dto)
        {
            return await _kanallarDal.DeleteAsync(dto);
        }

        public async Task<List<KanallarDto>> TGetAllAsync()
        {
            return await _kanallarDal.GetAllAsync();
        }

        public async Task<KanallarDto> TGetByIdAsync(int id)
        {
            return await _kanallarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KanallarDto dto)
        {
            return await _kanallarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KanallarDto dto)
        {
            return await _kanallarDal.UpdateAsync(dto);
        }
    }
}
