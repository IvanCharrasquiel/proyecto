using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Pages.Reservas;
using ProyectoO.Services;

namespace ProyectoO.ViewModels
{
    public class SeleccionarServiciosViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private readonly EmpleadoDTO _empleado;
        private readonly DateTime _fechaSeleccionada;
        private readonly HorarioDisponibleDTO _horarioSeleccionado;
        private readonly int _servicioPreSeleccionadoId;

        public ObservableCollection<ServicioDTO> ServiciosDisponibles { get; }

        public List<ServicioDTO> ServiciosSeleccionados => ServiciosDisponibles.Where(s => s.IsSelected).ToList();

        public bool PuedeContinuar => ServiciosSeleccionados.Any();

        public ICommand ContinuarCommand { get; }

        public SeleccionarServiciosViewModel(DateTime fechaSeleccionada, EmpleadoDTO empleado, ApiService apiService, HorarioDisponibleDTO horarioSeleccionado, int servicioPreSeleccionadoId, AuthService authService)
        {
            _fechaSeleccionada = fechaSeleccionada;
            _empleado = empleado;
            _apiService = apiService;
            _horarioSeleccionado = horarioSeleccionado;
            _servicioPreSeleccionadoId = servicioPreSeleccionadoId;
            _authService = authService;

            ServiciosDisponibles = new ObservableCollection<ServicioDTO>();

            ContinuarCommand = new Command(() =>
            {
                var resumenPage = new ResumenReservaPage(_horarioSeleccionado, _fechaSeleccionada, ServiciosSeleccionados, _empleado, _apiService, _authService);
                NavigationToPage(resumenPage);
            });

            _ = CargarServicios();
        }
        private async Task CargarServicios()
        {
            try
            {
                var servicios = await _apiService.GetAsync<List<ServicioDTO>>($"api/Servicios/ServiciosPorEmpleado/{_empleado.IdEmpleado}");
                ServiciosDisponibles.Clear();
                foreach (var servicio in servicios)
                {
                    if (servicio.IdServicio == _servicioPreSeleccionadoId)
                    {
                        servicio.IsSelected = true;
                    }
                    ServiciosDisponibles.Add(servicio);
                }

                OnPropertyChanged(nameof(ServiciosSeleccionados));
                OnPropertyChanged(nameof(PuedeContinuar));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar servicios: {ex.Message}", "OK");
            }
        }

        public static void NavigationToPage(ContentPage nuevaPagina)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
        }
        public static void OcultarDetalles()
        {
            App.FlyoutPage.IsPresented = false;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == nameof(ServicioDTO.IsSelected))
            {
                OnPropertyChanged(nameof(ServiciosSeleccionados));
                OnPropertyChanged(nameof(PuedeContinuar));
            }
        }
    }
}
