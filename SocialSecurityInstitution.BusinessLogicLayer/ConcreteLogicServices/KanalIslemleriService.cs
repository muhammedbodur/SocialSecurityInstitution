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
    public class KanalIslemleriService : IKanalIslemleriService
    {
        private readonly IKanalIslemleriDal _kanalIslemleriDal;

        public KanalIslemleriService(IKanalIslemleriDal kanalIslemleriDal)
        {
            _kanalIslemleriDal = kanalIslemleriDal;
        }

        public async Task<bool> TContainsAsync(KanalIslemleriDto dto)
        {
            return await _kanalIslemleriDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kanalIslemleriDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KanalIslemleriDto dto)
        {
            return await _kanalIslemleriDal.DeleteAsync(dto);
        }

        public async Task<List<KanalIslemleriDto>> TGetAllAsync()
        {
            return await _kanalIslemleriDal.GetAllAsync();
        }

        public async Task<KanalIslemleriDto> TGetByIdAsync(int id)
        {
            return await _kanalIslemleriDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KanalIslemleriDto dto)
        {
            return await _kanalIslemleriDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KanalIslemleriDto dto)
        {
            return await _kanalIslemleriDal.UpdateAsync(dto);
        }
    }
}
