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
    public class PersonelYetkileriService : IPersonelYetkileriService
    {
        private readonly IPersonelYetkileriDal _personelYetkileriDal;

        public PersonelYetkileriService(IPersonelYetkileriDal personelYetkileriDal)
        {
            _personelYetkileriDal = personelYetkileriDal;
        }

        public async Task<bool> TContainsAsync(PersonelYetkileriDto dto)
        {
            return await _personelYetkileriDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _personelYetkileriDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(PersonelYetkileriDto dto)
        {
            return await _personelYetkileriDal.DeleteAsync(dto);
        }

        public async Task<List<PersonelYetkileriDto>> TGetAllAsync()
        {
            return await _personelYetkileriDal.GetAllAsync();
        }

        public async Task<PersonelYetkileriDto> TGetByIdAsync(int id)
        {
            return await _personelYetkileriDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(PersonelYetkileriDto dto)
        {
            return await _personelYetkileriDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(PersonelYetkileriDto dto)
        {
            return await _personelYetkileriDal.UpdateAsync(dto);
        }
    }
}
