using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class HizmetBinalariCustomService : IHizmetBinalariCustomService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public HizmetBinalariCustomService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<HizmetBinalariDto>> GetHizmetBinalariByDepartmanIdAsync(int departmanId)
        {
            var hizmetBinalari = await _context.HizmetBinalari
                                  .Where(x => x.DepartmanId == departmanId && x.HizmetBinasiAktiflik == Enums.Aktiflik.Aktif)
                                  .ToListAsync();

            if (hizmetBinalari != null)
            {
                return _mapper.Map<List<HizmetBinalariDto>>(hizmetBinalari);
            }
            else
            {
                return null;
            }
        }

        public async Task<HizmetBinalariDepartmanlarDto> GetActiveHizmetBinasiAsync(int hizmetBinasiId, int departmanId)
        {
            var hizmetBinalari = await _context.HizmetBinalari
                                .Include(hb => hb.Departman)
                                .Where(hb => hb.HizmetBinasiAktiflik == Enums.Aktiflik.Aktif && hb.HizmetBinasiId == hizmetBinasiId && hb.DepartmanId == departmanId)
                                .Where(hb => hb.Departman.DepartmanAktiflik == Enums.Aktiflik.Aktif)
                                .AsNoTracking().FirstOrDefaultAsync();

            if (hizmetBinalari != null)
            {
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
            else
            {
                return null;
            }
        }

        public async Task<HizmetBinalariDepartmanlarDto> GetDepartmanHizmetBinasiAsync(int hizmetBinasiId)
        {
            var hizmetBinalari = await _context.HizmetBinalari
           .Include(hb => hb.Departman)
           .Where(hb => hb.HizmetBinasiId == hizmetBinasiId)
           .AsNoTracking().FirstOrDefaultAsync();

            if (hizmetBinalari != null)
            {
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
            else
            {
                return null;
            }
        }
    }
}
