using Peluqueria.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IPagoService
    {
        // Registrar un nuevo pago
        Task<PagoDTO> RegistrarPago(PagoDTO pagoDto);

        // Obtener todos los pagos asociados a una factura
        Task<List<PagoDTO>> ObtenerPagosPorFactura(int idFactura);

        // Actualizar el estado de un pago
        Task<bool> ActualizarEstadoPago(int idPago, string nuevoEstado);
    }
}
