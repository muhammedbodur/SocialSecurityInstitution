using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class KanalPersonelleriCustomService : IKanalPersonelleriCustomService
    {
        private readonly IMapper _mapper;

        public KanalPersonelleriCustomService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<KanalAltIslemleriDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo, int hizmetBinasiId)
        {
            using var context = new Context();

            // İlk sorgu: HizmetBinasiId'ye göre KanalAltIslemleri ve KanallarAlt
            var hizmetBinasiAltIslemleri = await context.KanalAltIslemleri
                .Where(kai => kai.HizmetBinasiId == hizmetBinasiId)
                .Join(context.KanallarAlt,
                    kai => kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kai, ka) => new { kai, ka.KanalAltAdi })
                .ToListAsync();

            // İkinci sorgu: tcKimlikNo ve HizmetBinasiId'ye göre KanalAltIslemleri ve KanallarAlt
            var personelAltIslemleri = await context.KanalAltIslemleri
                .Where(kai => kai.HizmetBinasiId == hizmetBinasiId)
                .Join(context.KanallarAlt,
                    kai => kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kai, ka) => new { kai, ka.KanalAltAdi })
                .Join(context.KanalPersonelleri,
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
            using var context = new Context();

            var query = from p in context.Personeller
                        join kp in context.KanalPersonelleri on p.TcKimlikNo equals kp.TcKimlikNo
                        join kai in context.KanalAltIslemleri on kp.KanalAltIslemId equals kai.KanalAltIslemId
                        join ka in context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
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
            using var context = new Context();

            var personellerAltKanallarDto = await context.Personeller
                .Where(p => p.HizmetBinasiId == hizmetBinasiId)
                .GroupJoin(context.KanalPersonelleri,
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
    }
}
