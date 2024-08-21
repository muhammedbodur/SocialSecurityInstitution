using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class DatabaseLogService : IDatabaseLogService
    {
        private readonly IDatabaseLogDal _databaseLogDal;

        public DatabaseLogService(IDatabaseLogDal databaseLogDal)
        {
            _databaseLogDal = databaseLogDal;
        }

        public Task<bool> TContainsAsync(DatabaseLogDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<int> TCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> TDeleteAsync(DatabaseLogDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<DatabaseLogDto>> TGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseLogDto> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<InsertResult> TInsertAsync(DatabaseLogDto dto)
        {
            return await _databaseLogDal.InsertAsync(dto);
        }

        public Task<bool> TUpdateAsync(DatabaseLogDto dto)
        {
            throw new NotImplementedException();
        }
    }

}
