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
    public class ServislerService : IServislerService
    {
        private readonly IServislerDal _servislerDal;

        public ServislerService(IServislerDal servislerDal)
        {
            _servislerDal = servislerDal;
        }

        public async Task<bool> TContainsAsync(ServislerDto dto)
        {
            return await _servislerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _servislerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(ServislerDto dto)
        {
            return await _servislerDal.DeleteAsync(dto);
        }

        public async Task<List<ServislerDto>> TGetAllAsync()
        {
            return await _servislerDal.GetAllAsync();
        }

        public async Task<ServislerDto> TGetByIdAsync(int id)
        {
            return await _servislerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(ServislerDto dto)
        {
            return await _servislerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(ServislerDto dto)
        {
            return await _servislerDal.UpdateAsync(dto);
        }
    }
}
