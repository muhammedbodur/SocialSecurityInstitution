using SocialSecurityInstitution.PresentationLayer.Services.AbstractPresentationServices;
using System.Security.Claims;

namespace SocialSecurityInstitution.PresentationLayer.Services.ConcretePresentationServices
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTcKimlikNo()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue("TcKimlikNo");
        }

        public string GetAdSoyad()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue("AdSoyad");
        }

        public string GetResim()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue("Resim");
        }

        public string GetEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }

        public int GetHizmetBinasiId()
        {
            var value = _httpContextAccessor.HttpContext.User.FindFirstValue("HizmetBinasiId");
            return int.TryParse(value, out int result) ? result : 0;
        }

        public string GetSessionId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue("SessionID");
        }
    }
}
