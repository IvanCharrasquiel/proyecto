using Microsoft.EntityFrameworkCore;
using Peluqueria.DAL.DBContext;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.Model;

namespace Peluqueria.DAL.Repositories
{
    public class FacturaRepository : GenericRepository<Factura>, IFacturaRepository
    {

        private AppDbContext _dbContext;

        public FacturaRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Factura> Registrar(Factura modelo)
        {
            Factura facturaGenerada = new Factura();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {

                try
                {
                    foreach (DetalleFactura dv in modelo.DetalleFacturas)
                    {
                        Servicio servicioEncontrado = _dbContext.Servicios.Where(s => s.IdServicio == dv.IdServicio).First();

                    }
                    NumeroDocumento correlativo = _dbContext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroDocumentos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    int cantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                    string numeroFactura = ceros + correlativo.UltimoNumero.ToString();
                    //00001
                    numeroFactura = numeroFactura.Substring(numeroFactura.Length - cantidadDigitos, cantidadDigitos);

                    modelo.NumeroDocumeto = numeroFactura;

                    await _dbContext.Facturas.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    facturaGenerada = modelo;

                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return facturaGenerada;

            }

        }
    }
}
