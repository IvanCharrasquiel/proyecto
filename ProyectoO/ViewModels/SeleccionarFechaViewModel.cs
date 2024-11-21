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
        private readonly int _empleadoId;
        private readonly ApiService _apiService;
        private readonly AuthService _authService;

        private DateTime _fechaSeleccionada;
        public DateTime FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set
            {
                if (_fechaSeleccionada != value)
                {
                    _ = CargarHorarios(value); 
                    
                }

            }
        }
        public ObservableCollection<HorarioDisponibleDTO> HorariosDisponibles { get; }

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
                    PuedeContinuar = true;
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

        public SeleccionarFechaViewModel(DateTime fechaSeleccionada, int empleadoId, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            _fechaSeleccionada = fechaSeleccionada;
            _empleadoId = empleadoId;
            _apiService = apiService;
            _authService = authService;

            HorariosDisponibles = new ObservableCollection<HorarioDisponibleDTO>();
            ContinuarCommand = new Command(() =>
            {
                var seleccionarServiciosPage = new SeleccionarServiciosPage(_fechaSeleccionada, _empleadoId, _apiService, HorarioSeleccionado, servicioPreSeleccionadoId, _authService);
                NavigationToPage(seleccionarServiciosPage);
            });

            // Inicialmente, deshabilitar el botón
            PuedeContinuar = false;
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
            var horarios = await _apiService.GetAsync<List<HorarioDisponibleDTO>>($"api/Horario/Disponibilidad?idEmpleado={_empleadoId}&fecha={fecha:yyyy-MM-dd}");
            HorariosDisponibles.Clear();
            foreach (var horario in horarios)
            {
                HorariosDisponibles.Add(horario);
            }
        }

        

        


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
