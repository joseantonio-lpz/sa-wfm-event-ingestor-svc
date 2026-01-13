using Microsoft.AspNetCore.Mvc;
using WFM.EventIngestor.API.Helpers;

namespace WFM.EventIngestor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        // Helpers de respuesta
        protected IActionResult OkResp(object data) => ResponseHelper.OkResponse(data);
        protected IActionResult CreatedResp(object data, string actionName, object routeValues) =>
            ResponseHelper.CreatedResponse(data, actionName, routeValues);
        protected IActionResult BadReq(string message) => ResponseHelper.BadRequestResponse(message);
        protected IActionResult NotFoundResp(string? message = null) => ResponseHelper.NotFoundResponse(message);
        protected IActionResult InternalError(string? message = null) => ResponseHelper.InternalErrorResponse(message);

        // Helper para claims del usuario
        protected string? GetUserId() => User?.FindFirst("sub")?.Value;
        protected string? GetUserName() => User?.Identity?.Name;
    }
}
