using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface ILoginControlService
    {
        Task<LoginDto> LoginControlAsync(string TcKimlikNo, string PassWord);
    }
}
