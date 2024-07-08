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
    public class ModulControllerService : IModulControllerService
    {
        private readonly IModulControllerDal _modulControllerDal;

        public ModulControllerService(IModulControllerDal modulControllerDal)
        {
            _modulControllerDal = modulControllerDal;
        }

        public async Task<bool> TContainsAsync(ModulControllerDto dto)
        {
            return await _modulControllerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _modulControllerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(ModulControllerDto dto)
        {
            return await _modulControllerDal.DeleteAsync(dto);
        }

        public async Task<List<ModulControllerDto>> TGetAllAsync()
        {
            return await _modulControllerDal.GetAllAsync();
        }

        public async Task<ModulControllerDto> TGetByIdAsync(int id)
        {
            return await _modulControllerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(ModulControllerDto dto)
        {
            return await _modulControllerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(ModulControllerDto dto)
        {
            return await _modulControllerDal.UpdateAsync(dto);
        }
    }
}
