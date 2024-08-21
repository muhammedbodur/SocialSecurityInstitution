using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IDataAccessLogger
    {
        Task LogActionAsync(string action, string tableName, string beforeData, string afterData);
    }
}
