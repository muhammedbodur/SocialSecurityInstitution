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

        public BankolarKullaniciCustomService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<BankolarKullaniciDto> GetBankolarKullaniciByBankoIdAsync(int bankoId)
        {
            using var context = new Context();

            var bankoKullanici = await context.BankolarKullanici
                .FirstOrDefaultAsync(bk => bk.BankoId == bankoId);

            var bankoKullaniciDto = _mapper.Map<BankolarKullaniciDto>(bankoKullanici);

            return bankoKullaniciDto;
        }
    }
}
