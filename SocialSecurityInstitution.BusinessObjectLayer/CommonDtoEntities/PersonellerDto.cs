using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonellerDto
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
        public int ServisId { get; set; }
        public int UnvanId { get; set; }
        public string? Gorev { get; set; }
        public string? Uzmanlik { get; set; }
        public int AtanmaNedeniId { get; set; }
        public int HizmetBinasiId { get; set; }
        public PersonelTipi PersonelTipi { get; set; }
        public required string Email { get; set; }
        public int Dahili { get; set; }
        public string? CepTelefonu { get; set; }
        public string? CepTelefonu2 { get; set; }
        public string? EvTelefonu { get; set; }
        public string? Adres { get; set; }
        public IllerDto? Il { get; set; }
        public IlcelerDto? Ilce { get; set; }
        public string? Semt { get; set; }
        public DateTime DogumTarihi { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public MedeniDurumu MedeniDurumu { get; set; }
        public KanGrubu KanGrubu { get; set; }
        public EvDurumu EvDurumu { get; set; }
        public int UlasimServis1 { get; set; }
        public int UlasimServis2 { get; set; }
        public int Tabldot { get; set; }
        public PersonelAktiflikDurum PersonelAktiflikDurum { get; set; }
        public string? EmekliSicilNo { get; set; }
        public OgrenimDurumu OgrenimDurumu { get; set; }
        public string? BitirdigiOkul { get; set; }
        public string? BitirdigiBolum { get; set; }
        public int OgrenimSuresi { get; set; }
        public string? Bransi { get; set; }
        public SendikalarDto? Sendika { get; set; }
        public SehitYakinligi SehitYakinligi { get; set; }
        public string? EsininAdi { get; set; }
        public EsininIsDurumu EsininIsDurumu { get; set; }
        public string? EsininUnvani { get; set; }
        public string? EsininIsAdresi { get; set; }
        public IllerDto? EsininIsIl { get; set; }
        public IlcelerDto? EsininIsIlce { get; set; }
        public string? EsininIsSemt { get; set; }
        public string? HizmetBilgisi { get; set; }
        public string? EgitimBilgisi { get; set; }
        public string? ImzaYetkileri { get; set; }
        public string? CezaBilgileri { get; set; }
        public string? EngelBilgileri { get; set; }
        public string? Resim { get; set; }
        public string PassWord { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }

        public ICollection<BankolarKullaniciDto> BankolarKullanici { get; set; }
        public ICollection<KanalPersonelleriDto> KanalPersonelleri { get; set; }
    }
}
