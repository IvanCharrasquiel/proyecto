using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IServicioService
    {
        Task<List<ServicioDTO>> Listar();
        Task<ServicioDTO> Crear(ServicioDTO modelo);
        Task<bool> Editar(ServicioDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
