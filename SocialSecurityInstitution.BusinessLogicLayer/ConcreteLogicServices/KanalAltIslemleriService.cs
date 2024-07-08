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
    public class KanalAltIslemleriService : IKanalAltIslemleriService
    {
        private readonly IKanalAltIslemleriDal _kanalAltIslemleriDal;

        public KanalAltIslemleriService(IKanalAltIslemleriDal kanalAltIslemleriDal)
        {
            _kanalAltIslemleriDal = kanalAltIslemleriDal;
        }

        public async Task<bool> TContainsAsync(KanalAltIslemleriDto dto)
        {
            return await _kanalAltIslemleriDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kanalAltIslemleriDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KanalAltIslemleriDto dto)
        {
            return await _kanalAltIslemleriDal.DeleteAsync(dto);
        }

        public async Task<List<KanalAltIslemleriDto>> TGetAllAsync()
        {
            return await _kanalAltIslemleriDal.GetAllAsync();
        }

        public async Task<KanalAltIslemleriDto> TGetByIdAsync(int id)
        {
            return await _kanalAltIslemleriDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KanalAltIslemleriDto dto)
        {
            return await _kanalAltIslemleriDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KanalAltIslemleriDto dto)
        {
            return await _kanalAltIslemleriDal.UpdateAsync(dto);
        }
    }
}
