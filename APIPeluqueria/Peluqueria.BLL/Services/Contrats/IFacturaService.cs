using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IFacturaService
    {
        Task<List<FacturaDTO>> ListarFacturas();
        Task<FacturaDTO> ObtenerPorId(int id);
        Task<FacturaDTO> Crear(FacturaDTO facturaDto);
        Task<bool> ActualizarEstado(int id, string nuevoEstado); // Para cambiar el estado de pago de la factura
        Task<List<FacturaDTO>> HistorialPorCliente(int idCliente); // Obtener el historial de facturación de un cliente
    }
}
