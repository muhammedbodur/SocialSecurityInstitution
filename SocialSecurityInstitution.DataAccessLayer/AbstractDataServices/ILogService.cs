using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface ILogService
    {
        Task LogActionAsync(string entityName, DatabaseAction action, string beforeData = null, string afterData = null);
    }
}

