using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class KanalPersonelleriDal : GenericRepository<KanalPersonelleri, KanalPersonelleriDto>, IKanalPersonelleriDal
    {
        public KanalPersonelleriDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<List<KanalAltIslemleriDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo, int hizmetBinasiId)
        {
            // İlk sorgu: HizmetBinasiId'ye göre KanalAltIslemleri ve KanallarAlt
            var hizmetBinasiAltIslemleri = await _context.KanalAltIslemleri
                .Where(kai => kai.HizmetBinasiId == hizmetBinasiId)
                .Join(_context.KanallarAlt,
                    kai => kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kai, ka) => new { kai, ka.KanalAltAdi })
                .ToListAsync();

            // İkinci sorgu: tcKimlikNo ve HizmetBinasiId'ye göre KanalAltIslemleri ve KanallarAlt
            var personelAltIslemleri = await _context.KanalAltIslemleri
                .Where(kai => kai.HizmetBinasiId == hizmetBinasiId)
                .Join(_context.KanallarAlt,
                    kai => kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kai, ka) => new { kai, ka.KanalAltAdi })
                .Join(_context.KanalPersonelleri,
                    combined => combined.kai.KanalAltIslemId,
                    kp => kp.KanalAltIslemId,
                    (combined, kp) => new { combined.kai, combined.KanalAltAdi, kp.TcKimlikNo })
                .Where(result => result.TcKimlikNo == tcKimlikNo)
                .ToListAsync();

            // Kesişmeyenleri bulma
            var eslesmeyenler = hizmetBinasiAltIslemleri
                .Where(hba => !personelAltIslemleri.Any(pa => pa.kai.KanalAltIslemId == hba.kai.KanalAltIslemId))
                .Select(hba => new KanalAltIslemleriDto
                {
                    KanalAltIslemId = hba.kai.KanalAltIslemId,
                    KanalAltIslemAdi = hba.KanalAltAdi,
                    KanalAltId = hba.kai.KanalAltId,
                    HizmetBinasiId = hba.kai.HizmetBinasiId,
                    KanalAltIslemAktiflik = hba.kai.KanalAltIslemAktiflik,
                    DuzenlenmeTarihi = hba.kai.DuzenlenmeTarihi,
                    EklenmeTarihi = hba.kai.EklenmeTarihi
                })
                .ToList();

            return eslesmeyenler;
        }

        public async Task<List<PersonelAltKanallariRequestDto>> GetPersonelAltKanallariAsync(string tcKimlikNo)
        {
            var query = from p in _context.Personeller
                        join kp in _context.KanalPersonelleri on p.TcKimlikNo equals kp.TcKimlikNo
                        join kai in _context.KanalAltIslemleri on kp.KanalAltIslemId equals kai.KanalAltIslemId
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        where p.TcKimlikNo == tcKimlikNo
                        orderby kp.Uzmanlik ascending
                        select new PersonelAltKanallariRequestDto
                        {
                            KanalPersonelId = kp.KanalPersonelId,
                            TcKimlikNo = p.TcKimlikNo,
                            AdSoyad = p.AdSoyad,
                            KanalAltIslemId = kp.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            Uzmanlik = kp.Uzmanlik
                        };

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<PersonellerAltKanallarRequestDto>> GetPersonellerAltKanallarAsync(int hizmetBinasiId)
        {
            var personellerAltKanallarDto = await _context.Personeller
                .Where(p => p.HizmetBinasiId == hizmetBinasiId)
                .GroupJoin(_context.KanalPersonelleri,
                    p => p.TcKimlikNo,
                    kp => kp.TcKimlikNo,
                    (p, kps) => new { Personel = p, KanalPersonelleri = kps })
                .SelectMany(
                    pk => pk.KanalPersonelleri.DefaultIfEmpty(),
                    (pk, kp) => new { pk.Personel, KanalPersonel = kp })
                .GroupBy(pkp => new { pkp.Personel.TcKimlikNo, pkp.Personel.AdSoyad })
                .Select(g => new
                {
                    g.Key.TcKimlikNo,
                    g.Key.AdSoyad,
                    UzmanSayisi = g.Count(pkp => pkp.KanalPersonel != null && pkp.KanalPersonel.Uzmanlik == Enums.PersonelUzmanlik.Uzman),
                    UzmanYrdSayisi = g.Count(pkp => pkp.KanalPersonel != null && pkp.KanalPersonel.Uzmanlik == Enums.PersonelUzmanlik.YrdUzman)
                })
            .ToListAsync();

            var requestDtos = personellerAltKanallarDto.Select(p => new PersonellerAltKanallarRequestDto
            {
                TcKimlikNo = p.TcKimlikNo,
                AdSoyad = p.AdSoyad,
                UzmanSayisi = p.UzmanSayisi,
                UzmanYrdSayisi = p.UzmanYrdSayisi,
            }).ToList();

            return requestDtos;
        }

        public async Task<List<PersonelAltKanallariRequestDto>> GetKanalPersonelleriWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var query = from p in _context.Personeller
                        join kp in _context.KanalPersonelleri on p.TcKimlikNo equals kp.TcKimlikNo
                        join kai in _context.KanalAltIslemleri on kp.KanalAltIslemId equals kai.KanalAltIslemId
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        where p.HizmetBinasiId == hizmetBinasiId
                        orderby kp.Uzmanlik ascending
                        select new PersonelAltKanallariRequestDto
                        {
                            KanalPersonelId = kp.KanalPersonelId,
                            TcKimlikNo = p.TcKimlikNo,
                            AdSoyad = p.AdSoyad,
                            KanalAltIslemId = kp.KanalAltIslemId,
                            KanalAltIslemAdi = ka.KanalAltAdi,
                            Uzmanlik = kp.Uzmanlik
                        };

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<KanalPersonelleriViewRequestDto>> GetKanalAltPersonelleriAsync(int kanalAltIslemId)
        {
            var query = from kp in _context.KanalPersonelleri
                        join p in _context.Personeller on kp.TcKimlikNo equals p.TcKimlikNo
                        join kai in _context.KanalAltIslemleri on kp.KanalAltIslemId equals kai.KanalAltIslemId
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId into kanalIslemGroup
                        from kanalIslem in kanalIslemGroup.DefaultIfEmpty()
                        join kn in _context.Kanallar on kanalIslem.KanalId equals kn.KanalId into kanalGroup
                        from kanal in kanalGroup.DefaultIfEmpty()
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId into hizmetBinasiGroup
                        from hizmetBinasi in hizmetBinasiGroup.DefaultIfEmpty()
                        join d in _context.Departmanlar on p.DepartmanId equals d.DepartmanId into departmanGroup
                        from dept in departmanGroup.DefaultIfEmpty()
                        join s in _context.Servisler on p.ServisId equals s.ServisId into servisGroup
                        from serv in servisGroup.DefaultIfEmpty()
                        join u in _context.Unvanlar on p.UnvanId equals u.UnvanId into unvanGroup
                        from unv in unvanGroup.DefaultIfEmpty()
                        where kp.KanalAltIslemId == kanalAltIslemId
                        orderby p.AdSoyad ascending
                        select new KanalPersonelleriViewRequestDto
                        {
                            KanalPersonelId = kp.KanalPersonelId,
                            TcKimlikNo = p.TcKimlikNo,
                            SicilNo = p.SicilNo,
                            AdSoyad = p.AdSoyad,
                            KanalAltIslemId = kp.KanalAltIslemId,
                            KanalAltAdi = ka.KanalAltAdi,
                            KanalIslemId = kai.KanalIslemId ?? 0,
                            KanalIslemAdi = kanal != null ? kanal.KanalAdi : "",
                            HizmetBinasiId = kai.HizmetBinasiId,
                            HizmetBinasiAdi = hizmetBinasi != null ? hizmetBinasi.HizmetBinasiAdi : "",
                            DepartmanId = p.DepartmanId,
                            DepartmanAdi = dept != null ? dept.DepartmanAdi : "",
                            ServisId = p.ServisId,
                            ServisAdi = serv != null ? serv.ServisAdi : "",
                            UnvanId = p.UnvanId,
                            UnvanAdi = unv != null ? unv.UnvanAdi : "",
                            Aktiflik = (Aktiflik)p.PersonelAktiflikDurum,
                            AktiflikAdi = p.PersonelAktiflikDurum.ToString(),
                            EklenmeTarihi = kp.EklenmeTarihi,
                            DuzenlenmeTarihi = kp.DuzenlenmeTarihi
                        };

            var result = await query.ToListAsync();
            return result;
        }
    }
}