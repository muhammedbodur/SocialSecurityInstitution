using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class PersonelCocuklariDal : GenericRepository<PersonelCocuklari, PersonelCocuklariDto>, IPersonelCocuklariDal
    {
        public PersonelCocuklariDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<PersonelCocuklariDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            var entity = await _context.PersonelCocuklari
                .AsNoTracking()
                .FirstOrDefaultAsync(pc => pc.PersonelTcKimlikNo == tcKimlikNo);

            return _mapper.Map<PersonelCocuklariDto>(entity);
        }
    }
}
