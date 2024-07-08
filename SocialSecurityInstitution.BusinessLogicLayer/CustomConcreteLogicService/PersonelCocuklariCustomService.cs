using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelCocuklariCustomService : IPersonelCocuklariCustomService
    {
        private readonly IMapper _mapper;

        public PersonelCocuklariCustomService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PersonelCocuklariDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            using var context = new Context();
            var entity = await context.PersonelCocuklari.FirstOrDefaultAsync(pc => pc.PersonelTcKimlikNo == tcKimlikNo);
            var dto = _mapper.Map<PersonelCocuklariDto>(entity);
            return dto;
        }
    }
}
