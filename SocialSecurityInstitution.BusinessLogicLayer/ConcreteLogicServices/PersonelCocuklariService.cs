using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class PersonelCocuklariService : IPersonelCocuklariService
    {
        private readonly IPersonelCocuklariDal _personelCocuklariDal;

        public PersonelCocuklariService(IPersonelCocuklariDal personelCocuklariDal)
        {
            _personelCocuklariDal = personelCocuklariDal;
        }

        public async Task<bool> TContainsAsync(PersonelCocuklariDto entity)
        {
            return await _personelCocuklariDal.ContainsAsync(entity);
        }

        public async Task<int> TCountAsync()
        {
            return await _personelCocuklariDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(PersonelCocuklariDto entity)
        {
            return await _personelCocuklariDal.DeleteAsync(entity);
        }

        public async Task<List<PersonelCocuklariDto>> TGetAllAsync()
        {
            return await _personelCocuklariDal.GetAllAsync();
        }

        public async Task<PersonelCocuklariDto> TGetByIdAsync(int id)
        {
            return await _personelCocuklariDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(PersonelCocuklariDto entity)
        {
            return await _personelCocuklariDal.InsertAsync(entity);
        }

        public async Task<bool> TUpdateAsync(PersonelCocuklariDto entity)
        {
            return await _personelCocuklariDal.UpdateAsync(entity);
        }

        
    }
}
