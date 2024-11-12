using AutoMapper;
using Peluqueria.DTO;
using Peluqueria.Model;

namespace Peluqueria.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Cargo
            CreateMap<Cargo, CargoDTO>().ReverseMap();
            #endregion

            #region DetalleFactura
            CreateMap<DetalleFactura, DetalleFacturaDTO>()
                .ForMember(dest => dest.Servicio, opt => opt.MapFrom(src => src.IdServicioNavigation))
                .ReverseMap();
            #endregion

            #region Empleado
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.IdCargoNavigation))
                .ReverseMap();
            #endregion

            #region EmpleadoHorario
            CreateMap<EmpleadoHorario, EmpleadoHorarioDTO>().ReverseMap();
            #endregion

            #region EmpleadoServicio
            CreateMap<EmpleadoServicio, EmpleadoServicioDTO>().ReverseMap();
            #endregion

            #region Factura
            CreateMap<Factura, FacturaDTO>()
                .ForMember(dest => dest.DetalleFacturas, opt => opt.MapFrom(src => src.DetalleFacturas))
                .ForMember(dest => dest.Pagos, opt => opt.MapFrom(src => src.Pagos)) // Mapeo de pagos (si es necesario)
                .ReverseMap();
            #endregion

            #region HorarioAtencion
            CreateMap<HorarioAtencion, HorarioAtencionDTO>().ReverseMap();
            #endregion

            #region MetodoPago
            CreateMap<MetodoPago, MetodoPagoDTO>().ReverseMap();
            #endregion

            #region Pago
            CreateMap<Pago, PagoDTO>().ReverseMap();
            #endregion

            #region Persona
            CreateMap<Persona, PersonaDTO>().ReverseMap();
            #endregion

            #region Promocion
            CreateMap<Promocion, PromocionDTO>()
                .ForMember(dest => dest.Servicio, opt => opt.MapFrom(src => src.IdServicioNavigation))
                .ReverseMap();
            #endregion

            #region Reserva
            CreateMap<Reserva, ReservaDTO>()
                .ForMember(dest => dest.Empleado, opt => opt.MapFrom(src => src.IdEmpleadoNavigation))
                .ForMember(dest => dest.Servicios, opt => opt.MapFrom(src => src.ServicioReservas.Select(sr => sr.IdServicioNavigation)))
                .ReverseMap();
            #endregion

            #region Servicio
            CreateMap<Servicio, ServicioDTO>().ReverseMap();
            #endregion

            #region ServicioReserva
            CreateMap<ServicioReserva, ServicioReservaDTO>().ReverseMap();
            #endregion

            #region Cliente
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.Persona, opt => opt.MapFrom(src => src.IdPersonaNavigation))
                .ForMember(dest => dest.Reservas, opt => opt.MapFrom(src => src.Reservas))
                .ReverseMap();
            #endregion

            #region DateOnly y DateTime Conversiones
            CreateMap<DateOnly, DateTime>()
                .ConvertUsing(d => d.ToDateTime(TimeOnly.MinValue));
            CreateMap<DateTime, DateOnly>()
                .ConvertUsing(d => DateOnly.FromDateTime(d));
            #endregion
        }
    }
}
