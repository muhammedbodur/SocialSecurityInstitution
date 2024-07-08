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
    public class KanalPersonelleriService : IKanalPersonelleriService
    {
        private readonly IKanalPersonelleriDal _kanalPersonelleriDal;

        public KanalPersonelleriService(IKanalPersonelleriDal kanalPersonelleriDal)
        {
            _kanalPersonelleriDal = kanalPersonelleriDal;
        }

        public async Task<bool> TContainsAsync(KanalPersonelleriDto dto)
        {
            return await _kanalPersonelleriDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kanalPersonelleriDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KanalPersonelleriDto dto)
        {
            return await _kanalPersonelleriDal.DeleteAsync(dto);
        }

        public async Task<List<KanalPersonelleriDto>> TGetAllAsync()
        {
            return await _kanalPersonelleriDal.GetAllAsync();
        }

        public async Task<KanalPersonelleriDto> TGetByIdAsync(int id)
        {
            return await _kanalPersonelleriDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KanalPersonelleriDto dto)
        {
            return await _kanalPersonelleriDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KanalPersonelleriDto dto)
        {
            return await _kanalPersonelleriDal.UpdateAsync(dto);
        }
    }
}
