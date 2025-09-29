using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class LoginLogoutLogDal : GenericRepository<LoginLogoutLog, LoginLogoutLogDto>, ILoginLogoutLogDal
    {
        public LoginLogoutLogDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<LoginLogoutLogDto> FindBySessionIdAsync(string sessionId)
        {
            var log = await _context.LoginLogoutLog
                .AsNoTracking()
                .FirstOrDefaultAsync(log => log.SessionID == sessionId);

            if (log == null)
            {
                return null;
            }

            return new LoginLogoutLogDto
            {
                Id = log.Id,
                TcKimlikNo = log.TcKimlikNo,
                LoginTime = log.LoginTime,
                LogoutTime = log.LogoutTime,
                SessionID = log.SessionID
            };
        }

        public async Task LogoutPreviousSessionsAsync(string tcKimlikNo)
        {
            var activeSessions = await _context.LoginLogoutLog
                .Where(log => log.TcKimlikNo == tcKimlikNo && log.LogoutTime == null)
                .ToListAsync();

            foreach (var session in activeSessions)
            {
                session.LogoutTime = DateTime.Now;
                _context.Update(session);
            }

            await _context.SaveChangesAsync();
        }
    }
}
