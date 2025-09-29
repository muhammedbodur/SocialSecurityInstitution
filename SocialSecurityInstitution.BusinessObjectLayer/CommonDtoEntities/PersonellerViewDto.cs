using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonellerViewDto
    {
        public required string TcKimlikNo { get; set; }
        public int SicilNo { get; set; }
        public required string AdSoyad { get; set; }
        public string? NickName { get; set; }
        public int PersonelKayitNo { get; set; }
        public int KartNo { get; set; }
        public DateTime KartNoAktiflikTarihi { get; set; }
        public DateTime KartNoDuzenlenmeTarihi { get; set; }
        public DateTime KartNoGonderimTarihi { get; set; }
        public IslemBasari KartGonderimIslemBasari { get; set; }
        public int DepartmanId { get; set; }
        public string? DepartmanAdi { get; set; }
        public List<DepartmanlarDto>? Departmanlar { get; set; }
        public int ServisId { get; set; }
        public string? ServisAdi { get; set; }
        public List<ServislerDto>? Servisler { get; set; }
        public int UnvanId { get; set; }
        public string? UnvanAdi { get; set; }
        public List<UnvanlarDto>? Unvanlar { get; set; }
        public string? Gorev { get; set; }
        public string? Uzmanlik { get; set; }
        public int AtanmaNedeniId { get; set; }
        public List<AtanmaNedenleriDto>? AtanmaNedenleri { get; set; }
        public int HizmetBinasiId { get; set; }
        public List<HizmetBinalariDto>? HizmetBinalari { get; set; }
        public PersonelTipi PersonelTipi { get; set; }
        public string? Email { get; set; }
        public int Dahili { get; set; }
        public string? CepTelefonu { get; set; }
        public string? CepTelefonu2 { get; set; }
        public string? EvTelefonu { get; set; }
        public string? Adres { get; set; }
        public int IlId { get; set; }
        public string? IlAdi { get; set; }
        public List<IllerDto>? Iller { get; set; }
        public int IlceId { get; set; }
        public string? IlceAdi { get; set; }
        public List<IlcelerDto>? Ilceler { get; set; }
        public string? Semt { get; set; }
        public DateTime DogumTarihi { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public MedeniDurumu MedeniDurumu { get; set; }
        public KanGrubu KanGrubu { get; set; }
        public EvDurumu EvDurumu { get; set; }
        public int UlasimServis1 { get; set; } = 0;
        public int UlasimServis2 { get; set; } = 0;
        public int Tabldot { get; set; } = 0;
        public PersonelAktiflikDurum PersonelAktiflikDurum { get; set; }
        public string? EmekliSicilNo { get; set; }
        public OgrenimDurumu OgrenimDurumu { get; set; }
        public string? BitirdigiOkul { get; set; }
        public string? BitirdigiBolum { get; set; }
        public int OgrenimSuresi { get; set; }
        public string? Bransi { get; set; }
        public int SendikaId { get; set; }
        public string? SendikaAdi { get; set; }
        public List<SendikalarDto>? Sendikalar { get; set; }
        public SehitYakinligi SehitYakinligi { get; set; }
        public string? EsininAdi { get; set; }
        public EsininIsDurumu EsininIsDurumu { get; set; } = EsininIsDurumu.belirtilmemis;
        public string? EsininUnvani { get; set; }
        public string? EsininIsAdresi { get; set; }
        public int EsininIsIlId { get; set; }
        public string? EsininIsIlAdi { get; set; }
        public int EsininIsIlceId { get; set; }
        public string? EsininIsIlceAdi { get; set; }
        public string? EsininIsSemt { get; set; }
        public string? HizmetBilgisi { get; set; }
        public string? EgitimBilgisi { get; set; }
        public string? ImzaYetkileri { get; set; }
        public string? CezaBilgileri { get; set; }
        public string? EngelBilgileri { get; set; }
        public string? Resim { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
        
    }
}
