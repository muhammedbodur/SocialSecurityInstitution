using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkiaSharp;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class LoginControlService : ILoginControlService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public LoginControlService(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LoginDto> LoginControlAsync(string TcKimlikNo, string PassWord)
        {
            var user = await _context.Personeller.AsNoTracking().FirstOrDefaultAsync(x => x.TcKimlikNo == TcKimlikNo && x.PassWord == PassWord);

            if (user != null)
            {
                var loginDto = new LoginDto
                {
                    TcKimlikNo = user.TcKimlikNo,
                    AdSoyad = user.AdSoyad,
                    Email = user.Email,
                    Resim = user.Resim,
                    HizmetBinasiId = user.HizmetBinasiId
                };

                return loginDto;
            }
            else
            {
                // Kullanıcı bulunamadı veya şifre hatalı
                return null;
            }
        }

        public async Task<LoginLogoutLogDto> FindBySessionIdAsync(string sessionId)
        {
            var log = await _context.LoginLogoutLog
                .AsNoTracking().FirstOrDefaultAsync(log => log.SessionID == sessionId);

            if (log == null) return null;

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
                .ToListAsync(); // LogoutTime nullable olduğu için null kontrolü yapıldı

            foreach (var session in activeSessions)
            {
                session.LogoutTime = DateTime.Now;
                _context.Update(session);
            }

            await _context.SaveChangesAsync();
        }
    }
}
