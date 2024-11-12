using Peluqueria.DTO;
using Peluqueria.Model;
using Peluqueria.DAL.Repositories.Contrats;
using AutoMapper;
using System.Threading.Tasks;
using Peluqueria.BLL.Services.Contrats;
using System.Collections.Generic;

namespace Peluqueria.BLL.Services
{
    public class PagoService : IPagoService
    {
        private readonly IGenericRepository<Pago> _pagoRepository;
        private readonly IGenericRepository<Factura> _facturaRepository;
        private readonly IMapper _mapper;

        public PagoService(IGenericRepository<Pago> pagoRepository, IGenericRepository<Factura> facturaRepository, IMapper mapper)
        {
            _pagoRepository = pagoRepository;
            _facturaRepository = facturaRepository;
            _mapper = mapper;
        }

        // Registrar un nuevo pago
        public async Task<PagoDTO> RegistrarPago(PagoDTO pagoDto)
        {
            var factura = await _facturaRepository.Obtener(f => f.IdFactura == pagoDto.IdFactura);
            if (factura == null)
            {
                throw new InvalidOperationException("Factura no encontrada");
            }

            // Crear un nuevo modelo de pago y mapearlo desde el DTO
            var pago = _mapper.Map<Pago>(pagoDto);

            // Validar que el monto sea mayor que cero
            if (pago.Monto <= 0)
            {
                throw new InvalidOperationException("El monto debe ser mayor que cero");
            }

            // Asocia el pago con la factura y el método de pago
            pago.FechaPago = DateTime.UtcNow;
            pago.Estado = "Completado"; // Estado predeterminado, puede cambiarse según la lógica
            var pagoCreado = await _pagoRepository.Crear(pago);

            // Devolver el DTO con el pago registrado
            return _mapper.Map<PagoDTO>(pagoCreado);
        }

        // Obtener todos los pagos asociados a una factura
        public async Task<List<PagoDTO>> ObtenerPagosPorFactura(int idFactura)
        {
            var pagos = await _pagoRepository.Consultar(p => p.IdFactura == idFactura);
            return _mapper.Map<List<PagoDTO>>(pagos.ToList());
        }

        // Actualizar el estado de un pago
        public async Task<bool> ActualizarEstadoPago(int idPago, string nuevoEstado)
        {
            var pagoExistente = await _pagoRepository.Obtener(p => p.IdPago == idPago);
            if (pagoExistente == null)
            {
                throw new KeyNotFoundException("Pago no encontrado");
            }

            pagoExistente.Estado = nuevoEstado;
            return await _pagoRepository.Editar(pagoExistente);
        }
    }
}
