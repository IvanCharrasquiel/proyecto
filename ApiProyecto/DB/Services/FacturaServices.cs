using DB.Request;

namespace DB.Services
{
    public class FacturaServices : IFacturaServices
    {
        private readonly AppDbContext _context;
        // Inyecta AppDbContext en el constructor
        public FacturaServices(AppDbContext context)
        {
            _context = context;
        }
        public void Add(FacturaRequest model)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var factura = new Factura
                    {
                        FechaEmision = DateTime.Now,
                        MontoTotal = model.DetalleFacturas.Sum(d => d.CantidadServicio * d.PrecioServicio),
                        IdCita = model.IdCita
                    };

                    _context.Facturas.Add(factura);
                    _context.SaveChanges();

                    foreach (var modelDetalle in model.DetalleFacturas)
                    {
                        var detalleFactura = new DB.DetalleFactura();
                        detalleFactura.Subtotal = modelDetalle.Subtotal;
                        detalleFactura.PrecioServicio = modelDetalle.PrecioServicio;
                        detalleFactura.IdFactura = factura.IdFactura;
                        detalleFactura.IdServicio = modelDetalle.IdServicio;
                        detalleFactura.CantidadServicio = modelDetalle.CantidadServicio;

                        _context.DetalleFacturas.Add(detalleFactura);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("Ocurrio un error en la inserción ");
                }
            }
        }
    }
}
