using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services;

namespace ProyectoO.ViewModels
{
    public class ResumenReservaViewModel : INotifyPropertyChanged
    {
        private readonly HorarioDisponibleDTO _horarioSeleccionado;
        private readonly DateTime _fechaSeleccionada;
        private readonly List<ServicioDTO> _serviciosSeleccionados;
        private readonly EmpleadoDTO _empleado;
        private readonly ApiService _apiService;
        private readonly AuthService _authService;

        public DateTime FechaSeleccionada => _fechaSeleccionada;
        public HorarioDisponibleDTO HorarioSeleccionado => _horarioSeleccionado;
        public List<ServicioDTO> ServiciosSeleccionados => _serviciosSeleccionados;
        public decimal PrecioTotal => ServiciosSeleccionados.Sum(s => s.Precio);

        public TimeSpan DuracionTotal => TimeSpan.FromMinutes(ServiciosSeleccionados.Sum(s => s.Duracion));
        public TimeSpan HoraFin => _horarioSeleccionado.HoraInicio + DuracionTotal;

        public EmpleadoDTO Empleado => _empleado;

        public ICommand ConfirmarReservaCommand { get; }

        public ResumenReservaViewModel(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada, List<ServicioDTO> serviciosSeleccionados, EmpleadoDTO empleado, ApiService apiService, AuthService authService)
        {
            _horarioSeleccionado = horarioSeleccionado;
            _fechaSeleccionada = fechaSeleccionada;
            _serviciosSeleccionados = serviciosSeleccionados;
            _empleado = empleado;
            _apiService = apiService;
            _authService = authService;

            ConfirmarReservaCommand = new Command(async () => await ConfirmarReserva());
        }

        private async Task ConfirmarReserva()
        {
            try
            {
                var reserva = new ReservaDTO
                {
                    Fecha = _fechaSeleccionada.Date,
                    HoraInicio = _horarioSeleccionado.HoraInicio,
                    HoraFin = HoraFin,
                    IdEmpleado = _empleado.IdEmpleado,
                    IdCliente = UserService.Instance.CurrentIdUser,
                    Servicios = ServiciosSeleccionados.Select(s => new ReservaServicioDTO { IdServicio = s.IdServicio }).ToList()
                };

                var reservaCreada = await _apiService.PostAsync<ReservaDTO, ReservaDTO>("api/Reserva", reserva);

                if (reservaCreada != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Reserva Confirmada", "Tu reserva ha sido realizada con éxito.", "OK");
                    // Navegar a otra página si es necesario
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo confirmar la reserva. Por favor, intenta nuevamente.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al confirmar la reserva: {ex.Message}", "OK");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
