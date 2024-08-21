using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class BankolarKullaniciCustomService : IBankolarKullaniciCustomService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public BankolarKullaniciCustomService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BankolarKullaniciDto> GetBankolarKullaniciByBankoIdAsync(int bankoId)
        {
            var bankoKullanici = await _context.BankolarKullanici
                .AsNoTracking().FirstOrDefaultAsync(bk => bk.BankoId == bankoId);

            var bankoKullaniciDto = _mapper.Map<BankolarKullaniciDto>(bankoKullanici);

            return bankoKullaniciDto;
        }
    }
}
