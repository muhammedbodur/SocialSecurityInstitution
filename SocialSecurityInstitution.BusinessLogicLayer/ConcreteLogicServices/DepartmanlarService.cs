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
    public class DepartmanlarService : IDepartmanlarService
    {
        private readonly IDepartmanlarDal _departmanlarDal;

        public DepartmanlarService(IDepartmanlarDal departmanlarDal)
        {
            _departmanlarDal = departmanlarDal;
        }

        public async Task<bool> TContainsAsync(DepartmanlarDto dto)
        {
            return await _departmanlarDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _departmanlarDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(DepartmanlarDto dto)
        {
            return await _departmanlarDal.DeleteAsync(dto);
        }

        public async Task<List<DepartmanlarDto>> TGetAllAsync()
        {
            return await _departmanlarDal.GetAllAsync();
        }

        public async Task<DepartmanlarDto> TGetByIdAsync(int id)
        {
            return await _departmanlarDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(DepartmanlarDto dto)
        {
            return await _departmanlarDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(DepartmanlarDto dto)
        {
            return await _departmanlarDal.UpdateAsync(dto);
        }
    }
}
