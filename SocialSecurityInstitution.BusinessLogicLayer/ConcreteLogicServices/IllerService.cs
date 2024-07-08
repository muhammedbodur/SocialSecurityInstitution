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
    public class IllerService : IIllerService
    {
        private readonly IIllerDal _illerDal;

        public IllerService(IIllerDal illerDal)
        {
            _illerDal = illerDal;
        }

        public async Task<bool> TContainsAsync(IllerDto dto)
        {
            return await _illerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _illerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(IllerDto dto)
        {
            return await _illerDal.DeleteAsync(dto);
        }

        public async Task<List<IllerDto>> TGetAllAsync()
        {
            return await _illerDal.GetAllAsync();
        }

        public async Task<IllerDto> TGetByIdAsync(int id)
        {
            return await _illerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(IllerDto dto)
        {
            return await _illerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(IllerDto dto)
        {
            return await _illerDal.UpdateAsync(dto);
        }
    }
}
