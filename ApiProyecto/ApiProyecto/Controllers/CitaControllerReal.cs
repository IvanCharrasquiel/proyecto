using DB.Request;
using DB.Response;
using DB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity.Infrastructure;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaControllerReal : ControllerBase
    {
        private readonly ICitaRepository _citaServices;

        public CitaControllerReal(ICitaRepository citaService)
        {
            _citaServices = citaService;
        }

        // POST: api/Cita
        [HttpPost]
        [HttpPost]
        public IActionResult Add(CitaRequest model)
        {
            var respuesta = new Respuesta();

            try
            {
                _citaServices.Add(model);
                respuesta.Exito = 1;
                respuesta.Mensaje = "Cita creada exitosamente.";
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                // Extrae todos los mensajes de las excepciones internas
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No additional details.";
                var deeperException = ex.InnerException?.InnerException;

                while (deeperException != null)
                {
                    innerExceptionMessage += " -> " + deeperException.Message;
                    deeperException = deeperException.InnerException;
                }

                respuesta.Mensaje = $"Error inesperado: {ex.Message}. Detalles: {innerExceptionMessage}";
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
            }
        }

    }
}
