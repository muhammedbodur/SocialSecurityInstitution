using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IKanalPersonelleriCustomService
    {
        Task<List<PersonellerAltKanallarRequestDto>> GetPersonellerAltKanallarAsync(int hizmetBinasiId);
        Task<List<PersonelAltKanallariRequestDto>> GetPersonelAltKanallariAsync(string tcKimlikNo);
        Task<List<KanalAltIslemleriDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo, int hizmetBinasiId);
    }
}
