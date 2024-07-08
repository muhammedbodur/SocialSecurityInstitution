namespace SocialSecurityInstitution.PresentationLayer.Models.Kiosk
{
    public class KioskSiraGosterViewModel
    {
        public int SiraNo { get; set; }
        public string SgmAdi { get; set; }
        public string Mesaj { get; set; }
        public int IslemDurum { get; set; }
        public string? Error { get; set; }
    }
}
