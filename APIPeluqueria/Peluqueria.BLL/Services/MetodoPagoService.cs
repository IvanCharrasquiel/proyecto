using Peluqueria.DTO;
using Peluqueria.Model;
using Peluqueria.DAL.Repositories.Contrats;
using AutoMapper;
using System.Threading.Tasks;
using Peluqueria.BLL.Services.Contrats;
using System.Collections.Generic;

namespace Peluqueria.BLL.Services
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IGenericRepository<MetodoPago> _metodoPagoRepository;
        private readonly IMapper _mapper;

        public MetodoPagoService(IGenericRepository<MetodoPago> metodoPagoRepository, IMapper mapper)
        {
            _metodoPagoRepository = metodoPagoRepository;
            _mapper = mapper;
        }

        // Obtener todos los métodos de pago disponibles
        public async Task<List<MetodoPagoDTO>> ObtenerTodos()
        {
            var metodosPago = await _metodoPagoRepository.Consultar();
            return _mapper.Map<List<MetodoPagoDTO>>(metodosPago.ToList());
        }

        // Agregar un nuevo método de pago
        public async Task<MetodoPagoDTO> AgregarMetodoPago(MetodoPagoDTO metodoPagoDto)
        {
            // Mapeamos el DTO a la entidad
            var metodoPago = _mapper.Map<MetodoPago>(metodoPagoDto);

            // Agregar el nuevo método de pago a la base de datos
            var metodoPagoCreado = await _metodoPagoRepository.Crear(metodoPago);

            // Devolvemos el DTO del método de pago recién agregado
            return _mapper.Map<MetodoPagoDTO>(metodoPagoCreado);
        }
    }
}

