using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class PersonelCocuklariService : IPersonelCocuklariService
    {
        public Task<bool> TContainsAsync(PersonelCocuklariDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> TCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task TDeleteAsync(PersonelCocuklariDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonelCocuklariDto>> TGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PersonelCocuklariDto> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task TInsertAsync(PersonelCocuklariDto entity)
        {
            throw new NotImplementedException();
        }

        public Task TUpdateAsync(PersonelCocuklariDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
