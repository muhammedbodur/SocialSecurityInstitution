using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.SqlDependencyServices
{
    public interface ISubscribeTableDependency
    {
        void SubscribeTablesDependency(string connectionString);
    }
}