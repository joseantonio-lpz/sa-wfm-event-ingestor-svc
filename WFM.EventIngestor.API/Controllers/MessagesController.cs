
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

        [HttpPost("form-submit")]
        public async Task<IActionResult> SendFormSubmit(
          [FromBody] FormSubmitMessage message,
          [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.FormSubmit, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }

        [HttpPost("task-started")]
        public async Task<IActionResult> SendTaskStarted(
         [FromBody] TaskStartedMessage message,
         [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.TaskStarted, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }

        [HttpPost("task-completed")]
        public async Task<IActionResult> SendTaskCompleted(
        [FromBody] TaskCompletedMessage message,
        [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.TaskCompleted, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }

        [HttpPost("task-assigned")]
        public async Task<IActionResult> SendTaskAssigned(
        [FromBody] TaskAssignedMessage message,
        [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.TaskAssigned, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }
    }
}