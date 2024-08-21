using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.CommonLayer.AbstractDependencyServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using TableDependency.SqlClient;
using System;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.CommonLayer.ConcreteDependencyServices
{
    public class LoginLogoutLogDependencyService : ILoginLogoutLogDependencyService
    {
        private readonly IConfiguration _configuration;
        private SqlTableDependency<LoginLogoutLog> _tableDependency;
        private readonly INotificationHub _notificationHub;

        public LoginLogoutLogDependencyService(IConfiguration configuration, INotificationHub notificationHub)
        {
            _configuration = configuration;
            _notificationHub = notificationHub;
        }

        public void SubscribeTableDependency()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            _tableDependency = new SqlTableDependency<LoginLogoutLog>(connectionString);
            _tableDependency.OnChanged += TableDependency_OnChanged;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(LoginLogoutLog)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<LoginLogoutLog> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var log = e.Entity;

                if (log.LogoutTime != null)
                {
                    await _notificationHub.SendNotificationToAll($"User {log.TcKimlikNo} has logged out.");
                }
                else if (log.LoginTime != null)
                {
                    await _notificationHub.SendNotificationToAll($"User {log.TcKimlikNo} has logged in.");
                }
            }
        }

        public void StopTableDependency()
        {
            _tableDependency?.Stop();
        }
    }
}
