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
    public class IlcelerService : IIlcelerService
    {
        private readonly IIlcelerDal _ilcelerDal;

        public IlcelerService(IIlcelerDal ilcelerDal)
        {
            _ilcelerDal = ilcelerDal;
        }

        public async Task<bool> TContainsAsync(IlcelerDto dto)
        {
            return await _ilcelerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _ilcelerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(IlcelerDto dto)
        {
            return await _ilcelerDal.DeleteAsync(dto);
        }

        public async Task<List<IlcelerDto>> TGetAllAsync()
        {
            return await _ilcelerDal.GetAllAsync();
        }

        public async Task<IlcelerDto> TGetByIdAsync(int id)
        {
            return await _ilcelerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(IlcelerDto dto)
        {
            return await _ilcelerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(IlcelerDto dto)
        {
            return await _ilcelerDal.UpdateAsync(dto);
        }
    }
}
