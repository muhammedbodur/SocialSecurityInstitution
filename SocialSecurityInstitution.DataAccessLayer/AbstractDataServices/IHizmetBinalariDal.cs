using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IHizmetBinalariDal : IGenericDal<HizmetBinalariDto>
    {
        Task<List<HizmetBinalariDto>> GetHizmetBinalariByDepartmanIdAsync(int departmanId);
        Task<HizmetBinalariDepartmanlarDto> GetActiveHizmetBinasiWithDepartmanAsync(int hizmetBinasiId, int departmanId);
        Task<HizmetBinalariDepartmanlarDto> GetHizmetBinasiWithDepartmanByIdAsync(int hizmetBinasiId);
        Task<List<HizmetBinalariDto>> GetAllActiveHizmetBinalariAsync();
        Task<List<HizmetBinalariDepartmanlarDto>> GetHizmetBinalariWithDepartmanDetailsAsync();
        Task<bool> IsHizmetBinasiActiveAsync(int hizmetBinasiId);
    }
}
