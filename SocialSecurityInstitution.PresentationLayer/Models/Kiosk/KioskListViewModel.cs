using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Kiosk
{
    public class KioskListViewModel
    {
        public List<KioskIslemGruplariRequestDto> KioskIslemGruplari { get; set; }
        public List<KioskGruplariDto> KioskGruplari { get; set; }
    }
}
