using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.BLL.Services
{
    public class DetalleFacturaService : IDetalleFacturaService
    {
        private readonly IGenericRepository<DetalleFactura> _detalleFacturaRepository;
        private readonly IMapper _mapper;

        public DetalleFacturaService(IGenericRepository<DetalleFactura> detalleFacturaRepository, IMapper mapper)
        {
            _detalleFacturaRepository = detalleFacturaRepository;
            _mapper = mapper;
        }

        // Listar todos los detalles de una factura
        public async Task<List<DetalleFacturaDTO>> ListarPorFactura(int idFactura)
        {
            try
            {
                var detalles = await _detalleFacturaRepository.Consultar(df => df.IdFactura == idFactura);
                return _mapper.Map<List<DetalleFacturaDTO>>(detalles.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Obtener un detalle de factura por ID
        public async Task<DetalleFacturaDTO> ObtenerPorId(int id)
        {
            try
            {
                var detalleFactura = await _detalleFacturaRepository.Obtener(df => df.IdDetalleFactura == id);
                return _mapper.Map<DetalleFacturaDTO>(detalleFactura);
            }
            catch
            {
                throw;
            }
        }

        // Crear un nuevo detalle de factura
        public async Task<DetalleFacturaDTO> Crear(DetalleFacturaDTO detalleFacturaDto)
        {
            try
            {
                var detalleFactura = _mapper.Map<DetalleFactura>(detalleFacturaDto);
                var detalleCreado = await _detalleFacturaRepository.Crear(detalleFactura);
                return _mapper.Map<DetalleFacturaDTO>(detalleCreado);
            }
            catch
            {
                throw;
            }
        }

        // Actualizar un detalle de factura existente
        public async Task<bool> Actualizar(DetalleFacturaDTO detalleFacturaDto)
        {
            try
            {
                var detalleExistente = await _detalleFacturaRepository.Obtener(df => df.IdDetalleFactura == detalleFacturaDto.IdDetalleFactura);
                if (detalleExistente == null)
                {
                    throw new KeyNotFoundException("Detalle de factura no encontrado");
                }

                _mapper.Map(detalleFacturaDto, detalleExistente);
                return await _detalleFacturaRepository.Editar(detalleExistente);
            }
            catch
            {
                throw;
            }
        }

        // Eliminar un detalle de factura
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var detalleExistente = await _detalleFacturaRepository.Obtener(df => df.IdDetalleFactura == id);
                if (detalleExistente == null)
                {
                    throw new KeyNotFoundException("Detalle de factura no encontrado");
                }

                return await _detalleFacturaRepository.Eliminar(detalleExistente);
            }
            catch
            {
                throw;
            }
        }
    }
}
