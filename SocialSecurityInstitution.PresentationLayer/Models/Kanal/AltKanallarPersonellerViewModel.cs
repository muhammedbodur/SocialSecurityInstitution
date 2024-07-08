using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Kanal
{
    public class AltKanallarPersonellerViewModel
    {
        public List<PersonellerDto> PersonellerList { get; set;}
        public List<KanalAltIslemleriRequestDto> KanalAltIslemleriList { get; set;}
    }
}
