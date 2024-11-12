using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peluqueria.BLL.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IGenericRepository<Servicio> _servicioRepository;
        private readonly IMapper _mapper;

        public ServicioService(IGenericRepository<Servicio> servicioRepository, IMapper mapper)
        {
            _servicioRepository = servicioRepository;
            _mapper = mapper;
        }

        // Método para listar todos los servicios
        public async Task<List<ServicioDTO>> Listar()
        {
            try
            {
                var listaServicios = await _servicioRepository.Consultar();
                return _mapper.Map<List<ServicioDTO>>(listaServicios.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Método para crear un nuevo servicio
        public async Task<ServicioDTO> Crear(ServicioDTO modelo)
        {
            try
            {
                var entidadServicio = _mapper.Map<Servicio>(modelo);
                var servicioCreado = await _servicioRepository.Crear(entidadServicio);
                return _mapper.Map<ServicioDTO>(servicioCreado);
            }
            catch
            {
                throw;
            }
        }

        // Método para editar un servicio existente
        public async Task<bool> Editar(ServicioDTO modelo)
        {
            try
            {
                var servicioModelo = _mapper.Map<Servicio>(modelo);
                var servicioEncontrado = await _servicioRepository.Obtener(s => s.IdServicio == servicioModelo.IdServicio);

                if (servicioEncontrado == null)
                {
                    throw new TaskCanceledException("El servicio no existe");
                }

                servicioEncontrado.NombreServicio = servicioModelo.NombreServicio;
                servicioEncontrado.Descripcion = servicioModelo.Descripcion;
                servicioEncontrado.Precio = servicioModelo.Precio;
                servicioEncontrado.Duracion = servicioModelo.Duracion;

                bool respuesta = await _servicioRepository.Editar(servicioEncontrado);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        // Método para eliminar un servicio
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var servicioEncontrado = await _servicioRepository.Obtener(s => s.IdServicio == id);

                if (servicioEncontrado == null)
                {
                    throw new TaskCanceledException("El servicio no existe");
                }

                bool respuesta = await _servicioRepository.Eliminar(servicioEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo eliminar el servicio");
                }

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
