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
        private readonly int _empleadoId;
        private readonly ApiService _apiService;
        private readonly AuthService _authService;

        public DateTime FechaSeleccionada => _fechaSeleccionada;
        public HorarioDisponibleDTO HorarioSeleccionado => _horarioSeleccionado;
        public List<ServicioDTO> ServiciosSeleccionados => _serviciosSeleccionados;
        public decimal PrecioTotal => ServiciosSeleccionados.Sum(s => s.Precio);

        public ICommand ConfirmarReservaCommand { get; }

        public ResumenReservaViewModel(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada, List<ServicioDTO> serviciosSeleccionados, int empleadoId, ApiService apiService, AuthService authService)
        {
            _horarioSeleccionado = horarioSeleccionado;
            _fechaSeleccionada = fechaSeleccionada;
            _serviciosSeleccionados = serviciosSeleccionados;
            _empleadoId = empleadoId;
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
                    HoraFin = _horarioSeleccionado.HoraFin,
                    IdEmpleado = _empleadoId,
                    
                    Servicios = ServiciosSeleccionados.Select(s => new ReservaServicioDTO { IdServicio = s.IdServicio }).ToList()
                };

                var resultado = await _apiService.PostAsync<ReservaDTO, HttpResponseMessage>("api/Reserva/CrearReserva", reserva);

                if (resultado.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Reserva Confirmada", "Tu reserva ha sido realizada con éxito.", "OK");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else
                {
                    var mensaje = await resultado.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al confirmar la reserva: {mensaje}", "OK");
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
