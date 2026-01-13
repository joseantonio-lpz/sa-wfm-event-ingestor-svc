using System.Data.Common;

namespace WFM.EventIngestor.Application.Common.Interfaces
{
    public interface IConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
