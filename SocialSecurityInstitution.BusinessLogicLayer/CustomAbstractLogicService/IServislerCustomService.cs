using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IServislerCustomService
    {
        Task<List<ServislerDto>> GetServislerByDepartmanIdAsync(int departmanId);
        Task<List<ServislerDto>> GetActiveServislerByDepartmanIdAsync(int departmanId);
    }
}
