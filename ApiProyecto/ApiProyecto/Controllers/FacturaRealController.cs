using DB;
using DB.Request;
using DB.Response;
using DB.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaRealController : ControllerBase
    {



        private IFacturaServices _facturaService;
        

        public FacturaRealController(IFacturaServices facturaService)
        {
            this._facturaService = facturaService;
        }

        [HttpPost]
        public IActionResult Add(FacturaRequest model)
        {
            Respuesta respuesta = new Respuesta();

            try 
            {
                _facturaService.Add(model);
                respuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }

    }
}
