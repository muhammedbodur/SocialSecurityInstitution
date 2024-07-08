using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class KanallarCustomService : IKanallarCustomService
    {
        private readonly IMapper _mapper;

        public KanallarCustomService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<KanalAltIslemleriDto>> GetKanalAltIslemleriAsync()
        {
            using var context = new Context();

            var kanalAltIslemleri = await context.KanalAltIslemleri
                .Include(b => b.KanalIslem)
                .ToListAsync();

            return _mapper.Map<List<KanalAltIslemleriDto>>(kanalAltIslemleri);
        }

        public async Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriAsync(int hizmetBinasiId)
        {
            using var context = new Context();

            var kanalIslemleri = await context.KanalIslemleri
                .Where(ki => ki.HizmetBinasiId == hizmetBinasiId)
                .Include(ki => ki.Kanallar)
                .Include(ki => ki.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .ToListAsync();

            var requestDtos = kanalIslemleri.Select(ki => new KanalIslemleriRequestDto
            {
                KanalIslemId = ki.KanalIslemId,
                KanalIslemAdi = ki.Kanallar.KanalAdi,
                HizmetBinasiId = ki.HizmetBinalari.HizmetBinasiId,
                HizmetBinasiAdi = ki.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = ki.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = ki.HizmetBinalari.Departman.DepartmanAdi,
                BaslangicNumara = ki.BaslangicNumara,
                BitisNumara = ki.BitisNumara,
                KanalIslemAktiflik = ki.KanalIslemAktiflik,
                EklenmeTarihi = ki.EklenmeTarihi,
                DuzenlenmeTarihi = ki.DuzenlenmeTarihi
            }).ToList();

            return requestDtos;
        }

        public async Task<KanalIslemleriRequestDto> GetKanalIslemleriByIdAsync(int kanalIslemId)
        {
            using var context = new Context();

            var kanalIslemleri = await context.KanalIslemleri
                .Where(ki => ki.KanalIslemId == kanalIslemId)
                .Include(ki => ki.Kanallar)
                .Include(ki => ki.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .FirstOrDefaultAsync();

            if (kanalIslemleri == null)
            {
                return null; // veya başka bir işlem yapılabilir
            }

            var requestDto = new KanalIslemleriRequestDto
            {
                KanalIslemId = kanalIslemleri.KanalIslemId,
                KanalIslemAdi = kanalIslemleri.Kanallar.KanalAdi,
                HizmetBinasiId = kanalIslemleri.HizmetBinalari.HizmetBinasiId,
                HizmetBinasiAdi = kanalIslemleri.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = kanalIslemleri.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = kanalIslemleri.HizmetBinalari.Departman.DepartmanAdi,
                BaslangicNumara = kanalIslemleri.BaslangicNumara,
                BitisNumara = kanalIslemleri.BitisNumara,
                KanalIslemAktiflik = kanalIslemleri.KanalIslemAktiflik,
                EklenmeTarihi = kanalIslemleri.EklenmeTarihi,
                DuzenlenmeTarihi = kanalIslemleri.DuzenlenmeTarihi
            };

            return requestDto;
        }

        public async Task<List<KanalPersonelleriViewDto>> GetKanalPersonelleriAsync(int hizmetBinasiId)
        {
            using var context = new Context();

            var kanalPersonelleri = await context.Personeller
                .Where(p => p.HizmetBinasi.HizmetBinasiId == hizmetBinasiId)
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .ToListAsync();


            var requestDtos = kanalPersonelleri.Select(p => new KanalPersonelleriViewDto
            {
                TcKimlikNo = p.TcKimlikNo,
                SicilNo = p.SicilNo,
                AdSoyad = p.AdSoyad,
                DepartmanId = p.DepartmanId,
                DepartmanAdi = p.Departman.DepartmanAdi,
                ServisId = p.ServisId,
                ServisAdi = p.Servis.ServisAdi,
                UnvanId = p.UnvanId,
                UnvanAdi = p.Unvan.UnvanAdi,
                //KanalIslemleri = 
            }).ToList();

            return requestDtos;
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriAsync(int hizmetBinasiId)
        {
            using var context = new Context();

            var query = from kai in context.KanalAltIslemleri
                        join hb in context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ka in context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join kig in context.KioskIslemGruplari on kai.KioskIslemGrupId equals kig.KioskIslemGrupId into kigGroup
                        from kig in kigGroup.DefaultIfEmpty()
                        where kai.HizmetBinasiId == hizmetBinasiId
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

        public async Task<KanalAltIslemleriRequestDto> GetKanalAltIslemleriByIdAsync(int kanalAltIslemId)
        {
            using var context = new Context();

            var query = from kai in context.KanalAltIslemleri
                        join hb in context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join d in context.Departmanlar on hb.DepartmanId equals d.DepartmanId
                        join ka in context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join kig in context.KioskIslemGruplari on kai.KioskIslemGrupId equals kig.KioskIslemGrupId into kigGroup
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
                            KioskIslemGrupAdi = kig.KioskGruplari.KioskGrupAdi, // null olabilir
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi
                        };

            var requestDtos = await query.FirstOrDefaultAsync();

            return requestDtos;
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriByIslemIdAsync(int kanalIslemId)
        {
            using var context = new Context();

            var result = await context.KanalAltIslemleri
            .Include(kai =>kai.HizmetBinalari)
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
            using var context = new Context();

            var query = from ki in context.KanalIslemleri
                        join k in context.Kanallar on ki.KanalId equals k.KanalId
                        join kai in context.KanalAltIslemleri on ki.KanalIslemId equals kai.KanalIslemId into kaiGroup
                        from kai in kaiGroup.DefaultIfEmpty()
                        where ki.HizmetBinasiId == hizmetBinasiId
                        group kai by new { ki.KanalIslemId, k.KanalAdi } into g
                        select new KanalAltIslemleriEslestirmeSayisiRequestDto
                        {
                            KanalIslemId = g.Key.KanalIslemId,
                            KanalIslemAdi = g.Key.KanalAdi,
                            EslestirmeSayisi = g.Count(kai => kai.KanalAltIslemId != null)
                        };

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<List<KanalAltIslemleriRequestDto>> GetKanalAltIslemleriEslestirmeYapilmamisAsync(int hizmetBinasiId)
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
                        where kai.HizmetBinasiId == hizmetBinasiId && ki.KanalIslemId == null
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
                            KioskIslemGrupAdi = kg.KioskGruplari.KioskGrupAdi, // null olabilir
                            EklenmeTarihi = kai.EklenmeTarihi,
                            DuzenlenmeTarihi = kai.DuzenlenmeTarihi,
                            KanalAltIslemAktiflik = kai.KanalAltIslemAktiflik
                        };

            var requestDtos = await query.ToListAsync();

            return requestDtos;
        }
    }
}
