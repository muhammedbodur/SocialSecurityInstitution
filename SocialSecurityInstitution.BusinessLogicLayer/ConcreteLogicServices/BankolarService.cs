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
    public class BankolarService : IBankolarService
    {
        private readonly IBankolarDal _bankolarDal;

        public BankolarService(IBankolarDal bankolarDal)
        {
            _bankolarDal = bankolarDal;
        }

        public async Task<bool> TContainsAsync(BankolarDto dto)
        {
            return await _bankolarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _bankolarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(BankolarDto dto)
        {
            return await _bankolarDal.DeleteAsync(dto);
        }

        public async Task<List<BankolarDto>> TGetAllAsync()
        {
            return await _bankolarDal.GetAllAsync();
        }

        public async Task<BankolarDto> TGetByIdAsync(int id)
        {
            return await _bankolarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(BankolarDto dto)
        {
            return await _bankolarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(BankolarDto dto)
        {
            return await _bankolarDal.UpdateAsync(dto);
        }
    }
}
