using Peluqueria.Model;

namespace Peluqueria.DAL.Repositories.Contrats
{
    public interface IFacturaRepository : IGenericRepository<Factura>
    {
        Task<Factura> Registrar(Factura modelo);
    }
}
