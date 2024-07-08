using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
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
    public class SendikalarService : ISendikalarService
    {
        private readonly ISendikalarDal _sendikalarDal;

        public SendikalarService(ISendikalarDal sendikalarDal)
        {
            _sendikalarDal = sendikalarDal;
        }

        public async Task<bool> TContainsAsync(SendikalarDto dto)
        {
            return await _sendikalarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _sendikalarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(SendikalarDto dto)
        {
            return await _sendikalarDal.DeleteAsync(dto);
        }

        public async Task<List<SendikalarDto>> TGetAllAsync()
        {
            return await _sendikalarDal.GetAllAsync();
        }

        public async Task<SendikalarDto> TGetByIdAsync(int id)
        {
            return await _sendikalarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(SendikalarDto dto)
        {
            return await _sendikalarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(SendikalarDto dto)
        {
            return await _sendikalarDal.UpdateAsync(dto);
        }
    }
}
