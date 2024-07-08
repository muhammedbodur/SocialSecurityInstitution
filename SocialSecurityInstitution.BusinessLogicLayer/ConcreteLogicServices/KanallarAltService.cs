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
    public class KanallarAltService : IKanallarAltService
    {
        private readonly IKanallarAltDal _kanallarAltDal;

        public KanallarAltService(IKanallarAltDal kanallarAltDal)
        {
            _kanallarAltDal = kanallarAltDal;
        }

        public async Task<bool> TContainsAsync(KanallarAltDto dto)
        {
            return await _kanallarAltDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kanallarAltDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KanallarAltDto dto)
        {
            return await _kanallarAltDal.DeleteAsync(dto);
        }

        public async Task<List<KanallarAltDto>> TGetAllAsync()
        {
            return await _kanallarAltDal.GetAllAsync();
        }

        public async Task<KanallarAltDto> TGetByIdAsync(int id)
        {
            return await _kanallarAltDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KanallarAltDto dto)
        {
            return await _kanallarAltDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KanallarAltDto dto)
        {
            return await _kanallarAltDal.UpdateAsync(dto);
        }
    }
}
