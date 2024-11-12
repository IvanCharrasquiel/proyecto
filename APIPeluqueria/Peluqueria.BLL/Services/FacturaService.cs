using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IGenericRepository<Factura> _facturaRepository;
        private readonly IMapper _mapper;

        public FacturaService(IGenericRepository<Factura> facturaRepository, IMapper mapper)
        {
            _facturaRepository = facturaRepository;
            _mapper = mapper;
        }

        // Listar todas las facturas
        public async Task<List<FacturaDTO>> ListarFacturas()
        {
            try
            {
                var listaFacturas = await _facturaRepository.Consultar();
                return _mapper.Map<List<FacturaDTO>>(listaFacturas.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener una factura por ID
        public async Task<FacturaDTO> ObtenerPorId(int id)
        {
            try
            {
                var factura = await _facturaRepository.Obtener(f => f.IdFactura == id);
                return _mapper.Map<FacturaDTO>(factura);
            }
            catch
            {
                throw;
            }
        }

        // Crear una nueva factura
        public async Task<FacturaDTO> Crear(FacturaDTO facturaDto)
        {
            try
            {
                var factura = _mapper.Map<Factura>(facturaDto);
                var facturaCreada = await _facturaRepository.Crear(factura);
                return _mapper.Map<FacturaDTO>(facturaCreada);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar el estado de una factura (ej., pagada, pendiente)
        public async Task<bool> ActualizarEstado(int id, string nuevoEstado)
        {
            try
            {
                var facturaExistente = await _facturaRepository.Obtener(f => f.IdFactura == id);
                if (facturaExistente == null)
                {
                    throw new KeyNotFoundException("Factura no encontrada");
                }

                facturaExistente.Estado = nuevoEstado;
                return await _facturaRepository.Editar(facturaExistente);
            }
            catch
            {
                throw;
            }
        }

        // Obtener el historial de facturación de un cliente
        public async Task<List<FacturaDTO>> HistorialPorCliente(int idCliente)
        {
            try
            {
                var historial = await _facturaRepository.Consultar(f => f.IdReservaNavigation.IdCliente == idCliente);
                return _mapper.Map<List<FacturaDTO>>(historial.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
