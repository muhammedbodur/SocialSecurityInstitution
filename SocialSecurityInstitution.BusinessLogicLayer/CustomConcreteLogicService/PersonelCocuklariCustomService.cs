using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly Context _context;

        public PersonelCocuklariCustomService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PersonelCocuklariDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            var entity = await _context.PersonelCocuklari.AsNoTracking().FirstOrDefaultAsync(pc => pc.PersonelTcKimlikNo == tcKimlikNo);
            var dto = _mapper.Map<PersonelCocuklariDto>(entity);
            return dto;
        }
    }
}
