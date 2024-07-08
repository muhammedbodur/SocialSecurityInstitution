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
    public class BankolarKullaniciService : IBankolarKullaniciService
    {
        private readonly IBankolarKullaniciDal _bankolarKullaniciDal;

        public BankolarKullaniciService(IBankolarKullaniciDal bankolarKullaniciDal)
        {
            _bankolarKullaniciDal = bankolarKullaniciDal;
        }

        public async Task<bool> TContainsAsync(BankolarKullaniciDto dto)
        {
            return await _bankolarKullaniciDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _bankolarKullaniciDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(BankolarKullaniciDto dto)
        {
            return await _bankolarKullaniciDal.DeleteAsync(dto);
        }

        public async Task<List<BankolarKullaniciDto>> TGetAllAsync()
        {
            return await _bankolarKullaniciDal.GetAllAsync();
        }

        public async Task<BankolarKullaniciDto> TGetByIdAsync(int id)
        {
            return await _bankolarKullaniciDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(BankolarKullaniciDto dto)
        {
            return await _bankolarKullaniciDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(BankolarKullaniciDto dto)
        {
            return await _bankolarKullaniciDal.UpdateAsync(dto);
        }
    }
}
