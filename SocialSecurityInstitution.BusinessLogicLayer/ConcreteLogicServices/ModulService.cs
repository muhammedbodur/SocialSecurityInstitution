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
    public class ModulService : IModulService
    {
        private readonly IModulDal _modulDal;

        public ModulService(IModulDal modulDal)
        {
            _modulDal = modulDal;
        }

        public async Task<bool> TContainsAsync(ModulDto dto)
        {
            return await _modulDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _modulDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(ModulDto dto)
        {
            return await _modulDal.DeleteAsync(dto);
        }

        public async Task<List<ModulDto>> TGetAllAsync()
        {
            return await _modulDal.GetAllAsync();
        }

        public async Task<ModulDto> TGetByIdAsync(int id)
        {
            return await _modulDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(ModulDto dto)
        {
            return await _modulDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(ModulDto dto)
        {
            return await _modulDal.UpdateAsync(dto);
        }
    }
}
