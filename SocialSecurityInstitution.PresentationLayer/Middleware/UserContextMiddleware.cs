using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using System.Security.Claims;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserContextMiddleware> _logger;

    public UserContextMiddleware(RequestDelegate next, ILogger<UserContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IUserContextService userContextService)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            userContextService.TcKimlikNo = context.User.FindFirst("TcKimlikNo")?.Value;
            userContextService.AdSoyad = context.User.FindFirst(ClaimTypes.Name)?.Value;
            userContextService.Email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            userContextService.SessionID = context.User.FindFirst("SessionID")?.Value;
            userContextService.HizmetBinasiId = int.TryParse(context.User.FindFirst("HizmetBinasiId")?.Value, out var hizmetBinasiId)
                ? hizmetBinasiId
                : 0;

            if (string.IsNullOrEmpty(userContextService.TcKimlikNo))
            {
                throw new InvalidOperationException("TcKimlikNo null veya boş geldi.");
            }
            _logger.LogInformation($"UserContextService dolduruldu: TcKimlikNo={userContextService.TcKimlikNo}, AdSoyad={userContextService.AdSoyad}, Email={userContextService.Email}, SessionID={userContextService.SessionID}, HizmetBinasiId={userContextService.HizmetBinasiId}");
        }

        await _next(context);
    }
}
