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
    public class UnvanlarService : IUnvanlarService
    {
        private readonly IUnvanlarDal _unvanlarDal;

        public UnvanlarService(IUnvanlarDal unvanlarDal)
        {
            _unvanlarDal = unvanlarDal;
        }

        public async Task<bool> TContainsAsync(UnvanlarDto dto)
        {
            return await _unvanlarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _unvanlarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(UnvanlarDto dto)
        {
            return await _unvanlarDal.DeleteAsync(dto);
        }

        public async Task<List<UnvanlarDto>> TGetAllAsync()
        {
            return await _unvanlarDal.GetAllAsync();
        }

        public async Task<UnvanlarDto> TGetByIdAsync(int id)
        {
            return await _unvanlarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(UnvanlarDto dto)
        {
            return await _unvanlarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(UnvanlarDto dto)
        {
            return await _unvanlarDal.UpdateAsync(dto);
        }
    }
}
