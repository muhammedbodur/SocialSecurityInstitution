using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkiaSharp;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using Enums = SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
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
        private readonly Context _context;

        public PersonelCustomService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<PersonellerDto>> GetPersonellerDepartmanIdAndHizmetBinasiIdAsync(int departmanId, int hizmetBinasiId)
        {
            var query = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .Where(p => p.DepartmanId == departmanId && p.HizmetBinasiId == hizmetBinasiId)
                .ToListAsync();

            var dtoList = _mapper.Map<List<PersonellerDto>>(query);
            return dtoList;
        }

        public async Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var personeller = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .Include(p => p.HubConnection)
                .Where(p => p.HizmetBinasiId == hizmetBinasiId && p.HubConnection != null && p.HubConnection.ConnectionStatus == Enums.ConnectionStatus.online)
                .ToListAsync();

            var dtoList = personeller.Select(p => new PersonellerLiteDto
            {
                TcKimlikNo = p.TcKimlikNo,
                SicilNo = p.SicilNo,
                AdSoyad = p.AdSoyad,
                DepartmanId = p.DepartmanId,
                HizmetBinasiId = p.HizmetBinasiId,
                SessionID = p.SessionID,
                ConnectionId = p.HubConnection?.ConnectionId,
                ConnectionStatus = p.HubConnection?.ConnectionStatus
            }).ToList();

            return dtoList;
        }

        public async Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync()
        {
            var personeller = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .Include(p => p.Sendika)
                .Include(p => p.Il)
                .Include(p => p.Ilce)
                .Include(p => p.HubConnection)
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
                    EsininIsIlAdi = _context.Iller.Where(i => i.IlId == p.EsininIsIlId).Select(i => i.IlAdi).FirstOrDefault(),
                    EsininIsIlceAdi = _context.Ilceler.Where(ic => ic.IlceId == p.EsininIsIlceId).Select(ic => ic.IlceAdi).FirstOrDefault(),
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
                    ConnectionId = p.HubConnection.ConnectionId,
                    ConnectionStatus = p.HubConnection.ConnectionStatus,
                    DuzenlenmeTarihi = p.DuzenlenmeTarihi
                }).ToListAsync();

            return personeller;
        }

        public async Task<PersonellerDto> TGetByTcKimlikNoAsync(string tcKimlikNo)
        {
            var entity = await _context.Personeller
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TcKimlikNo == tcKimlikNo);
            var dto = _mapper.Map<PersonellerDto>(entity);
            return dto;
        }

        public async Task<PersonellerDto> UpdateSessionIDAsync(string tcKimlikNo, string newSessionId)
        {
            var personel = await _context.Personeller
                .AsNoTracking().FirstOrDefaultAsync(p => p.TcKimlikNo == tcKimlikNo);

            if (personel != null)
            {
                personel.SessionID = newSessionId;
                _context.Personeller.Update(personel);
                await _context.SaveChangesAsync();

                var personelDto = _mapper.Map<PersonellerDto>(personel);

                return personelDto;
            }
            else
            {
                return null;
            }
        }
    }
}
