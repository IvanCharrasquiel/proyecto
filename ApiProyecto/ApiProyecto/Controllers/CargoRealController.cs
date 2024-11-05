using DB;
using DB.Request;
using DB.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoRealController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargoRealController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {

                using (AppDbContext db = _context)
                {
                    var lst = db.Clientes.ToList();
                    oRespuesta.Exito = 1;

                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);

        }

        [HttpPost]
        public IActionResult Add(CargoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (AppDbContext db = _context)
                {
                    Cargo oCargo = new Cargo();
                    oCargo.Cargo1 = oModel.Cargo;
                    db.Add(oCargo);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }


            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CargoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (AppDbContext db = _context)
                {
                    Cargo oCargo = db.Cargos.Find(oModel.IdCargo);
                    oCargo.Cargo1 = oModel.Cargo;
                    db.Entry(oCargo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }


            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (AppDbContext db = _context)
                {
                    Cargo oCargo = db.Cargos.Find(id);
                    db.Remove(oCargo);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }


            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }



    }
}
