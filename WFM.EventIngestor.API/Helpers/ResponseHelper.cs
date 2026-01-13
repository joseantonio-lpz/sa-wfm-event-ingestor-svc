using Microsoft.AspNetCore.Mvc;

namespace WFM.EventIngestor.API.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult OkResponse(object data)
        {
            return new Microsoft.AspNetCore.Mvc.OkObjectResult(new { success = true, data });
        }

        public static IActionResult CreatedResponse(object data, string actionName, object routeValues)
        {
            return new Microsoft.AspNetCore.Mvc.CreatedAtActionResult(
                actionName,
                null,
                routeValues,
                new { success = true, data });
        }

        public static IActionResult BadRequestResponse(string error)
        {
            return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new { success = false, error });
        }

        public static IActionResult NotFoundResponse(string? error = null)
        {
            return new Microsoft.AspNetCore.Mvc.NotFoundObjectResult(new { success = false, error = error ?? "Not Found" });
        }

        public static IActionResult InternalErrorResponse(string? error = null)
        {
            return new Microsoft.AspNetCore.Mvc.ObjectResult(new { success = false, error = error ?? "Internal server error" })
            {
                StatusCode = 500
            };
        }
    }
}
