// =============================================================================
// 1. ÖNCE: HizmetBinalariDal - Zenginleştirilmiş Repository
// =============================================================================

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class HizmetBinalariDal : GenericRepository<HizmetBinalari, HizmetBinalariDto>, IHizmetBinalariDal
    {
        public HizmetBinalariDal(Context context, IMapper mapper, ILogService logService)
            : base(context, mapper, logService)
        {
        }

        public async Task<List<HizmetBinalariDto>> GetHizmetBinalariByDepartmanIdAsync(int departmanId)
        {
            var hizmetBinalari = await _context.HizmetBinalari
                .Where(x => x.DepartmanId == departmanId &&
                           x.HizmetBinasiAktiflik == Aktiflik.Aktif)
                .OrderBy(x => x.HizmetBinasiAdi)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HizmetBinalariDto>>(hizmetBinalari);
        }

        public async Task<HizmetBinalariDepartmanlarDto> GetActiveHizmetBinasiWithDepartmanAsync(int hizmetBinasiId, int departmanId)
        {
            var hizmetBinalari = await _context.HizmetBinalari
                .Include(hb => hb.Departman)
                .Where(hb => hb.HizmetBinasiAktiflik == Aktiflik.Aktif &&
                           hb.HizmetBinasiId == hizmetBinasiId &&
                           hb.DepartmanId == departmanId &&
                           hb.Departman.DepartmanAktiflik == Aktiflik.Aktif)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (hizmetBinalari == null) return null;

            return new HizmetBinalariDepartmanlarDto
            {
                HizmetBinasiId = hizmetBinalari.HizmetBinasiId,
                HizmetBinasiAdi = hizmetBinalari.HizmetBinasiAdi,
                HizmetBinasiAktiflik = hizmetBinalari.HizmetBinasiAktiflik,
                HizmetBinasiEklenmeTarihi = hizmetBinalari.EklenmeTarihi,
                HizmetBinasiDuzenlenmeTarihi = hizmetBinalari.DuzenlenmeTarihi,
                DepartmanId = hizmetBinalari.DepartmanId,
                DepartmanAdi = hizmetBinalari.Departman.DepartmanAdi,
                DepartmanAktiflik = hizmetBinalari.Departman.DepartmanAktiflik,
                DepartmanEklenmeTarihi = hizmetBinalari.Departman.EklenmeTarihi,
                DepartmanDuzenlenmeTarihi = hizmetBinalari.Departman.DuzenlenmeTarihi
            };
        }

        public async Task<HizmetBinalariDepartmanlarDto> GetHizmetBinasiWithDepartmanByIdAsync(int hizmetBinasiId)
        {
            var hizmetBinalari = await _context.HizmetBinalari
                .Include(hb => hb.Departman)
                .Where(hb => hb.HizmetBinasiId == hizmetBinasiId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (hizmetBinalari == null) return null;

            return new HizmetBinalariDepartmanlarDto
            {
                HizmetBinasiId = hizmetBinalari.HizmetBinasiId,
                HizmetBinasiAdi = hizmetBinalari.HizmetBinasiAdi,
                HizmetBinasiAktiflik = hizmetBinalari.HizmetBinasiAktiflik,
                HizmetBinasiEklenmeTarihi = hizmetBinalari.EklenmeTarihi,
                HizmetBinasiDuzenlenmeTarihi = hizmetBinalari.DuzenlenmeTarihi,
                DepartmanId = hizmetBinalari.DepartmanId,
                DepartmanAdi = hizmetBinalari.Departman.DepartmanAdi,
                DepartmanAktiflik = hizmetBinalari.Departman.DepartmanAktiflik,
                DepartmanEklenmeTarihi = hizmetBinalari.Departman.EklenmeTarihi,
                DepartmanDuzenlenmeTarihi = hizmetBinalari.Departman.DuzenlenmeTarihi
            };
        }

        public async Task<List<HizmetBinalariDto>> GetAllActiveHizmetBinalariAsync()
        {
            var hizmetBinalari = await _context.HizmetBinalari
                .Where(hb => hb.HizmetBinasiAktiflik == Aktiflik.Aktif)
                .OrderBy(hb => hb.HizmetBinasiAdi)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HizmetBinalariDto>>(hizmetBinalari);
        }

        public async Task<List<HizmetBinalariDepartmanlarDto>> GetHizmetBinalariWithDepartmanDetailsAsync()
        {
            var results = await _context.HizmetBinalari
                .Include(hb => hb.Departman)
                .Where(hb => hb.HizmetBinasiAktiflik == Aktiflik.Aktif &&
                           hb.Departman.DepartmanAktiflik == Aktiflik.Aktif)
                .OrderBy(hb => hb.Departman.DepartmanAdi)
                .ThenBy(hb => hb.HizmetBinasiAdi)
                .AsNoTracking()
                .ToListAsync();

            return results.Select(hb => new HizmetBinalariDepartmanlarDto
            {
                HizmetBinasiId = hb.HizmetBinasiId,
                HizmetBinasiAdi = hb.HizmetBinasiAdi,
                HizmetBinasiAktiflik = hb.HizmetBinasiAktiflik,
                HizmetBinasiEklenmeTarihi = hb.EklenmeTarihi,
                HizmetBinasiDuzenlenmeTarihi = hb.DuzenlenmeTarihi,
                DepartmanId = hb.DepartmanId,
                DepartmanAdi = hb.Departman.DepartmanAdi,
                DepartmanAktiflik = hb.Departman.DepartmanAktiflik,
                DepartmanEklenmeTarihi = hb.Departman.EklenmeTarihi,
                DepartmanDuzenlenmeTarihi = hb.Departman.DuzenlenmeTarihi
            }).ToList();
        }

        public async Task<bool> IsHizmetBinasiActiveAsync(int hizmetBinasiId)
        {
            return await _context.HizmetBinalari
                .AnyAsync(hb => hb.HizmetBinasiId == hizmetBinasiId &&
                              hb.HizmetBinasiAktiflik == Aktiflik.Aktif);
        }
    }
}