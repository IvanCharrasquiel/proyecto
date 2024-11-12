using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;
using System.Linq;

namespace Peluqueria.BLL.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository<Cliente> _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        // Listar todos los clientes
        public async Task<List<ClienteDTO>> Lista()
        {
            try
            {
                var listaCliente = await _clienteRepository.Consultar();
                return _mapper.Map<List<ClienteDTO>>(listaCliente.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener un cliente por ID
        public async Task<ClienteDTO> ObtenerPorId(int id)
        {
            try
            {
                var cliente = await _clienteRepository.Obtener(c => c.IdCliente == id);
                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch
            {
                throw;
            }
        }

        // Crear un nuevo cliente (sin hash de contraseña, uso interno)
        public async Task<ClienteDTO> Crear(ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);
                var clienteCreado = await _clienteRepository.Crear(cliente);
                return _mapper.Map<ClienteDTO>(clienteCreado);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar un cliente existente
        public async Task<bool> Actualizar(ClienteDTO clienteDto)
        {
            try
            {
                var clienteExistente = await _clienteRepository.Obtener(c => c.IdCliente == clienteDto.IdCliente);
                if (clienteExistente == null)
                {
                    throw new KeyNotFoundException("Cliente no encontrado");
                }

                _mapper.Map(clienteDto, clienteExistente);
                return await _clienteRepository.Editar(clienteExistente);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar un cliente
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var clienteExistente = await _clienteRepository.Obtener(c => c.IdCliente == id);
                if (clienteExistente == null)
                {
                    throw new KeyNotFoundException("Cliente no encontrado");
                }

                return await _clienteRepository.Eliminar(clienteExistente);
            }
            catch
            {
                throw;
            }
        }

        // Validar credenciales de cliente
        // Ubicación: Services/ClienteService.cs
        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var cliente = await _clienteRepository.Obtener(c => c.IdPersonaNavigation.Email == correo);

                // Verificar si la contraseña es correcta
                if (cliente == null || !PasswordHelper.VerifyPassword(cliente.Contraseña, clave))
                {
                    return null; // Credenciales inválidas
                }

                // Devolver un DTO con los datos de sesión sin token
                return _mapper.Map<SesionDTO>(cliente.IdPersonaNavigation);
            }
            catch
            {
                throw;
            }
        }


        // Registrar un nuevo cliente con contraseña segura
        public async Task<ClienteDTO> RegistrarCliente(ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);
                cliente.Contraseña = PasswordHelper.HashPassword(clienteDto.Contraseña); // Hash de la contraseña
                var clienteCreado = await _clienteRepository.Crear(cliente);

                return _mapper.Map<ClienteDTO>(clienteCreado);
            }
            catch
            {
                throw;
            }
        }

        // Cambiar la contraseña de un cliente existente
        public async Task<bool> CambiarContraseña(int idCliente, string nuevaContraseña)
        {
            try
            {
                var cliente = await _clienteRepository.Obtener(c => c.IdCliente == idCliente);
                if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado");

                cliente.Contraseña = PasswordHelper.HashPassword(nuevaContraseña); // Hash de la nueva contraseña
                return await _clienteRepository.Editar(cliente);
            }
            catch
            {
                throw;
            }
        }
    }
}
