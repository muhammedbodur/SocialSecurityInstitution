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
        public required string AdSoyad { get; set; }
        public string? NickName { get; set; }
        public int SicilNo { get; set; }
        public int DepartmanId { get; set; }
        public Departmanlar? Departman { get; set; }
        public int ServisId { get; set; }
        public Servisler? Servis { get; set; }
        public int UnvanId { get; set; }
        public Unvanlar? Unvan { get; set; }
        public string? Gorev { get; set; }
        public string? Uzmanlik { get; set; }
        public AtanmaNedenleri? AtanmaNedeni { get; set; }
        public HizmetBinalari? HizmetBinasi { get; set; }
        public PersonelTipi PersonelTipi { get; set; }
        public required string Email { get; set; }
        public int Dahili { get; set; }
        public string? CepTelefonu { get; set; }
        public string? CepTelefonu2 { get; set; }
        public string? EvTelefonu { get; set; }
        public string? Adres { get; set; }
        public Iller? Il { get; set; }
        public Ilceler? Ilce { get; set; }
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
        public Sendikalar? Sendika { get; set; }
        public SehitYakinligi SehitYakinligi { get; set; }
        public string? EsininAdi { get; set; }
        public EsininIsDurumu EsininIsDurumu { get; set; }
        public string? EsininUnvani { get; set; }
        public string? EsininIsAdresi { get; set; }
        public Iller? EsininIsIl { get; set; }
        public Ilceler? EsininIsIlce { get; set; }
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
