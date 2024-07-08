using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Kanal
{
    public class KanalListViewModel
    {
        public List<KanalIslemleriRequestDto> KanalIslemleri { get; set; }
        public List<KanallarDto> Kanallar { get; set; }
    }
}
