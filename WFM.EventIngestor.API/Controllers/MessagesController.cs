
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WFM.EventIngestor.Application.DTOs;
using WFM.EventIngestor.Application.Interfaces;
using WFM.EventIngestor.Infrastructure.Configuration;

namespace WFM.EventIngestor.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class MessagesController : BaseController
    {
        private readonly IKafkaProducerService _producer;
        public MessagesController(IKafkaProducerService producer)
        {
            _producer = producer;
        }

        [HttpPost("entity-created")]
        public async Task<IActionResult> SendEntityCreated(
            [FromBody] EntityCreatedMessage message,
            [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.EntityCreated, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }

        [HttpPost("task-rejected")]
        public async Task<IActionResult> SendTaskRejected(
            [FromBody] TaskRejectedMessage message,
            [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.TaskRejected, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }
    }
}