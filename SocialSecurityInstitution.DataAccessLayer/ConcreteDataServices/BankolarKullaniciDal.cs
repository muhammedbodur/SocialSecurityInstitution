using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class BankolarKullaniciDal : GenericRepository<BankolarKullanici, BankolarKullaniciDto>, IBankolarKullaniciDal
    {
        public BankolarKullaniciDal(Context context, IMapper mapper, ILogService logService)
            : base(context, mapper, logService)
        {
        }

        public async Task<BankolarKullaniciDto> GetBankolarKullaniciByBankoIdAsync(int bankoId)
        {
            var bankoKullanici = await _context.BankolarKullanici
                .Where(bk => bk.BankoId == bankoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return _mapper.Map<BankolarKullaniciDto>(bankoKullanici);
        }

        public async Task<BankolarKullaniciDto> GetBankolarKullaniciWithDetailsByTcKimlikNoAsync(string tcKimlikNo)
        {
            var bankoKullanici = await _context.BankolarKullanici
                .Include(bk => bk.Bankolar)
                    .ThenInclude(b => b.HizmetBinalari)
                        .ThenInclude(hb => hb.Departman)
                .Include(bk => bk.Personel)
                    .ThenInclude(p => p.Departman)
                .Include(bk => bk.Personel)
                    .ThenInclude(p => p.HizmetBinasi)
                .Where(bk => bk.TcKimlikNo == tcKimlikNo)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return _mapper.Map<BankolarKullaniciDto>(bankoKullanici);
        }

        public async Task<List<BankolarKullaniciDto>> GetActiveBankolarKullaniciByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var bankoKullanicilari = await _context.BankolarKullanici
                .Include(bk => bk.Bankolar)
                .Include(bk => bk.Personel)
                .Where(bk => bk.Bankolar.HizmetBinasiId == hizmetBinasiId &&
                           bk.Bankolar.BankoAktiflik == Enums.Aktiflik.Aktif &&
                           bk.Personel.PersonelAktiflikDurum == Enums.PersonelAktiflikDurum.Aktif)
                .OrderBy(bk => bk.Bankolar.BankoNo)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<BankolarKullaniciDto>>(bankoKullanicilari);
        }

        public async Task<bool> IsTcKimlikNoAssignedToBankoAsync(string tcKimlikNo)
        {
            return await _context.BankolarKullanici
                .AnyAsync(bk => bk.TcKimlikNo == tcKimlikNo);
        }

        public async Task<List<BankolarKullaniciDto>> GetBankolarByTcKimlikNoAsync(string tcKimlikNo)
        {
            var bankoKullanicilari = await _context.BankolarKullanici
                .Include(bk => bk.Bankolar)
                    .ThenInclude(b => b.HizmetBinalari)
                .Where(bk => bk.TcKimlikNo == tcKimlikNo)
                .OrderBy(bk => bk.Bankolar.BankoNo)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<BankolarKullaniciDto>>(bankoKullanicilari);
        }
    }
}