using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class InsertResult
    {
        public bool IsSuccess { get; }
        public object LastPrimaryKeyValue { get; }

        public InsertResult(bool isSuccess, object lastPrimaryKeyValue)
        {
            IsSuccess = isSuccess;
            LastPrimaryKeyValue = lastPrimaryKeyValue;
        }
    }

}
