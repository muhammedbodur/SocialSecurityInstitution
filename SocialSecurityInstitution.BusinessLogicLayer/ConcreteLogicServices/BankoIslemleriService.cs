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
    public class BankoIslemleriService : IBankoIslemleriService
    {
        private readonly IBankoIslemleriDal _bankoIslemleriDal;

        public BankoIslemleriService(IBankoIslemleriDal bankoIslemleriDal)
        {
            _bankoIslemleriDal = bankoIslemleriDal;
        }

        public async Task<bool> TContainsAsync(BankoIslemleriDto dto)
        {
            return await _bankoIslemleriDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _bankoIslemleriDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(BankoIslemleriDto dto)
        {
            return await _bankoIslemleriDal.DeleteAsync(dto);
        }

        public async Task<List<BankoIslemleriDto>> TGetAllAsync()
        {
            return await _bankoIslemleriDal.GetAllAsync();
        }

        public async Task<BankoIslemleriDto> TGetByIdAsync(int id)
        {
            return await _bankoIslemleriDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(BankoIslemleriDto dto)
        {
            return await _bankoIslemleriDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(BankoIslemleriDto dto)
        {
            return await _bankoIslemleriDal.UpdateAsync(dto);
        }
    }
}
