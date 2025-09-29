using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Personeller
    {
        [Key]
        public string TcKimlikNo { get; set; } = string.Empty;
        public int SicilNo { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string? NickName { get; set; }
        public int PersonelKayitNo { get; set; }
        public int KartNo { get; set; }
        public DateTime? KartNoAktiflikTarihi { get; set; }
        public DateTime? KartNoDuzenlenmeTarihi { get; set; }
        public DateTime? KartNoGonderimTarihi { get; set; }
        public IslemBasari KartGonderimIslemBasari { get; set; }
        public int DepartmanId { get; set; }
        [ForeignKey("DepartmanId")]
        public Departmanlar? Departman { get; set; }
        public int ServisId { get; set; }
        [ForeignKey("ServisId")]
        public Servisler? Servis { get; set; }
        public int UnvanId { get; set; }
        [ForeignKey("UnvanId")]
        public Unvanlar? Unvan { get; set; }
        public string? Gorev { get; set; }
        public string? Uzmanlik { get; set; }
        public int AtanmaNedeniId { get; set; }
        [ForeignKey("AtanmaNedeniId")]
        public AtanmaNedenleri? AtanmaNedeni { get; set; }
        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public HizmetBinalari? HizmetBinasi { get; set; }
        public PersonelTipi PersonelTipi { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Dahili { get; set; }
        public string? CepTelefonu { get; set; }
        public string? CepTelefonu2 { get; set; }
        public string? EvTelefonu { get; set; }
        public string? Adres { get; set; }
        public int IlId { get; set; }
        [ForeignKey("IlId")]
        public Iller? Il { get; set; }
        public int IlceId { get; set; }
        [ForeignKey("IlceId")]
        public Ilceler? Ilce { get; set; }
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
        [ForeignKey("SendikaId")]
        public Sendikalar? Sendika { get; set; }
        public SehitYakinligi SehitYakinligi { get; set; }
        public string? EsininAdi { get; set; }
        public EsininIsDurumu EsininIsDurumu { get; set; } = EsininIsDurumu.belirtilmemis;
        public string? EsininUnvani { get; set; }
        public string? EsininIsAdresi { get; set; }
        public int EsininIsIlId { get; set; } = 0;
        [ForeignKey("EsininIsIlId")]
        public Iller? EsininIsIl { get; set; } 
        public int EsininIsIlceId { get; set; } = 0;
        [ForeignKey("EsininIsIlceId")]
        public Ilceler? EsininIsIlce { get; set; }
        public string? EsininIsSemt { get; set; }
        public string? HizmetBilgisi { get; set; }
        public string? EgitimBilgisi { get; set; }
        public string? ImzaYetkileri { get; set; }
        public string? CezaBilgileri { get; set; }
        public string? EngelBilgileri { get; set; }
        public string? Resim { get; set; }
        public string PassWord { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public string? SessionID { get; set; }

        public ICollection<BankolarKullanici> BankolarKullanici { get; set; }
        public ICollection<KanalPersonelleri> KanalPersonelleri { get; set; }
        public ICollection<DatabaseLog> DatabaseLog { get; set; }
        public HubConnection? HubConnection { get; set; }

        public Personeller() => PassWord = TcKimlikNo;
    }
}
