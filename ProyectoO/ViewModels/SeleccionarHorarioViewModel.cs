using System;
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
    public class SeleccionarHorarioViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly int _empleadoId;
        private readonly DateTime _fechaSeleccionada;
        private readonly AuthService _authService;

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
                    OnPropertyChanged(nameof(PuedeContinuar));
                }
            }
        }

        public bool PuedeContinuar => HorarioSeleccionado != null;

        public ICommand ContinuarCommand { get; }

        public SeleccionarHorarioViewModel(DateTime fechaSeleccionada, int empleadoId, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            _fechaSeleccionada = fechaSeleccionada;
            _empleadoId = empleadoId;
            _apiService = apiService;
            _authService = authService;

            HorariosDisponibles = new ObservableCollection<HorarioDisponibleDTO>();
            ContinuarCommand = new Command(() =>
            {
                var seleccionarServiciosPage = new SeleccionarServiciosPage(_fechaSeleccionada, _empleadoId, _apiService, HorarioSeleccionado, servicioPreSeleccionadoId, _authService);
                Application.Current.MainPage.Navigation.PushAsync(seleccionarServiciosPage);
            });

            _ = CargarHorarios();
        }

        private async Task CargarHorarios()
        {
            try
            {
                var horarios = await _apiService.GetAsync<List<HorarioDisponibleDTO>>(
                    $"api/Horario/Disponibilidad?idEmpleado={_empleadoId}&fecha={_fechaSeleccionada:yyyy-MM-dd}");

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
