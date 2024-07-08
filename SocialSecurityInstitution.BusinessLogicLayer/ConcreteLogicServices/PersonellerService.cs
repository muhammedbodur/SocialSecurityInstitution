using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using AutoMapper;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class PersonellerService : IPersonellerService
    {
        private readonly IPersonellerDal _personellerDal;

        public PersonellerService(IPersonellerDal personellerDal)
        {
            _personellerDal = personellerDal;
        }

        public async Task<bool> TContainsAsync(PersonellerDto dto)
        {
            return await _personellerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _personellerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(PersonellerDto dto)
        {
            return await _personellerDal.DeleteAsync(dto);
        }

        public async Task<List<PersonellerDto>> TGetAllAsync()
        {
            return await _personellerDal.GetAllAsync();
        }

        public async Task<PersonellerDto> TGetByIdAsync(int id)
        {
            return await _personellerDal.GetByIdAsync(id);
        }
        
        public async Task<InsertResult> TInsertAsync(PersonellerDto dto)
        {
            return await _personellerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(PersonellerDto dto)
        {
            return await _personellerDal.UpdateAsync(dto);
        }
    }
}
