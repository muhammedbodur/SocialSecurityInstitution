using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class BankolarCustomService : IBankolarCustomService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public BankolarCustomService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BankolarRequestDto> GetBankoByIdAsync(int bankoId)
        {
            var banko = await _context.Bankolar
                .Include(b => b.HizmetBinalari)
                .ThenInclude(hb => hb.Departman)
                .AsNoTracking().FirstOrDefaultAsync(b => b.BankoId == bankoId);

            var bankoDto = new BankolarRequestDto
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
                BankoEklenmeTarihi = banko.EklenmeTarihi,
                BankoDuzenlenmeTarihi = banko.DuzenlenmeTarihi
            };

            return bankoDto;
        }

        public async Task<List<BankolarRequestDto>> GetBankolarWithDetailsAsync()
        {
            var bankolar = await _context.Bankolar
                .Include(b => b.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .Include(b => b.BankolarKullanici)
                    .ThenInclude(bk => bk.Personel)
                .Where(b => b.HizmetBinalari.HizmetBinasiAktiflik == Aktiflik.Aktif &&
                            b.HizmetBinalari.Departman.DepartmanAktiflik == Aktiflik.Aktif)
                .Select(b => new {
                    Banko = b,
                    BankoKullanici = b.BankolarKullanici.FirstOrDefault(bk => bk.Personel.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif)
                })
                .OrderBy(b => b.Banko.HizmetBinalari.HizmetBinasiId)
                .ThenBy(b => b.Banko.BankoNo)
                .ToListAsync();


            var bankolarRequestDto = bankolar.Select(b => new BankolarRequestDto
            {
                BankoId = b.Banko.BankoId,
                BankoNo = b.Banko.BankoNo,
                TcKimlikNo = b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.TcKimlikNo ?? "",
                SicilNo = b.Banko.BankolarKullanici?.FirstOrDefault()?.Personel?.SicilNo ?? 0,
                PersonelAdSoyad = b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.AdSoyad ?? "",
                PersonelNickName = b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.NickName ?? "",
                PersonelResim = b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.Resim ?? "empty.png",
                PersonelDepartmanId = b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.DepartmanId != null ? Convert.ToInt32(b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.DepartmanId) : 0,
                PersonelDepartmanAdi = b.Banko.BankolarKullanici.FirstOrDefault()?.Personel?.Departman?.DepartmanAdi ?? "",
                DepartmanId = b.Banko.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = b.Banko.HizmetBinalari.Departman.DepartmanAdi,
                DepartmanAktiflik = b.Banko.HizmetBinalari.Departman.DepartmanAktiflik,
                HizmetBinasiId = b.Banko.HizmetBinasiId,
                HizmetBinasiAdi = b.Banko.HizmetBinalari.HizmetBinasiAdi,
                HizmetBinasiAktiflik = b.Banko.HizmetBinalari.HizmetBinasiAktiflik,
                BankoAktiflik = b.Banko.BankoAktiflik,
                BankoEklenmeTarihi = b.Banko.EklenmeTarihi,
                BankoDuzenlenmeTarihi = b.Banko.DuzenlenmeTarihi
            }).ToList();

            return bankolarRequestDto;
        }

        public async Task<PersonellerDto> GetBankoPersonelDetailAsync(string tcKimlikNo)
        {
            var personel = await _context.Personeller
                .Where(p => p.TcKimlikNo == tcKimlikNo)
                .Include(p => p.Departman)
                    .ThenInclude(d => d.HizmetBinalari)
                .Include(p => p.BankolarKullanici)
                    .ThenInclude(bk => bk.Bankolar)
                .AsNoTracking().FirstOrDefaultAsync();

            var personelDto = _mapper.Map<PersonellerDto>(personel);
            return personelDto;
        }

        public async Task<List<DepartmanPersonelleriDto>> GetDeparmanPersonelleriAsync(int bankoId)
        {
            var personeller = await _context.Bankolar
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
                .Where(bd => bd.Departman.DepartmanAktiflik == Aktiflik.Aktif)
                .Select(bd => new { Personel = bd.Personel, Departman = bd.Departman })
                .ToListAsync();

            var departmanPersonelleriDto = personeller.Select(bd => new DepartmanPersonelleriDto
            {
                TcKimlikNo = bd.Personel.TcKimlikNo,
                SicilNo = bd.Personel.SicilNo,
                PersonelAdSoyad = bd.Personel.AdSoyad,
                DepartmanId = bd.Personel.DepartmanId,
                DepartmanAdi = bd.Departman.DepartmanAdi
            }).ToList();

            return departmanPersonelleriDto;
        }

        public async Task<List<HizmetBinasiPersonelleriDto>> GetHizmetBinasiPersonelleriAsync(int bankoId)
        {
            var personellerList = await _context.Bankolar
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
                .ToListAsync();

            return personellerList;
        }
    }
}
