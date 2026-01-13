using WFM.EventIngestor.Application.Common.Models;

namespace WFM.EventIngestor.services
{
    public class Test
    {
        public Result<string> GetData()
        {
            
            return Result<string>.Ok("Hello, World!");
        }
    }
}