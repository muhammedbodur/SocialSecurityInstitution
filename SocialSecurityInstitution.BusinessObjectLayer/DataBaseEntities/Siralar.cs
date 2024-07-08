using SocialSecurityInstitution.BusinessObjectLayer;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Siralar
{
    [Key]
    public int SiraId { get; set; }
    public int Sira { get; set; }
    public int? BankoIslemId { get; set; }
    [ForeignKey("BankoIslemId")]
    public BankoIslemleri BankoIslem { get; set; }
    public int KanalAltIslemId { get; set; }
    [ForeignKey("KanalAltIslemId")]
    public KanalAltIslemleri KanalAltIslem { get; set; }
    public DateTime SiraAlisZamani { get; set; } = DateTime.Now;
    public DateTime? IslemBaslamaZamani { get; set; }
    public DateTime? IslemBitisZamani { get; set; }
    public BeklemeDurum BeklemeDurum { get; set; } = 0;

    private DateTime _siraAlisTarihi;

    [NotMapped]
    public DateTime SiraAlisTarihi
    {
        get { return _siraAlisTarihi; }
        set { _siraAlisTarihi = value; }
    }

    public Siralar()
    {
        _siraAlisTarihi = DateTime.Now.Date; // Nesne oluşturulduğunda varsayılan olarak atanır
    }
}