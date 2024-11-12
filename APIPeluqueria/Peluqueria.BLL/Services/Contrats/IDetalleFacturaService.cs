using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IDetalleFacturaService
    {
        Task<List<DetalleFacturaDTO>> ListarPorFactura(int idFactura);
        Task<DetalleFacturaDTO> ObtenerPorId(int id);
        Task<DetalleFacturaDTO> Crear(DetalleFacturaDTO detalleFacturaDto);
        Task<bool> Actualizar(DetalleFacturaDTO detalleFacturaDto);
        Task<bool> Eliminar(int id);
    }
}
