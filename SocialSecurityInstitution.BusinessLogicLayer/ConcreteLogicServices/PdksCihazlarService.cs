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
    public class PdksCihazlarService : IPdksCihazlarService
    {
        private readonly IPdksCihazlarDal _pdksCihazlarDal;

        public PdksCihazlarService(IPdksCihazlarDal pdksCihazlarDal)
        {
            _pdksCihazlarDal = pdksCihazlarDal;
        }

        public async Task<bool> TContainsAsync(PdksCihazlarDto dto)
        {
            return await _pdksCihazlarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _pdksCihazlarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(PdksCihazlarDto dto)
        {
            return await _pdksCihazlarDal.DeleteAsync(dto);
        }

        public async Task<List<PdksCihazlarDto>> TGetAllAsync()
        {
            return await _pdksCihazlarDal.GetAllAsync();
        }

        public async Task<PdksCihazlarDto> TGetByIdAsync(int id)
        {
            return await _pdksCihazlarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(PdksCihazlarDto dto)
        {
            return await _pdksCihazlarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(PdksCihazlarDto dto)
        {
            return await _pdksCihazlarDal.UpdateAsync(dto);
        }
    }
}
