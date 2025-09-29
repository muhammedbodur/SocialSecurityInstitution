using SocialSecurityInstitution.BusinessObjectLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
public class SiralarTvListeDto
{
    private KatTipi _katTipi;

    public int SiraId { get; set; }
    public int SiraNo { get; set; }
    public int HizmetBinasiId { get; set; }
    public DateTime? SiraAlisZamani { get; set; }
    public DateTime? IslemBaslamaZamani { get; set; }
    public BeklemeDurum BeklemeDurum { get; set; }
    public string? TcKimlikNo { get; set; }
    public string? AdSoyad { get; set; }
    public int BankoId { get; set; }
    public int BankoNo { get; set; }
    public BankoTipi BankoTipi { get; set; }
    public int TvId { get; set; }
    public string ConnectionId { get; set; }

    public KatTipi KatTipi
    {
        get => _katTipi;
        set
        {
            _katTipi = value;
            KatTipiDisplayName = _katTipi.GetDisplayName(); // DisplayName'i burada güncelliyoruz
        }
    }

    public string KatTipiDisplayName { get; private set; }
}