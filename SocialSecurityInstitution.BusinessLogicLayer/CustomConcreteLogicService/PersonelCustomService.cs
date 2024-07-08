using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelCustomService : IPersonelCustomService
    {
        private readonly IMapper _mapper;

        public PersonelCustomService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<PersonellerDto>> GetPersonellerDepartmanIdAndHizmetBinasiIdAsync(int departmanId, int hizmetBinasiId)
        {
            using var context = new Context();
            var query = await context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .Where(p => p.DepartmanId == departmanId && p.HizmetBinasiId == hizmetBinasiId)
                .ToListAsync();

            var dtoList = _mapper.Map<List<PersonellerDto>>(query);
            return dtoList;
        }

        public async Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync()
        {
            using var context = new Context();

            var personeller = await context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .Include(p => p.Sendika)
                .Include(p => p.Il)
                .Include(p => p.Ilce)
                .Select(p => new PersonelRequestDto
                {
                    TcKimlikNo = p.TcKimlikNo,
                    AdSoyad = p.AdSoyad,
                    SicilNo = p.SicilNo,
                    DepartmanId = p.DepartmanId,
                    DepartmanAdi = p.Departman.DepartmanAdi,
                    ServisId = p.ServisId,
                    ServisAdi = p.Servis.ServisAdi,
                    UnvanId = p.UnvanId,
                    UnvanAdi = p.Unvan.UnvanAdi,
                    Gorev = p.Gorev,
                    Uzmanlik = p.Uzmanlik,
                    AtanmaNedeni = p.AtanmaNedeni.AtanmaNedeni,
                    SendikaAdi = p.Sendika.SendikaAdi,
                    IlAdi = p.Il.IlAdi,
                    IlceAdi = p.Ilce.IlceAdi,
                    EsininIsIlAdi = context.Iller.Where(i => i.IlId == p.EsininIsIlId).Select(i => i.IlAdi).FirstOrDefault(),
                    EsininIsIlceAdi = context.Ilceler.Where(ic => ic.IlceId == p.EsininIsIlceId).Select(ic => ic.IlceAdi).FirstOrDefault(),
                    PersonelAktiflikDurum = p.PersonelAktiflikDurum,
                    Resim = p.Resim,
                    Dahili = p.Dahili,
                    Email = p.Email,
                    CepTelefonu = p.CepTelefonu,
                    CepTelefonu2 = p.CepTelefonu2,
                    EvTelefonu = p.EvTelefonu,
                    Adres = p.Adres,
                    DogumTarihi = p.DogumTarihi,
                    Cinsiyet = p.Cinsiyet,
                    MedeniDurumu = p.MedeniDurumu,
                    KanGrubu = p.KanGrubu,
                    EvDurumu = p.EvDurumu,
                    UlasimServis1 = p.UlasimServis1,
                    UlasimServis2 = p.UlasimServis2,
                    Tabldot = p.Tabldot,
                    EmekliSicilNo = p.EmekliSicilNo,
                    OgrenimDurumu = p.OgrenimDurumu,
                    BitirdigiOkul = p.BitirdigiOkul,
                    BitirdigiBolum = p.BitirdigiBolum,
                    OgrenimSuresi = p.OgrenimSuresi,
                    Bransi = p.Bransi,
                    SehitYakinligi = p.SehitYakinligi,
                    EsininAdi = p.EsininAdi,
                    EsininIsDurumu = p.EsininIsDurumu,
                    EsininUnvani = p.EsininUnvani,
                    EsininIsAdresi = p.EsininIsAdresi,
                    EsininIsSemt = p.EsininIsSemt,
                    HizmetBilgisi = p.HizmetBilgisi,
                    EgitimBilgisi = p.EgitimBilgisi,
                    ImzaYetkileri = p.ImzaYetkileri,
                    CezaBilgileri = p.CezaBilgileri,
                    EngelBilgileri = p.EngelBilgileri,
                    DuzenlenmeTarihi = p.DuzenlenmeTarihi
                }).ToListAsync();

            return personeller;
        }

        public async Task<PersonellerDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            using var context = new Context();
            var entity = await context.Personeller.FirstOrDefaultAsync(p => p.TcKimlikNo == tcKimlikNo);
            var dto = _mapper.Map<PersonellerDto>(entity);
            return dto;
        }
    }
}
