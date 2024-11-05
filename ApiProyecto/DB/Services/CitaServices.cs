using DB.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Services
{
    public class CitaServices : ICitaServices
    {
        private readonly AppDbContext _context;
        // Inyecta AppDbContext en el constructor
        public CitaServices(AppDbContext context)
        {
            _context = context;
        }



        public void Add(CitaRequest model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var cita = new Cita
                    {
                        IdCliente = model.IdCliente,
                        IdEmpleado = model.IdEmpleado,
                        Fecha = DateTime.Now,
                        IdHorario = model.IdHorario,
                    };

                    _context.Cita.Add(cita);
                    _context.SaveChanges();

                    // Obtener el IdCita generado automáticamente
                    int idCitaGenerado = cita.IdCita;

                    foreach (var modelFactura in model.lfactura)
                    {
                        var factura = new DB.Factura
                        {
                            FechaEmision = DateTime.Now,
                            MontoTotal = modelFactura.MontoTotal,
                            IdCita = idCitaGenerado  // Asignar el IdCita generado
                        };

                        _context.Facturas.Add(factura);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Ocurrió un error en la inserción: " + ex.Message, ex);
                }
            }
        }





    }
}
