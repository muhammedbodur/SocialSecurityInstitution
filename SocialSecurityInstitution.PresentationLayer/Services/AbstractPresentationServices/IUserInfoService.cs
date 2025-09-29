namespace SocialSecurityInstitution.PresentationLayer.Services.AbstractPresentationServices
{
    public interface IUserInfoService
    {
        string GetTcKimlikNo();
        string GetAdSoyad();
        string GetResim();
        string GetEmail();
        int GetHizmetBinasiId();
        string GetSessionId();
    }
}
