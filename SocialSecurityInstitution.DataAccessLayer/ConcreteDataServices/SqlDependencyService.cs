using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient.Base.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class SqlDependencyService<TEntity> where TEntity : class, new()
    {
        private readonly string _connectionString;
        private SqlTableDependency<TEntity> _tableDependency;

        public event Action OnDataChange;

        public SqlDependencyService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void StartListening()
        {
            _tableDependency = new SqlTableDependency<TEntity>(_connectionString);
            _tableDependency.OnChanged += OnDependencyChange;
            _tableDependency.OnError += OnDependencyError;
            _tableDependency.Start();
        }

        private void OnDependencyChange(object sender, RecordChangedEventArgs<TEntity> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                OnDataChange?.Invoke();
            }
        }

        private void OnDependencyError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"SQL Table Dependency error: {e.Error.Message}");
        }

        public void StopListening()
        {
            _tableDependency.Stop();
        }
    }
}
