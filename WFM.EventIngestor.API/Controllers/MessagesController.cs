
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WFM.EventIngestor.Application.DTOs;
using WFM.EventIngestor.Application.Interfaces;
using WFM.EventIngestor.Infrastructure.Configuration;

namespace WFM.EventIngestor.API.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : BaseController
    {
        private readonly IKafkaProducerService _producer;
        public MessagesController(IKafkaProducerService producer)
        {
            _producer = producer;
        }

        [HttpPost("social-content")]
        public async Task<IActionResult> SendSocialContent(
            [FromBody] SocialContentMessage message,
            [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.SocialContent, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }

        [HttpPost("sinergia-flow")]
        public async Task<IActionResult> SendSinergiaFlow(
            [FromBody] SinergiaFlowMessage message,
            [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.SinergiaFlow, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }

        [HttpPost("adsa-store")]
        public async Task<IActionResult> SendAdsaStore(
            [FromBody] AdsaStoreMessage message,
            [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.AdsaStore, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        } 
        
        [HttpPost("exception-firma")]
        public async Task<IActionResult> SendExceptionFirma(
            [FromBody] ExceptionFirmaMessage message,
            [FromServices] IOptions<KafkaSettings> kafkaSettings)
        {
            var resp = await _producer.SendMessageAsync(kafkaSettings.Value.Topics.ExceptionFirma, message);
            return resp.IsSuccess
                ? OkResp(new { message = resp.Data })
                : BadReq(resp.Message);
        }
    }
}