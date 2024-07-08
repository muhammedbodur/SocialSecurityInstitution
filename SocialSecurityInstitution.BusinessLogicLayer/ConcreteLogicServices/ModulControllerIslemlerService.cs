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
    public class ModulControllerIslemlerService : IModulControllerIslemlerService
    {
        private readonly IModulControllerIslemlerDal _modulControllerIslemlerDal;

        public ModulControllerIslemlerService(IModulControllerIslemlerDal modulControllerIslemlerDal)
        {
            _modulControllerIslemlerDal = modulControllerIslemlerDal;
        }

        public async Task<bool> TContainsAsync(ModulControllerIslemlerDto dto)
        {
            return await _modulControllerIslemlerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _modulControllerIslemlerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(ModulControllerIslemlerDto dto)
        {
            return await _modulControllerIslemlerDal.DeleteAsync(dto);
        }

        public async Task<List<ModulControllerIslemlerDto>> TGetAllAsync()
        {
            return await _modulControllerIslemlerDal.GetAllAsync();
        }

        public async Task<ModulControllerIslemlerDto> TGetByIdAsync(int id)
        {
            return await _modulControllerIslemlerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(ModulControllerIslemlerDto dto)
        {
            return await _modulControllerIslemlerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(ModulControllerIslemlerDto dto)
        {
            return await _modulControllerIslemlerDal.UpdateAsync(dto);
        }
    }
}
