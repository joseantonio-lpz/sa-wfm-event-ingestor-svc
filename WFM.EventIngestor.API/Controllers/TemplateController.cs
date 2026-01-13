using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFM.EventIngestor.Application.Common.Interfaces;
using WFM.EventIngestor.Domain.Enums;

namespace WFM.EventIngestor.API.Controllers
{
    public class TemplateController : BaseController
    {
        private readonly IAppLogger _logger;
        public TemplateController(IAppLogger logger)
        {
            _logger = logger;
        }

        // GET: api/template
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                var items = new[]
                {
                    new { Id = 1, Name = "Item 1" },
                    new { Id = 2, Name = "Item 2" }
                };
                return OkResp(items);
            }
            catch (Exception ex)
            {
                return InternalError(ex.Message);
            }
        }

        // GET: api/template/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFoundResp("Item not found");

                var item = new { Id = id, Name = $"Item {id}" };
                return OkResp(item);
            }
            catch (Exception ex)
            {
                return InternalError(ex.Message);
            }
        }

        // POST: api/template
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] dynamic model)
        {
            try
            {
                if (model == null) return BadReq("Invalid payload");

                model.Id = 999; // simula creación
                return CreatedResp(model, nameof(GetById), new { id = model.Id });
            }
            catch (Exception ex)
            {
                return InternalError(ex.Message);
            }
        }

        // PUT: api/template/5
        [HttpPut("{id:int}")]
        [AllowAnonymous]
        public IActionResult Update(int id, [FromBody] dynamic model)
        {
            try
            {
                if (id <= 0) return NotFoundResp("Item not found");
                if (model == null) return BadReq("Invalid payload");

                model.Id = id; // simula actualización
                return OkResp(model);
            }
            catch (Exception ex)
            {
                return InternalError(ex.Message);
            }
        }

        // DELETE: api/template/5
        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0) return NotFoundResp("Item not found");

                return OkResp(new { message = $"Item {id} deleted" });
            }
            catch (Exception ex)
            {
                return InternalError(ex.Message);
            }
        }

        /// <summary>
        /// Registra un log en el sistema
        /// </summary>
        [HttpPost("SaveLog")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveLog()
        {
            await _logger.LogAsync(
                ObjectResultEnum.LogLevel.Info,
                "",
                "Ejecutando DoSomething",
                username: "hector",
                callsite: "SaveLog",
                thread: Thread.CurrentThread.ManagedThreadId.ToString()
            );
            return OkResp(new { message = "Log guardado correctamente" });
        }
       
        /// <summary>
        /// Valida un usuario en el sistema
        /// </summary>
        [HttpGet("validate")]
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public IActionResult ValidateUser()
        {
            // Aquí solo entra si pasó la autenticación básica
            return OkResp(new { message = "Usuario validado correctamente con BasicAuthentication" });
        }
    }
}
