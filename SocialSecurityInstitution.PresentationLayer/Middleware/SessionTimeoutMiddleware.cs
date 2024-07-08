using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class SessionTimeoutMiddleware
{
    private readonly RequestDelegate _next;

    public SessionTimeoutMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/Logout")) // Logout sayfasında sonsuz döngüyü önlemek için
        {
            if (context.Session.Keys.Contains("LastAccess"))
            {
                var lastAccess = context.Session.GetString("LastAccess");
                if (!string.IsNullOrEmpty(lastAccess))
                {
                    var lastAccessTime = DateTime.Parse(lastAccess);
                    if ((DateTime.Now - lastAccessTime).TotalMinutes >= 2) // Oturum zaman aşımı süresi (örneğin 20 dakika)
                    {
                        // Oturum süresi dolmuş, logout işlemi yapılıyor
                        context.Session.Clear(); // Session verilerini temizle
                        context.Response.Redirect("/Logout"); // Logout sayfasına yönlendir
                        return;
                    }
                }
            }
        }

        // Oturum süresi dolmamış, request pipeline'a devam et
        await _next(context);
    }
}

// Middleware genişletme yöntemi
public static class SessionTimeoutMiddlewareExtensions
{
    public static IApplicationBuilder UseSessionTimeoutMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SessionTimeoutMiddleware>();
    }
}
