using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class KanalAltIslemleriDal : GenericRepository<KanalAltIslemleri, KanalAltIslemleriDto>, IKanalAltIslemleriDal
    {
        public KanalAltIslemleriDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<List<KanalAltIslemleriDto>> GetAllKanalAltIslemleriAsync()
        {
            var kanalAltIslemleri = await _context.KanalAltIslemleri
                .Include(b => b.KanalIslem)
                .ToListAsync();

            return _mapper.Map<List<KanalAltIslemleriDto>>(kanalAltIslemleri);
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var query = from kai in _context.KanalAltIslemleri
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in _context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join kig in _context.KioskIslemGruplari on kai.KioskIslemGrupId equals kig.KioskIslemGrupId into kigGroup
                        from kig in kigGroup.DefaultIfEmpty()
                        where kai.HizmetBinasiId == hizmetBinasiId
                        select new KanalAltIslemleriRequestDto
                        {
                            KanalAltIslemId = kai.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            KanalAltId = ka.KanalAltId,
                            KanalAltAdi = ka.KanalAltAdi,
                            KanalIslemId = kai.KanalIslemId,
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hb.HizmetBinasiAdi,
                            DepartmanId = hb.DepartmanId,
                            DepartmanAdi = d.DepartmanAdi,
                            KioskIslemGrupId = kai.KioskIslemGrupId,
                            KioskIslemGrupAdi = kig.KioskGruplari.KioskGrupAdi,
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            return await query.ToListAsync();
        }

        public async Task<KanalAltIslemleriRequestDto> GetKanalAltIslemleriByIdWithDetailsAsync(int kanalAltIslemId)
        {
            var query = from kai in _context.KanalAltIslemleri
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in _context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join kig in _context.KioskIslemGruplari on kai.KioskIslemGrupId equals kig.KioskIslemGrupId into kigGroup
                        from kig in kigGroup.DefaultIfEmpty()
                        where kai.KanalAltIslemId == kanalAltIslemId
                        select new KanalAltIslemleriRequestDto
                        {
                            KanalAltIslemId = kai.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            KanalAltId = ka.KanalAltId,
                            KanalAltAdi = ka.KanalAltAdi,
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hb.HizmetBinasiAdi,
                            DepartmanId = hb.DepartmanId,
                            DepartmanAdi = d.DepartmanAdi,
                            KioskIslemGrupId = kai.KioskIslemGrupId,
                            KioskIslemGrupAdi = kig.KioskGruplari.KioskGrupAdi,
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByIslemIdAsync(int kanalIslemId)
        {
            var result = await _context.KanalAltIslemleri
                .Include(kai => kai.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .Include(kai => kai.KanallarAlt)
                .Include(kai => kai.KanalIslem)
                    .ThenInclude(ki => ki.Kanallar)
                .Where(kai => kai.KanalIslemId == kanalIslemId)
                .ToListAsync();

            var requestDtos = result.Select(k => new KanalAltIslemleriRequestDto
            {
                KanalAltIslemId = k.KanalAltIslemId,
                KanalAltIslemAdi = k.KanallarAlt.KanalAltAdi,
                KanalAltAdi = k.KanallarAlt.KanalAltAdi,
                KanalAltId = k.KanalAltId,
                HizmetBinasiAdi = k.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = k.HizmetBinalari.DepartmanId,
                DepartmanAdi = k.HizmetBinalari.Departman.DepartmanAdi
            }).ToList();

            return requestDtos;
        }

        public async Task<List<KanalAltIslemleriEslestirmeSayisiRequestDto>> GetKanalAltIslemleriEslestirmeSayisiAsync(int hizmetBinasiId)
        {
            var query = from ki in _context.KanalIslemleri
                        join k in _context.Kanallar on ki.KanalId equals k.KanalId
                        join kai in _context.KanalAltIslemleri on ki.KanalIslemId equals kai.KanalIslemId into kaiGroup
                        from kai in kaiGroup.DefaultIfEmpty()
                        where ki.HizmetBinasiId == hizmetBinasiId
                        group kai by new { ki.KanalIslemId, k.KanalAdi } into g
                        select new KanalAltIslemleriEslestirmeSayisiRequestDto
                        {
                            KanalIslemId = g.Key.KanalIslemId,
                            KanalIslemAdi = g.Key.KanalAdi,
                            EslestirmeSayisi = g.Count(kai => kai.KanalAltIslemId != null)
                        };

            return await query.ToListAsync();
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            var query = from kai in _context.KanalAltIslemleri
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in _context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId into kiGroup
                        from ki in kiGroup.DefaultIfEmpty()
                        join kg in _context.KioskIslemGruplari on kai.KioskIslemGrupId equals kg.KioskIslemGrupId into kgGroup
                        from kg in kgGroup.DefaultIfEmpty()
                        where kai.HizmetBinasiId == hizmetBinasiId && ki.KanalIslemId == null
                        select new KanalAltIslemleriRequestDto
                        {
                            KanalAltIslemId = kai.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            KanalAltId = ka.KanalAltId,
                            KanalAltAdi = ka.KanalAltAdi,
                            KanalIslemId = kai.KanalIslemId,
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hb.HizmetBinasiAdi,
                            DepartmanId = hb.DepartmanId,
                            DepartmanAdi = d.DepartmanAdi,
                            KioskIslemGrupId = kai.KioskIslemGrupId,
                            KioskIslemGrupAdi = kg.KioskGruplari.KioskGrupAdi,
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            return await query.ToListAsync();
        }

        // Kiosk related methods
        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            var query = from kai in _context.KanalAltIslemleri
                        join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in _context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join kig in _context.KioskIslemGruplari on kai.KioskIslemGrupId equals kig.KioskIslemGrupId into kigGroup
                        from kig in kigGroup.DefaultIfEmpty()
                        where kai.HizmetBinasiId == hizmetBinasiId && kai.KioskIslemGrupId == null
                        select new KanalAltIslemleriRequestDto
                        {
                            KanalAltIslemId = kai.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            KanalAltId = ka.KanalAltId,
                            KanalAltAdi = ka.KanalAltAdi,
                            KanalIslemId = kai.KanalIslemId,
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hb.HizmetBinasiAdi,
                            DepartmanId = hb.DepartmanId,
                            DepartmanAdi = d.DepartmanAdi,
                            KioskIslemGrupId = kai.KioskIslemGrupId,
                            KioskIslemGrupAdi = kig.KioskGruplari.KioskGrupAdi,
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            return await query.ToListAsync();
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskIslemGruplariKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            var query = from kai in _context.KanalAltIslemleri
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in _context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId into kiGroup
                        from ki in kiGroup.DefaultIfEmpty()
                        join kg in _context.KioskIslemGruplari on kai.KioskIslemGrupId equals kg.KioskIslemGrupId into kgGroup
                        from kg in kgGroup.DefaultIfEmpty()
                        where kai.HizmetBinasiId == hizmetBinasiId && kai.KioskIslemGrupId == null
                        select new KanalAltIslemleriRequestDto
                        {
                            KanalAltIslemId = kai.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            KanalAltId = ka.KanalAltId,
                            KanalAltAdi = ka.KanalAltAdi,
                            KanalIslemId = kai.KanalIslemId,
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hb.HizmetBinasiAdi,
                            DepartmanId = hb.DepartmanId,
                            DepartmanAdi = d.DepartmanAdi,
                            KioskIslemGrupId = kai.KioskIslemGrupId,
                            KioskIslemGrupAdi = kg.KioskGruplari.KioskGrupAdi,
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            return await query.ToListAsync();
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskKanalAltIslemleriByKioskIslemGrupIdAsync(int kioskIslemGrupId)
        {
            var result = await _context.KanalAltIslemleri
                .Include(kai => kai.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .Include(kai => kai.KanallarAlt)
                .Include(kai => kai.KanalIslem)
                    .ThenInclude(ki => ki.Kanallar)
                .Include(kai => kai.KioskIslemGruplari)
                    .ThenInclude(kai => kai.KioskGruplari)
                .Where(kai => kai.KioskIslemGrupId == kioskIslemGrupId)
                .ToListAsync();

            var requestDtos = result.Select(k => new KanalAltIslemleriRequestDto
            {
                KanalAltIslemId = k.KanalAltIslemId,
                KanalAltIslemAdi = k.KanallarAlt.KanalAltAdi,
                KanalAltIslemAktiflik = k.KanalAltIslemAktiflik,
                KanalAltAdi = k.KanallarAlt.KanalAltAdi,
                KanalAltId = k.KanalAltId,
                KioskIslemGrupAdi = k.KioskIslemGruplari.KioskGruplari.KioskGrupAdi,
                KioskIslemGrupId = k.KioskIslemGrupId,
                HizmetBinasiAdi = k.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = k.HizmetBinalari.DepartmanId,
                DepartmanAdi = k.HizmetBinalari.Departman.DepartmanAdi
            }).ToList();

            return requestDtos;
        }
    }
}
