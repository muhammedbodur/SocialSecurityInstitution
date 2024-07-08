using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using Microsoft.Identity.Client;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class LoginLogoutLogService : ILoginLogoutLogService
    {
        private readonly ILoginLogoutLogDal _loginLogoutLogDal;

        public LoginLogoutLogService(ILoginLogoutLogDal loginLogoutLogDal)
        {
            _loginLogoutLogDal = loginLogoutLogDal;
        }

        public async Task<bool> TContainsAsync(LoginLogoutLogDto dto)
        {
            return await _loginLogoutLogDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _loginLogoutLogDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(LoginLogoutLogDto dto)
        {
            return await _loginLogoutLogDal.DeleteAsync(dto);
        }

        public async Task<List<LoginLogoutLogDto>> TGetAllAsync()
        {
            return await _loginLogoutLogDal.GetAllAsync();
        }

        public async Task<LoginLogoutLogDto> TGetByIdAsync(int id)
        {
            return await _loginLogoutLogDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(LoginLogoutLogDto dto)
        {
            return await _loginLogoutLogDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(LoginLogoutLogDto dto)
        {
            return await _loginLogoutLogDal.UpdateAsync(dto);
        }
    }
}
