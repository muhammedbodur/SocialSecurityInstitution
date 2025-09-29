using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class KioskIslemGruplariDal : GenericRepository<KioskIslemGruplari, KioskIslemGruplariDto>, IKioskIslemGruplariDal
    {
        public KioskIslemGruplariDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto>> GetKioskIslemGruplariAltIslemlerEslestirmeSayisiAsync(int hizmetBinasiId)
        {
            var query = from kig in _context.KioskIslemGruplari
                        join hb in _context.HizmetBinalari on kig.HizmetBinasiId equals hb.HizmetBinasiId
                        join kg in _context.KioskGruplari on kig.KioskGrupId equals kg.KioskGrupId
                        join kai in _context.KanalAltIslemleri on kig.KioskIslemGrupId equals kai.KioskIslemGrupId into kaiGroup
                        from kai in kaiGroup.DefaultIfEmpty()
                        join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId into kiGroup
                        from ki in kiGroup.DefaultIfEmpty()
                        where kig.HizmetBinasiId == hizmetBinasiId
                        group new { kig, kg, kai } by new { kig.KioskIslemGrupId, kg.KioskGrupAdi, kig.KioskIslemGrupSira } into g
                        select new KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto
                        {
                            KioskIslemGrupId = g.Key.KioskIslemGrupId,
                            KioskIslemGrupAdi = g.Key.KioskGrupAdi,
                            KioskIslemGrupSira = g.Key.KioskIslemGrupSira,
                            EslestirmeSayisi = g.Count(x => x.kai.KanalAltIslemId != null)
                        };

            return await query.ToListAsync();
        }

        public async Task<List<KioskIslemGruplariRequestDto>> GetKioskIslemGruplariByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var result = await _context.KioskIslemGruplari
                .Include(kig => kig.KioskGruplari)
                .Include(kig => kig.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .Where(kig => kig.HizmetBinasiId == hizmetBinasiId)
                .ToListAsync();

            var requestDtos = result.Select(ki => new KioskIslemGruplariRequestDto
            {
                KioskIslemGrupId = ki.KioskIslemGrupId,
                KioskIslemGrupAdi = ki.KioskGruplari.KioskGrupAdi,
                KioskGrupId = ki.KioskGrupId,
                HizmetBinasiId = ki.HizmetBinasiId,
                HizmetBinasiAdi = ki.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = ki.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = ki.HizmetBinalari.Departman.DepartmanAdi,
                KioskIslemGrupSira = ki.KioskIslemGrupSira,
                KioskIslemGrupAktiflik = ki.KioskIslemGrupAktiflik,
                EklenmeTarihi = ki.EklenmeTarihi,
                DuzenlenmeTarihi = ki.DuzenlenmeTarihi
            }).ToList();

            return requestDtos;
        }

        public async Task<KioskIslemGruplariRequestDto> GetKioskIslemGruplariByIdWithDetailsAsync(int kioskIslemGrupId)
        {
            var result = await _context.KioskIslemGruplari
                .Include(kig => kig.KioskGruplari)
                .Include(kig => kig.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .Where(kig => kig.KioskIslemGrupId == kioskIslemGrupId)
                .AsNoTracking().FirstOrDefaultAsync();

            if (result == null)
            {
                return null;
            }

            var requestDto = new KioskIslemGruplariRequestDto
            {
                KioskIslemGrupId = result.KioskIslemGrupId,
                KioskIslemGrupAdi = result.KioskGruplari.KioskGrupAdi,
                KioskGrupId = result.KioskGrupId,
                HizmetBinasiId = result.HizmetBinasiId,
                HizmetBinasiAdi = result.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = result.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = result.HizmetBinalari.Departman.DepartmanAdi,
                KioskIslemGrupSira = result.KioskIslemGrupSira,
                KioskIslemGrupAktiflik = result.KioskIslemGrupAktiflik,
                EklenmeTarihi = result.EklenmeTarihi,
                DuzenlenmeTarihi = result.DuzenlenmeTarihi
            };

            return requestDto;
        }
    }
}
