using System.Data;
using Oracle.ManagedDataAccess.Client;
using WFM.EventIngestor.Application.Common.Interfaces;
using WFM.EventIngestor.Application.Common.Models;
using WFM.EventIngestor.Application.Interfaces;
using static WFM.EventIngestor.Domain.Enums.ObjectResultEnum;

namespace WFM.EventIngestor.Infrastructure.Persistence
{
    public class LogRepository : ILogRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public LogRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Guarda una entrada de log en la base de datos.
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        public async Task SaveLogEntryAsync( LogLevel logLevel,
            string logger,
            string message,
            string? exception = null,
            string? callsite = null,
            string? thread = null,
            string? username = null,
            string? properties = null)
        {           
            try
            {
                 // 🔹 Crear conexión desde el factory
                await using var conn = _connectionFactory.CreateConnection();
                await conn.OpenAsync();

                // 🔹 Crear comando
                await using var cmd = conn.CreateCommand();
                var oracleCmd = (OracleCommand)cmd;
                oracleCmd.CommandType = CommandType.StoredProcedure;
                oracleCmd.CommandText = "PKG_APP_LOG.INSERT_LOG";
                oracleCmd.BindByName = true;

                // 🔹 Establecer parámetros
                oracleCmd.Parameters.Add("p_log_level", OracleDbType.Varchar2).Value = logLevel.ToString();
                oracleCmd.Parameters.Add("p_logger", OracleDbType.Varchar2).Value = logger;
                oracleCmd.Parameters.Add("p_message", OracleDbType.Varchar2).Value = message;
                oracleCmd.Parameters.Add("p_exception", OracleDbType.Varchar2).Value = exception ?? (object)DBNull.Value;
                oracleCmd.Parameters.Add("p_callsite", OracleDbType.Varchar2).Value = callsite ?? (object)DBNull.Value;
                oracleCmd.Parameters.Add("p_thread", OracleDbType.Varchar2).Value = thread ?? (object)DBNull.Value;
                oracleCmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username ?? (object)DBNull.Value;
                oracleCmd.Parameters.Add("p_properties", OracleDbType.Varchar2).Value = properties ?? (object)DBNull.Value;

                // 🔹 Ejecutar comando
                await oracleCmd.ExecuteNonQueryAsync();   
            }
            catch (Exception)
            {               
            }                    
        }
    }
}