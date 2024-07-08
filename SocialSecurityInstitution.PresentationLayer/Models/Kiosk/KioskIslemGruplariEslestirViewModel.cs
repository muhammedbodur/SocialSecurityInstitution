using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Kiosk
{
    public class KioskIslemGruplariEslestirViewModel
    {
        public List<KanalAltIslemleriRequestDto> KioskIslemGruplariKanalAltIslemleri { get; set; }
        public List<KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto> KioskIslemGruplariAltIslemlerEslestirmeSayisi { get; set; }
    }
}
