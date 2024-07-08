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
    public class YetkilerService : IYetkilerService
    {
        private readonly IYetkilerDal _yetkilerDal;

        public YetkilerService(IYetkilerDal yetkilerDal)
        {
            _yetkilerDal = yetkilerDal;
        }

        public async Task<bool> TContainsAsync(YetkilerDto dto)
        {
            return await _yetkilerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _yetkilerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(YetkilerDto dto)
        {
            return await _yetkilerDal.DeleteAsync(dto);
        }

        public async Task<List<YetkilerDto>> TGetAllAsync()
        {
            return await _yetkilerDal.GetAllAsync();
        }

        public async Task<YetkilerDto> TGetByIdAsync(int id)
        {
            return await _yetkilerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(YetkilerDto dto)
        {
            return await _yetkilerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(YetkilerDto dto)
        {
            return await _yetkilerDal.UpdateAsync(dto);
        }
    }
}
