using Microsoft.AspNetCore.Http;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class LogService : ILogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Context _context;

        public LogService(IHttpContextAccessor httpContextAccessor, Context context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task LogActionAsync(string entityName, DatabaseAction action, string beforeData = null, string afterData = null)
        {
            var logHariciTablolar = new[] { "LoginLogoutLog", "Siralar", "HubConnection", "HubTvConnection" };
            if (!logHariciTablolar.Contains(entityName))
            {
                try
                {
                    string tcKimlikNo = GetUserTcKimlikNo();
                    var log = new DatabaseLog
                    {
                        TcKimlikNo = tcKimlikNo ?? "ANONİM!",
                        DatabaseAction = action,
                        TableName = entityName,
                        ActionTime = DateTime.Now,
                        BeforeData = beforeData ?? string.Empty,
                        AfterData = afterData ?? string.Empty
                    };
                    _context.DatabaseLog.Add(log);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    throw;
                }
            }
            else
            {
                Console.WriteLine($"Tablo: {entityName}");
            }
        }

        private string GetUserTcKimlikNo()
        {
            var claims = _httpContextAccessor.HttpContext?.User?.Claims;
            string TcKimlikNo = claims?.FirstOrDefault(c => c.Type == "TcKimlikNo")?.Value;
            return TcKimlikNo;
        }

        private void HandleException(Exception ex)
        {
            // Bu metot ileride log dosyasına yazdırma işlevi için genişletilebilir.
            Console.WriteLine($"Hata: {ex.Message}");
        }
    }
}
