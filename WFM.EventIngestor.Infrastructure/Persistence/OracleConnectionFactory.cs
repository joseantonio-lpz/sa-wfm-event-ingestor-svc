using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using WFM.EventIngestor.Application.Common.Interfaces;


namespace WFM.EventIngestor.Infrastructure.Persistence
{
    public class OracleConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public OracleConnectionFactory(string connectionString) => _connectionString = connectionString;

        /// <summary>
        /// Crea una nueva conexión a la base de datos Oracle.
        /// </summary>
        /// <returns></returns>
        public DbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
