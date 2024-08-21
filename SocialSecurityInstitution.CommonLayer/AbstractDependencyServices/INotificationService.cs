using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.CommonLayer.AbstractDependencyServices
{
    public interface INotificationService
    {
        Task SendNotificationToUser(string tcKimlikNo, object data);
        Task SendNotificationToAll(object data);
    }
}
