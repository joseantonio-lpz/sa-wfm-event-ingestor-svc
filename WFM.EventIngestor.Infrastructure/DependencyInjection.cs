using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WFM.EventIngestor.Application.Common.Interfaces;
using WFM.EventIngestor.Application.Interfaces;
using WFM.EventIngestor.Infrastructure.Kafka;
using WFM.EventIngestor.Infrastructure.Logging;
using WFM.EventIngestor.Infrastructure.Persistence;

namespace WFM.EventIngestor.Infrastructure
{
    public static class DependencyInjection
    {        
        /// <summary>
        /// Configura los servicios de infraestructura.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Obtiene la cadena de conexión de appsettings.json           
            services.AddScoped<IConnectionFactory>(_ => new OracleConnectionFactory(
                    configuration.GetConnectionString("OracleDb") 
                        ?? throw new InvalidOperationException("La cadena de conexión 'OracleDb' no está configurada.")
                )
            );         

            //Registra el repositorio de logs
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddSingleton<FileLogger>();
            services.AddScoped<DbLogger>();
            services.AddScoped<IAppLogger, CompositeLogger>();

            services.AddScoped<IKafkaProducerService, KafkaProducerService>();

            return services;
        }
    }
}