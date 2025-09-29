using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class BankolarDal : GenericRepository<Bankolar, BankolarDto>, IBankolarDal
    {
        public BankolarDal(Context context, IMapper mapper, ILogService logService)
            : base(context, mapper, logService)
        {
        }

        // ✅ Domain-specific queries artık Repository'de
        public async Task<BankolarRequestDto> GetBankoWithDetailsByIdAsync(int bankoId)
        {
            var banko = await _context.Bankolar
                .Include(b => b.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BankoId == bankoId);

            if (banko == null) return null;

            return new BankolarRequestDto
            {
                BankoId = banko.BankoId,
                DepartmanId = banko.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = banko.HizmetBinalari.Departman.DepartmanAdi,
                DepartmanAktiflik = banko.HizmetBinalari.Departman.DepartmanAktiflik,
                HizmetBinasiId = banko.HizmetBinasiId,
                HizmetBinasiAdi = banko.HizmetBinalari.HizmetBinasiAdi,
                HizmetBinasiAktiflik = banko.HizmetBinalari.HizmetBinasiAktiflik,
                BankoNo = banko.BankoNo,
                BankoAktiflik = banko.BankoAktiflik,
                KatTipi = banko.KatTipi,
                BankoEklenmeTarihi = banko.EklenmeTarihi,
                BankoDuzenlenmeTarihi = banko.DuzenlenmeTarihi
            };
        }

        public async Task<List<BankolarRequestDto>> GetBankolarWithDetailsAsync()
        {
            var bankolar = await _context.Bankolar
                .Include(b => b.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .Include(b => b.BankolarKullanici)
                    .ThenInclude(bk => bk.Personel)
                        .ThenInclude(p => p.Departman)
                .Where(b => b.HizmetBinalari.HizmetBinasiAktiflik == Aktiflik.Aktif &&
                            b.HizmetBinalari.Departman.DepartmanAktiflik == Aktiflik.Aktif)
                .OrderBy(b => b.HizmetBinalari.HizmetBinasiId)
                .ThenBy(b => b.BankoNo)
                .AsNoTracking()
                .ToListAsync();

            return bankolar.Select(b =>
            {
                var aktiveBankoKullanici = b.BankolarKullanici
                    .FirstOrDefault(bk => bk.Personel.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif);

                return new BankolarRequestDto
                {
                    BankoId = b.BankoId,
                    BankoNo = b.BankoNo,
                    TcKimlikNo = aktiveBankoKullanici?.Personel?.TcKimlikNo ?? "",
                    SicilNo = aktiveBankoKullanici?.Personel?.SicilNo ?? 0,
                    PersonelAdSoyad = aktiveBankoKullanici?.Personel?.AdSoyad ?? "",
                    PersonelNickName = aktiveBankoKullanici?.Personel?.NickName ?? "",
                    PersonelResim = aktiveBankoKullanici?.Personel?.Resim ?? "empty.png",
                    PersonelDepartmanId = aktiveBankoKullanici?.Personel?.DepartmanId ?? 0,
                    PersonelDepartmanAdi = aktiveBankoKullanici?.Personel?.Departman?.DepartmanAdi ?? "",
                    DepartmanId = b.HizmetBinalari.Departman.DepartmanId,
                    DepartmanAdi = b.HizmetBinalari.Departman.DepartmanAdi,
                    DepartmanAktiflik = b.HizmetBinalari.Departman.DepartmanAktiflik,
                    HizmetBinasiId = b.HizmetBinasiId,
                    HizmetBinasiAdi = b.HizmetBinalari.HizmetBinasiAdi,
                    HizmetBinasiAktiflik = b.HizmetBinalari.HizmetBinasiAktiflik,
                    BankoAktiflik = b.BankoAktiflik,
                    KatTipi = b.KatTipi,
                    BankoEklenmeTarihi = b.EklenmeTarihi,
                    BankoDuzenlenmeTarihi = b.DuzenlenmeTarihi
                };
            }).ToList();
        }

        public async Task<List<DepartmanPersonelleriDto>> GetDepartmanPersonelleriByBankoIdAsync(int bankoId)
        {
            var result = await _context.Bankolar
                .Where(b => b.BankoId == bankoId)
                .Join(_context.HizmetBinalari,
                      b => b.HizmetBinasiId,
                      hb => hb.HizmetBinasiId,
                      (b, hb) => new { Banko = b, HizmetBinasi = hb })
                .Join(_context.Departmanlar,
                      bh => bh.HizmetBinasi.DepartmanId,
                      d => d.DepartmanId,
                      (bh, d) => new { bh.Banko, bh.HizmetBinasi, Departman = d })
                .Join(_context.Personeller,
                      bd => bd.Departman.DepartmanId,
                      p => p.DepartmanId,
                      (bd, p) => new { bd.Banko, bd.HizmetBinasi, bd.Departman, Personel = p })
                .Where(bd => bd.Departman.DepartmanAktiflik == Aktiflik.Aktif &&
                           bd.Personel.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif)
                .Select(bd => new DepartmanPersonelleriDto
                {
                    TcKimlikNo = bd.Personel.TcKimlikNo,
                    SicilNo = bd.Personel.SicilNo,
                    PersonelAdSoyad = bd.Personel.AdSoyad,
                    DepartmanId = bd.Personel.DepartmanId,
                    DepartmanAdi = bd.Departman.DepartmanAdi
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<HizmetBinasiPersonelleriDto>> GetHizmetBinasiPersonelleriByBankoIdAsync(int bankoId)
        {
            var result = await _context.Bankolar
                .Where(b => b.BankoId == bankoId)
                .Join(_context.HizmetBinalari,
                    b => b.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (b, hb) => new { Banko = b, HizmetBinasi = hb })
                .Join(_context.Departmanlar,
                    bh => bh.HizmetBinasi.DepartmanId,
                    d => d.DepartmanId,
                    (bh, d) => new { bh.Banko, bh.HizmetBinasi, Departman = d })
                .Join(_context.Personeller.Where(p => p.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif),
                    bhd => bhd.HizmetBinasi.HizmetBinasiId,
                    p => p.HizmetBinasiId,
                    (bhd, p) => new HizmetBinasiPersonelleriDto
                    {
                        TcKimlikNo = p.TcKimlikNo,
                        SicilNo = p.SicilNo,
                        PersonelAdSoyad = p.AdSoyad,
                        HizmetBinasiId = bhd.HizmetBinasi.HizmetBinasiId,
                        HizmetBinasiAdi = bhd.HizmetBinasi.HizmetBinasiAdi,
                        DepartmanId = bhd.Departman.DepartmanId,
                        DepartmanAdi = bhd.Departman.DepartmanAdi,
                    })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<PersonellerDto> GetPersonelWithDetailsByTcKimlikNoAsync(string tcKimlikNo)
        {
            var personel = await _context.Personeller
                .Where(p => p.TcKimlikNo == tcKimlikNo)
                .Include(p => p.Departman)
                    .ThenInclude(d => d.HizmetBinalari)
                .Include(p => p.BankolarKullanici)
                    .ThenInclude(bk => bk.Bankolar)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .Include(p => p.HizmetBinasi)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return _mapper.Map<PersonellerDto>(personel);
        }

        // ✅ Diğer domain-specific metodlar (varsa eklenebilir)
        public async Task<List<PersonellerDto>> GetPersonellerByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var personeller = await _context.Personeller
                .Where(p => p.HizmetBinasiId == hizmetBinasiId &&
                           p.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif)
                .Include(p => p.Departman)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .OrderBy(p => p.AdSoyad)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PersonellerDto>>(personeller);
        }

        public async Task<List<PersonellerDto>> GetPersonellerByDepartmanIdAsync(int departmanId)
        {
            var personeller = await _context.Personeller
                .Where(p => p.DepartmanId == departmanId &&
                           p.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif)
                .Include(p => p.Departman)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .OrderBy(p => p.AdSoyad)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PersonellerDto>>(personeller);
        }
    }
}