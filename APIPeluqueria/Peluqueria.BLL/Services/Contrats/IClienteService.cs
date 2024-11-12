using System.Collections.Generic;
using System.Threading.Tasks;
using Peluqueria.DTO;

namespace Peluqueria.BLL.Services.Contrats
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> Lista();
        Task<ClienteDTO> ObtenerPorId(int id);
        Task<ClienteDTO> Crear(ClienteDTO clienteDto);
        Task<bool> Actualizar(ClienteDTO clienteDto);
        Task<bool> Eliminar(int id);
        Task<SesionDTO> ValidarCredenciales(string correo, string clave);
        Task<ClienteDTO> RegistrarCliente(ClienteDTO clienteDto); // Registro seguro
        Task<bool> CambiarContraseña(int idCliente, string nuevaContraseña); // Cambio seguro
    }
}
