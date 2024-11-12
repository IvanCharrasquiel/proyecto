using Peluqueria.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IMetodoPagoService
    {
        // Obtener todos los métodos de pago disponibles
        Task<List<MetodoPagoDTO>> ObtenerTodos();

        // Agregar un nuevo método de pago
        Task<MetodoPagoDTO> AgregarMetodoPago(MetodoPagoDTO metodoPagoDto);
    }
}
