using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class KioskIslemGruplariCustomService : IKioskIslemGruplariCustomService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public KioskIslemGruplariCustomService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

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
                        KanalIslemId = kai.KanalIslemId, //null olabilir
                        HizmetBinasiId = kai.HizmetBinasiId,
                        HizmetBinasiAdi = hb.HizmetBinasiAdi,
                        DepartmanId = hb.DepartmanId,
                        DepartmanAdi = d.DepartmanAdi,
                        KioskIslemGrupId = kai.KioskIslemGrupId, //null olabilir
                        KioskIslemGrupAdi = kig.KioskGruplari.KioskGrupAdi, // null olabilir
                        EklenmeTarihi = kai.EklenmeTarihi,
                        DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                        KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                    };

            var requestDtos = await query.ToListAsync();

            return requestDtos;
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
                        group new { kig, kg , kai } by new { kig.KioskIslemGrupId, kg.KioskGrupAdi, kig.KioskIslemGrupSira } into g
                        select new KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto
                        {
                            KioskIslemGrupId = g.Key.KioskIslemGrupId,
                            KioskIslemGrupAdi = g.Key.KioskGrupAdi,
                            KioskIslemGrupSira = g.Key.KioskIslemGrupSira,
                            EslestirmeSayisi = g.Count(k => k.kai.KioskIslemGrupId != null)
                        };

            var resultList = await query.ToListAsync();

            return resultList;
        }

        public async Task<List<KioskIslemGruplariRequestDto>> GetKioskIslemGruplariAsync(int hizmetBinasiId)
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

        public async Task<KioskIslemGruplariRequestDto> GetKioskIslemGruplariByIdAsync(int kioskIslemGrupId)
        {
            var result = await _context.KioskIslemGruplari
            .Include(kig => kig.KioskGruplari)
            .Include(kig => kig.HizmetBinalari)
                .ThenInclude(hb => hb.Departman)
            .Where(kig => kig.KioskIslemGrupId == kioskIslemGrupId)
            .AsNoTracking().FirstOrDefaultAsync();

            if (result == null)
            {
                return null; // veya başka bir işlem yapılabilir
            }

            var requestDtos = new KioskIslemGruplariRequestDto
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

            return requestDtos;
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
                            KanalIslemId = kai.KanalIslemId, //null olabilir
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hb.HizmetBinasiAdi,
                            DepartmanId = hb.DepartmanId,
                            DepartmanAdi = d.DepartmanAdi,
                            KioskIslemGrupId = kai.KioskIslemGrupId, //null olacak
                            KioskIslemGrupAdi = kg.KioskGruplari.KioskGrupAdi, // null olacak
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            var requestDtos = await query.ToListAsync();

            return requestDtos;
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
