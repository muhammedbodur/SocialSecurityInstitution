using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Kanal
{
    public class KanalAltListViewModel
    {
        public List<KanalAltIslemleriRequestDto> KanalAltIslemleri { get; set; }
        public List<KanallarAltDto> KanallarAlt { get; set; }
    }
}
