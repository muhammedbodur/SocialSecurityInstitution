// IKanalPersonelleriDal.cs - Güncellenen interface
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IKanalPersonelleriDal : IGenericDal<KanalPersonelleriDto>
    {
        Task<List<KanalAltIslemleriDto>> GetPersonelAltKanallarEslesmeyenlerAsync(string tcKimlikNo, int hizmetBinasiId);
        Task<List<PersonelAltKanallariRequestDto>> GetPersonelAltKanallariAsync(string tcKimlikNo);
        Task<List<PersonellerAltKanallarRequestDto>> GetPersonellerAltKanallarAsync(int hizmetBinasiId);
        Task<List<PersonelAltKanallariRequestDto>> GetKanalPersonelleriWithHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<KanalPersonelleriViewRequestDto>> GetKanalAltPersonelleriAsync(int kanalAltIslemId);
    }
}