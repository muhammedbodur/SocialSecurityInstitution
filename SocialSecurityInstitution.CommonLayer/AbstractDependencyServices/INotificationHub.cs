using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.CommonLayer.AbstractDependencyServices
{
    public interface INotificationHub
    {
        Task SendNotificationToAll(string message);
    }
}
