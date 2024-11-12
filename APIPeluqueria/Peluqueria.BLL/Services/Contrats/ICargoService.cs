using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface ICargoService
    {
        Task<List<CargoDTO>> Lista();
        Task<CargoDTO> ObtenerPorId(int id);
        Task<CargoDTO> Crear(CargoDTO cargoDto);
        Task<bool> Actualizar(CargoDTO cargoDto);
        Task<bool> Eliminar(int id);
    }
}
