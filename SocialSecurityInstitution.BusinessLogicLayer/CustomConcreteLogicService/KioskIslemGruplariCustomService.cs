using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public KioskIslemGruplariCustomService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKioskAltKanalIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
        {
            using var context = new Context();

            var query = from kai in context.KanalAltIslemleri
                join hb in context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                join d in context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                join ka in context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                join kig in context.KioskIslemGruplari on kai.KioskIslemGrupId equals kig.KioskIslemGrupId into kigGroup
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
            using var context = new Context();

            var query = from kig in context.KioskIslemGruplari
                        join hb in context.HizmetBinalari on kig.HizmetBinasiId equals hb.HizmetBinasiId
                        join kg in context.KioskGruplari on kig.KioskGrupId equals kg.KioskGrupId
                        join kai in context.KanalAltIslemleri on kig.KioskIslemGrupId equals kai.KioskIslemGrupId into kaiGroup
                        from subKai in kaiGroup.DefaultIfEmpty()
                        where kig.HizmetBinasiId == hizmetBinasiId
                        group subKai by new { kig.KioskIslemGrupId, kg.KioskGrupAdi , kig.KioskIslemGrupSira} into g
                        select new KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto
                        {
                            KioskIslemGrupId = g.Key.KioskIslemGrupId,
                            KioskIslemGrupAdi = g.Key.KioskGrupAdi,
                            KioskIslemGrupSira = g.Key.KioskIslemGrupSira,
                            EslestirmeSayisi = g.Count(kai => kai.KioskIslemGrupId != null)
                        };

            var resultList = await query.ToListAsync();

            return resultList;
        }

        public async Task<List<KioskIslemGruplariRequestDto>> GetKioskIslemGruplariAsync(int hizmetBinasiId)
        {
            using var context = new Context();

            var result = await context.KioskIslemGruplari
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
            using var context = new Context();

            var result = await context.KioskIslemGruplari
            .Include(kig => kig.KioskGruplari)
            .Include(kig => kig.HizmetBinalari)
                .ThenInclude(hb => hb.Departman)
            .Where(kig => kig.KioskIslemGrupId == kioskIslemGrupId)
            .FirstOrDefaultAsync();

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
            using var context = new Context();

            var query = from kai in context.KanalAltIslemleri
                        join ka in context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join hb in context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ki in context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId into kiGroup
                        from ki in kiGroup.DefaultIfEmpty()
                        join kg in context.KioskIslemGruplari on kai.KioskIslemGrupId equals kg.KioskIslemGrupId into kgGroup
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
            using var context = new Context();

            var result = await context.KanalAltIslemleri
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
