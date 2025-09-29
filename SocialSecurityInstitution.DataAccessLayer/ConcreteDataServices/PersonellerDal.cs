using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class PersonellerDal : GenericRepository<Personeller, PersonellerDto>, IPersonellerDal
    {
        public PersonellerDal(Context context, IMapper mapper, ILogService logService)
            : base(context, mapper, logService)
        {
        }

        public async Task<PersonellerDto> GetPersonelWithDetailsByTcKimlikNoAsync(string tcKimlikNo)
        {
            var personel = await _context.Personeller
                .Where(p => p.TcKimlikNo == tcKimlikNo)
                .Include(p => p.Departman)
                    .ThenInclude(d => d.HizmetBinalari)
                .Include(p => p.BankolarKullanici)
                    .ThenInclude(bk => bk.Bankolar)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .Include(p => p.HizmetBinasi)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return _mapper.Map<PersonellerDto>(personel);
        }

        public async Task<List<PersonellerDto>> GetPersonellerByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var personeller = await _context.Personeller
                .Where(p => p.HizmetBinasiId == hizmetBinasiId &&
                           p.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif)
                .Include(p => p.Departman)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .OrderBy(p => p.AdSoyad)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PersonellerDto>>(personeller);
        }

        public async Task<List<PersonellerDto>> GetPersonellerByDepartmanIdAsync(int departmanId)
        {
            var personeller = await _context.Personeller
                .Where(p => p.DepartmanId == departmanId &&
                           p.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif)
                .Include(p => p.Departman)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .OrderBy(p => p.AdSoyad)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PersonellerDto>>(personeller);
        }

        public async Task<List<KanalPersonelleriViewRequestDto>> GetKanalPersonelleriViewByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var kanalPersonelleri = await _context.Personeller
                .Where(p => p.HizmetBinasi.HizmetBinasiId == hizmetBinasiId)
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .Include(p => p.Servis)
                .Include(p => p.Unvan)
                .ToListAsync();

            var requestDtos = kanalPersonelleri.Select(p => new KanalPersonelleriViewRequestDto
            {
                KanalPersonelId = 0, // Bu değer join'den gelecek
                KanalAltIslemId = 0, // Bu değer join'den gelecek
                KanalAltAdi = "", // Bu değer join'den gelecek
                KanalIslemId = 0, // Bu değer join'den gelecek
                KanalIslemAdi = "", // Bu değer join'den gelecek
                TcKimlikNo = p.TcKimlikNo,
                AdSoyad = p.AdSoyad,
                HizmetBinasiId = p.HizmetBinasiId,
                HizmetBinasiAdi = p.HizmetBinasi.HizmetBinasiAdi,
                DepartmanId = p.DepartmanId,
                DepartmanAdi = p.Departman.DepartmanAdi,
                Aktiflik = p.PersonelAktiflikDurum == PersonelAktiflikDurum.Aktif ? Aktiflik.Aktif : Aktiflik.Pasif,
                AktiflikAdi = p.PersonelAktiflikDurum.ToString(),
                EklenmeTarihi = DateTime.Now, // Bu değer join'den gelecek
                DuzenlenmeTarihi = p.DuzenlenmeTarihi
            }).ToList();

            return requestDtos;
        }

        public async Task<LoginDto> AuthenticateUserAsync(string tcKimlikNo, string password)
        {
            var user = await _context.Personeller
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TcKimlikNo == tcKimlikNo && x.PassWord == password);

            if (user == null)
            {
                return null;
            }

            var loginDto = new LoginDto
            {
                TcKimlikNo = user.TcKimlikNo,
                AdSoyad = user.AdSoyad,
                Email = user.Email,
                Resim = user.Resim,
                HizmetBinasiId = user.HizmetBinasiId
            };

            return loginDto;
        }

        public async Task<List<PersonellerDto>> GetPersonellerByDepartmanAndHizmetBinasiAsync(int departmanId, int hizmetBinasiId)
        {
            var result = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .Where(p => p.DepartmanId == departmanId && p.HizmetBinasiId == hizmetBinasiId)
                .ToListAsync();

            return _mapper.Map<List<PersonellerDto>>(result);
        }

        public async Task<List<PersonellerLiteDto>> GetPersonellerWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var personeller = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.HizmetBinasi)
                .Include(p => p.HubConnection)
                .Where(p => p.HizmetBinasiId == hizmetBinasiId && p.HubConnection != null && p.HubConnection.ConnectionStatus == BusinessObjectLayer.CommonEntities.Enums.ConnectionStatus.online)
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

        public async Task<PersonellerDto> GetByTcKimlikNoAsync(string tcKimlikNo)
        {
            var entity = await _context.Personeller
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TcKimlikNo == tcKimlikNo);
            
            return _mapper.Map<PersonellerDto>(entity);
        }

        public async Task<PersonellerDto> UpdateSessionIdAsync(string tcKimlikNo, string newSessionId)
        {
            var personel = await _context.Personeller
                .FirstOrDefaultAsync(p => p.TcKimlikNo == tcKimlikNo);

            if (personel != null)
            {
                personel.SessionID = newSessionId;
                _context.Personeller.Update(personel);
                await _context.SaveChangesAsync();

                return _mapper.Map<PersonellerDto>(personel);
            }

            return null;
        }

        public async Task<List<PersonellerLiteDto>> GetActivePersonelListAsync()
        {
            var personeller = await _context.Personeller
                .Where(p => p.PersonelAktiflikDurum == BusinessObjectLayer.CommonEntities.Enums.PersonelAktiflikDurum.Aktif)
                .OrderBy(p => p.AdSoyad)
                .ToListAsync();

            var dtoList = personeller.Select(p => new PersonellerLiteDto
            {
                TcKimlikNo = p.TcKimlikNo,
                SicilNo = p.SicilNo,
                AdSoyad = p.AdSoyad,
                DepartmanId = p.DepartmanId,
                HizmetBinasiId = p.HizmetBinasiId,
                SessionID = null,
                ConnectionId = null,
                ConnectionStatus = null
            }).ToList();

            return dtoList;
        }

        public async Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync()
        {
            try
            {
                var personeller = await _context.Personeller
                    .Include(p => p.Departman)
                    .Include(p => p.Servis)
                    .Include(p => p.Unvan)
                    .Include(p => p.Sendika)
                    .Include(p => p.Il)
                    .Include(p => p.Ilce)
                    .Include(p => p.HubConnection)
                    .Include(p => p.AtanmaNedeni)
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
                        ConnectionStatus = p.HubConnection.ConnectionStatus == null ? ConnectionStatus.offline : p.HubConnection.ConnectionStatus,
                        DuzenlenmeTarihi = p.DuzenlenmeTarihi
                    }).ToListAsync();

                return personeller;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");

                return new List<PersonelRequestDto>(); // Boş liste dönerek çağıran kodun çökmesini önler
            }
        }

        public async Task<List<PersonelAltKanallarDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo)
        {
            // Tüm kanal alt işlemlerini al
            var allKanalAltIslemleri = await _context.KanalAltIslemleri
                .Join(_context.KanallarAlt,
                    kai => kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kai, ka) => new { kai, ka })
                .Join(_context.KanalIslemleri,
                    kaika => kaika.kai.KanalIslemId,
                    ki => ki.KanalIslemId,
                    (kaika, ki) => new { kaika.kai, kaika.ka, ki })
                .Join(_context.HizmetBinalari,
                    kaikaki => kaikaki.kai.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (kaikaki, hb) => new PersonelAltKanallarDto
                    {
                        KanalAltIslemId = kaikaki.kai.KanalAltIslemId,
                        KanalAltAdi = kaikaki.ka.KanalAltAdi,
                        KanalIslemId = kaikaki.ki.KanalIslemId,
                        KanalIslemAdi = "Kanal İşlem " + kaikaki.ki.KanalIslemId, // KanalIslemleri entity'sinde KanalIslemAdi property'si yok
                        TcKimlikNo = "",
                        AdSoyad = "",
                        HizmetBinasiId = hb.HizmetBinasiId,
                        HizmetBinasiAdi = hb.HizmetBinasiAdi,
                        EklenmeTarihi = kaikaki.kai.EklenmeTarihi,
                        DuzenlenmeTarihi = kaikaki.kai.DuzenlenmeTarihi
                    })
                .ToListAsync();

            // Personelin mevcut kanal alt işlemlerini al
            var personelKanalAltIslemleri = await _context.KanalPersonelleri
                .Where(kp => kp.TcKimlikNo == tcKimlikNo)
                .Select(kp => kp.KanalAltIslemId)
                .ToListAsync();

            // Eşleşmeyenleri filtrele
            var eslesmeyenler = allKanalAltIslemleri
                .Where(kai => !personelKanalAltIslemleri.Contains(kai.KanalAltIslemId))
                .ToList();

            return eslesmeyenler;
        }

        public async Task<List<PersonelAltKanallarDto>> GetPersonelAltKanallariAsync(string tcKimlikNo)
        {
            var result = await _context.KanalPersonelleri
                .Where(kp => kp.TcKimlikNo == tcKimlikNo)
                .Join(_context.KanalAltIslemleri,
                    kp => kp.KanalAltIslemId,
                    kai => kai.KanalAltIslemId,
                    (kp, kai) => new { kp, kai })
                .Join(_context.KanallarAlt,
                    kpkai => kpkai.kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kpkai, ka) => new { kpkai.kp, kpkai.kai, ka })
                .Join(_context.KanalIslemleri,
                    kpkaika => kpkaika.kai.KanalIslemId,
                    ki => ki.KanalIslemId,
                    (kpkaika, ki) => new { kpkaika.kp, kpkaika.kai, kpkaika.ka, ki })
                .Join(_context.Personeller,
                    kpkaikaki => kpkaikaki.kp.TcKimlikNo,
                    p => p.TcKimlikNo,
                    (kpkaikaki, p) => new { kpkaikaki.kp, kpkaikaki.kai, kpkaikaki.ka, kpkaikaki.ki, p })
                .Join(_context.HizmetBinalari,
                    kpkaikakip => kpkaikakip.kai.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (kpkaikakip, hb) => new PersonelAltKanallarDto
                    {
                        KanalAltIslemId = kpkaikakip.kai.KanalAltIslemId,
                        KanalAltAdi = kpkaikakip.ka.KanalAltAdi,
                        KanalIslemId = kpkaikakip.ki.KanalIslemId,
                        KanalIslemAdi = "Kanal İşlem " + kpkaikakip.ki.KanalIslemId, // KanalIslemleri entity'sinde KanalIslemAdi property'si yok
                        TcKimlikNo = kpkaikakip.p.TcKimlikNo,
                        AdSoyad = kpkaikakip.p.AdSoyad,
                        HizmetBinasiId = hb.HizmetBinasiId,
                        HizmetBinasiAdi = hb.HizmetBinasiAdi,
                        EklenmeTarihi = kpkaikakip.kp.EklenmeTarihi,
                        DuzenlenmeTarihi = kpkaikakip.kp.DuzenlenmeTarihi
                    })
                .ToListAsync();

            return result;
        }

        public async Task<List<PersonellerAltKanallarDto>> GetPersonellerAltKanallarAsync(int hizmetBinasiId)
        {
            var result = await _context.KanalPersonelleri
                .Join(_context.KanalAltIslemleri,
                    kp => kp.KanalAltIslemId,
                    kai => kai.KanalAltIslemId,
                    (kp, kai) => new { kp, kai })
                .Join(_context.KanallarAlt,
                    kpkai => kpkai.kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (kpkai, ka) => new { kpkai.kp, kpkai.kai, ka })
                .Join(_context.KanalIslemleri,
                    kpkaika => kpkaika.kai.KanalIslemId,
                    ki => ki.KanalIslemId,
                    (kpkaika, ki) => new { kpkaika.kp, kpkaika.kai, kpkaika.ka, ki })
                .Join(_context.Personeller,
                    kpkaikaki => kpkaikaki.kp.TcKimlikNo,
                    p => p.TcKimlikNo,
                    (kpkaikaki, p) => new { kpkaikaki.kp, kpkaikaki.kai, kpkaikaki.ka, kpkaikaki.ki, p })
                .Join(_context.HizmetBinalari,
                    kpkaikakip => kpkaikakip.kai.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (kpkaikakip, hb) => new { kpkaikakip.kp, kpkaikakip.kai, kpkaikakip.ka, kpkaikakip.ki, kpkaikakip.p, hb })
                .Join(_context.Departmanlar,
                    kpkaikakiphb => kpkaikakiphb.hb.DepartmanId,
                    d => d.DepartmanId,
                    (kpkaikakiphb, d) => new PersonellerAltKanallarDto
                    {
                        KanalAltIslemId = kpkaikakiphb.kai.KanalAltIslemId,
                        KanalAltAdi = kpkaikakiphb.ka.KanalAltAdi,
                        KanalIslemId = kpkaikakiphb.ki.KanalIslemId,
                        KanalIslemAdi = "Kanal İşlem " + kpkaikakiphb.ki.KanalIslemId, // KanalIslemleri entity'sinde KanalIslemAdi property'si yok
                        TcKimlikNo = kpkaikakiphb.p.TcKimlikNo,
                        AdSoyad = kpkaikakiphb.p.AdSoyad,
                        HizmetBinasiId = kpkaikakiphb.hb.HizmetBinasiId,
                        HizmetBinasiAdi = kpkaikakiphb.hb.HizmetBinasiAdi,
                        DepartmanId = d.DepartmanId,
                        DepartmanAdi = d.DepartmanAdi,
                        EklenmeTarihi = kpkaikakiphb.kp.EklenmeTarihi,
                        DuzenlenmeTarihi = kpkaikakiphb.kp.DuzenlenmeTarihi
                    })
                .Where(result => result.HizmetBinasiId == hizmetBinasiId)
                .ToListAsync();

            return result;
        }

        public async Task<PersonellerViewDto> GetPersonelViewForEditAsync(string tcKimlikNo)
        {
            var data = await _context.Personeller
                .Where(p => p.TcKimlikNo == tcKimlikNo)
                .Select(p => new PersonellerViewDto
                {
                    // Temel bilgiler - direkt mapping
                    TcKimlikNo = p.TcKimlikNo,
                    SicilNo = p.SicilNo,
                    AdSoyad = p.AdSoyad,
                    NickName = p.NickName,
                    PersonelKayitNo = p.PersonelKayitNo,
                    KartNo = p.KartNo,
                    KartNoAktiflikTarihi = p.KartNoAktiflikTarihi ?? DateTime.MinValue,
                    KartNoDuzenlenmeTarihi = p.KartNoDuzenlenmeTarihi ?? DateTime.MinValue,
                    KartNoGonderimTarihi = p.KartNoGonderimTarihi ?? DateTime.MinValue,
                    KartGonderimIslemBasari = p.KartGonderimIslemBasari,

                    // ID'ler
                    DepartmanId = p.DepartmanId,
                    ServisId = p.ServisId,
                    UnvanId = p.UnvanId,
                    AtanmaNedeniId = p.AtanmaNedeniId,
                    IlId = p.IlId,
                    IlceId = p.IlceId,
                    SendikaId = p.SendikaId,
                    EsininIsIlId = p.EsininIsIlId,
                    EsininIsIlceId = p.EsininIsIlceId,

                    // Navigation property'lerden gelen adlar - EF Core otomatik JOIN yapar
                    DepartmanAdi = p.Departman != null ? p.Departman.DepartmanAdi : null,
                    ServisAdi = p.Servis != null ? p.Servis.ServisAdi : null,
                    UnvanAdi = p.Unvan != null ? p.Unvan.UnvanAdi : null,
                    HizmetBinasiId = p.HizmetBinasiId,
                    IlAdi = p.Il != null ? p.Il.IlAdi : null,
                    IlceAdi = p.Ilce != null ? p.Ilce.IlceAdi : null,
                    SendikaAdi = p.Sendika != null ? p.Sendika.SendikaAdi : null,
                    EsininIsIlAdi = p.EsininIsIl != null ? p.EsininIsIl.IlAdi : null,
                    EsininIsIlceAdi = p.EsininIsIlce != null ? p.EsininIsIlce.IlceAdi : null,

                    // Diğer tüm alanlar
                    Gorev = p.Gorev,
                    Uzmanlik = p.Uzmanlik,
                    PersonelTipi = p.PersonelTipi,
                    Email = p.Email,
                    Dahili = p.Dahili,
                    CepTelefonu = p.CepTelefonu,
                    CepTelefonu2 = p.CepTelefonu2,
                    EvTelefonu = p.EvTelefonu,
                    Adres = p.Adres,
                    Semt = p.Semt,
                    DogumTarihi = p.DogumTarihi,
                    Cinsiyet = p.Cinsiyet,
                    MedeniDurumu = p.MedeniDurumu,
                    KanGrubu = p.KanGrubu,
                    EvDurumu = p.EvDurumu,
                    UlasimServis1 = p.UlasimServis1,
                    UlasimServis2 = p.UlasimServis2,
                    Tabldot = p.Tabldot,
                    PersonelAktiflikDurum = p.PersonelAktiflikDurum,
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
                    Resim = p.Resim,
                    DuzenlenmeTarihi = DateTime.Now,

                    // Dropdown listeleri null - ayrı servisle dolduracağız
                    Departmanlar = null,
                    Servisler = null,
                    Unvanlar = null,
                    AtanmaNedenleri = null,
                    HizmetBinalari = null,
                    Iller = null,
                    Ilceler = null,
                    Sendikalar = null
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return data;
        }
    }
}