using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Kanal
{
    public class KanalIslemleriEslestirViewModel
    {
        public List<KanalAltIslemleriRequestDto> KanalAltIslemleri { get; set; }
        public List<KanalAltIslemleriEslestirmeSayisiRequestDto> KanalAltIslemleriEslestirmeSayisi { get; set; }
    }
}
