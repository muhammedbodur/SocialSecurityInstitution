using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using NToastNotify;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SocialSecurityInstitution.PresentationLayer.Middleware
{

    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public SessionValidationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var sessionId = context.User.FindFirst("SessionID")?.Value;
                var tcKimlikNo = context.User.FindFirst("TcKimlikNo")?.Value;

                if (!string.IsNullOrEmpty(sessionId) && !string.IsNullOrEmpty(tcKimlikNo))
                {
                    // Scoped servisi almak için scope oluşturun
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var personelCustomService = scope.ServiceProvider.GetRequiredService<IPersonelCustomService>();

                        // Personel bilgilerini veritabanından al
                        var personel = await personelCustomService.TGetByTcKimlikNoAsync(tcKimlikNo);

                        if (personel == null || personel.SessionID != sessionId)
                        {
                            // Geçersiz oturum, kullanıcının oturumunu sonlandır
                            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                            context.Response.Cookies.Delete(".AspNetCore.Cookies");

                            context.Session.SetString("LoginErrorMessage", "Kullanıcınızla Başka Bir Browser/Bilgisayardan Giriş Yapıldı!");
                            context.Response.Redirect("/Login");
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
