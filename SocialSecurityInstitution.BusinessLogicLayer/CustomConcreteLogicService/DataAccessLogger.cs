using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class DataAccessLogger : IDataAccessLogger
    {
        private readonly IDatabaseLogService _databaseLogService;
        private readonly IUserContextService _userContextService;

        public DataAccessLogger(IDatabaseLogService databaseLogService, IUserContextService userContextService)
        {
            _databaseLogService = databaseLogService;
            _userContextService = userContextService;
        }

        public async Task LogActionAsync(string action, string tableName, string beforeData, string afterData)
        {
            var logDto = new DatabaseLogDto
            {
                TcKimlikNo = _userContextService.TcKimlikNo,
                DatabaseAction = Enum.Parse<DatabaseAction>(action),
                TableName = tableName,
                ActionTime = DateTime.Now,
                BeforeData = beforeData,
                AfterData = afterData
            };

            await _databaseLogService.TInsertAsync(logDto);
        }
    }
}
