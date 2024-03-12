using Microsoft.EntityFrameworkCore;
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
        public async Task<LoginDto> LoginControlAsync(string TcKimlikNo, string PassWord)
        {
            using var context = new Context();
            var user = await context.Personeller.FirstOrDefaultAsync(x => x.TcKimlikNo == TcKimlikNo && x.PassWord == PassWord);

            if (user != null)
            {
                var loginDto = new LoginDto
                {
                    TcKimlikNo = user.TcKimlikNo,
                    AdSoyad = user.AdSoyad,
                    Email = user.Email,
                };

                return loginDto;
            }
            else
            {
                // Kullanıcı bulunamadı veya şifre hatalı
                return null;
            }
        }

    }
}
