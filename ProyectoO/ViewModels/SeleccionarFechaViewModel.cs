using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Pages.Reservas;
using ProyectoO.Services;

namespace ProyectoO.ViewModels
{
    public class SeleccionarFechaViewModel : INotifyPropertyChanged
    {
        private readonly EmpleadoDTO _empleado;
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private readonly int _servicioPreSeleccionadoId;

        private DateTime _fechaSeleccionada;
        public DateTime FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set
            {
                if (_fechaSeleccionada != value)
                {
                    _fechaSeleccionada = value;
                    OnPropertyChanged(nameof(FechaSeleccionada));
                    _ = CargarHorarios(_fechaSeleccionada);
                }
            }
        }
        public ObservableCollection<HorarioDisponibleDTO> HorariosDisponibles { get; } = new ObservableCollection<HorarioDisponibleDTO>();

        private HorarioDisponibleDTO _horarioSeleccionado;
        public HorarioDisponibleDTO HorarioSeleccionado
        {
            get => _horarioSeleccionado;
            set
            {
                if (_horarioSeleccionado != value)
                {
                    _horarioSeleccionado = value;
                    OnPropertyChanged(nameof(HorarioSeleccionado));
                    PuedeContinuar = _horarioSeleccionado != null;
                    OnPropertyChanged(nameof(PuedeContinuar));
                }
            }
        }


        private bool _puedeContinuar;
        public bool PuedeContinuar
        {
            get => _puedeContinuar;
            private set
            {
                if (_puedeContinuar != value)
                {
                    _puedeContinuar = value;
                    OnPropertyChanged(nameof(PuedeContinuar));
                }
            }
        }

        public ICommand ContinuarCommand { get; }

        public SeleccionarFechaViewModel(DateTime fechaSeleccionada, EmpleadoDTO empleado, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            _fechaSeleccionada = fechaSeleccionada;
            _empleado = empleado;
            _apiService = apiService;
            _authService = authService;
            _servicioPreSeleccionadoId = servicioPreSeleccionadoId;

            ContinuarCommand = new Command(() =>
            {
                var seleccionarServiciosPage = new SeleccionarServiciosPage(_fechaSeleccionada, _empleado, _apiService, HorarioSeleccionado, _servicioPreSeleccionadoId, _authService);
                NavigationToPage(seleccionarServiciosPage);
            });

            PuedeContinuar = false;

            _ = CargarHorarios(_fechaSeleccionada);
        }



        public static void NavigationToPage(ContentPage nuevaPagina)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
        }
        public static void OcultarDetalles()
        {
            App.FlyoutPage.IsPresented = false;
        }

        private async Task CargarHorarios(DateTime fecha)
        {
            try
            {
                var horarios = await _apiService.GetAsync<List<HorarioDisponibleDTO>>($"api/Horario/Disponibilidad?idEmpleado={_empleado.IdEmpleado}&fecha={fecha:yyyy-MM-dd}");
                HorariosDisponibles.Clear();
                foreach (var horario in horarios)
                {
                    HorariosDisponibles.Add(horario);
                }

                if (!HorariosDisponibles.Any())
                {
                    await Application.Current.MainPage.DisplayAlert("Información", "No hay horarios disponibles para esta fecha.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar horarios: {ex.Message}", "OK");
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
