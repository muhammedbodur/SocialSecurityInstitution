using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IKanalIslemleriDal : IGenericDal<KanalIslemleriDto>
    {
        Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<KanalIslemleriRequestDto> GetKanalIslemleriByIdWithDetailsAsync(int kanalIslemId);
    }
}
