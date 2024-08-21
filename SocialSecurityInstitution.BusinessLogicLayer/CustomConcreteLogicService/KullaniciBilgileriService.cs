using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class KullaniciBilgileriService : IKullaniciBilgileriService
    {
        private readonly ILogger<KullaniciBilgileriService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KullaniciBilgileriService(ILogger<KullaniciBilgileriService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public KullaniciBilgileriDto GetKullaniciBilgileri()
        {
            var context = _httpContextAccessor.HttpContext;

            var tcKimlikNo = context?.Items["TcKimlikNo"] as string;
            var adSoyad = context?.Items["AdSoyad"] as string;
            var email = context?.Items["Email"] as string;
            var resim = context?.Items["Resim"] as string;
            var sessionId = context?.Items["SessionId"] as string;
            var hizmetBinasiIdString = context?.Items["hizmetBinasiId"] as string;

            if (string.IsNullOrEmpty(tcKimlikNo) || string.IsNullOrEmpty(adSoyad))
            {
                _logger.LogWarning("Kullanıcı bilgileri null veya boş.");
                throw new InvalidOperationException("Kullanıcı bilgileri eksik.");
            }

            int hizmetBinasiId;
            if (!int.TryParse(hizmetBinasiIdString, out hizmetBinasiId))
            {
                _logger.LogWarning("Hizmet Binasi ID geçersiz. Varsayılan olarak 0 kullanılıyor.");
                hizmetBinasiId = 0;
            }

            return new KullaniciBilgileriDto
            {
                TcKimlikNo = tcKimlikNo,
                AdSoyad = adSoyad,
                Email = email,
                Resim = resim,
                SessionID = sessionId,
                HizmetBinasiId = hizmetBinasiId
            };
        }
    }
}
