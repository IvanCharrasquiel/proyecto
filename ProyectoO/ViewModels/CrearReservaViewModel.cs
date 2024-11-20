using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ProyectoO.DTO;
using ProyectoO.Helpers;

namespace ProyectoO.ViewModels
{
    public class CrearReservaViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private int _empleadoId;

        public CrearReservaViewModel(ApiService apiService, int empleadoId)
        {
            _apiService = apiService;
            _empleadoId = empleadoId;

            HorariosDisponibles = new ObservableCollection<HorarioDisponibleDTO>();
            ServiciosDisponibles = new ObservableCollection<ServicioDTO>();
            ConfirmarReservaCommand = new Command(async () => await ConfirmarReserva());

            CargarServicios();
        }

        // Propiedades
        public ObservableCollection<HorarioDisponibleDTO> HorariosDisponibles { get; }
        public ObservableCollection<ServicioDTO> ServiciosDisponibles { get; }

        private DateTime _selectedFecha = DateTime.Today;
        public DateTime SelectedFecha
        {
            get => _selectedFecha;
            set
            {
                if (_selectedFecha != value)
                {
                    _selectedFecha = value;
                    OnPropertyChanged(nameof(SelectedFecha));
                    _ = CargarHorarios(value);
                }
            }
        }

        private HorarioDisponibleDTO _horarioSeleccionado;
        public HorarioDisponibleDTO HorarioSeleccionado
        {
            get => _horarioSeleccionado;
            set
            {
                _horarioSeleccionado = value;
                OnPropertyChanged(nameof(HorarioSeleccionado));
                ActualizarResumen();
            }
        }

        public bool MostrarResumen => HorarioSeleccionado != null && ServiciosDisponibles.Any(s => s.IsSelected);

        public bool PuedeConfirmarReserva => MostrarResumen;

        public string Resumen { get; private set; }

        public ICommand ConfirmarReservaCommand { get; }

        // Métodos
        private async Task CargarServicios()
        {
            var servicios = await _apiService.GetAsync<List<ServicioDTO>>($"api/Servicios/ServiciosPorEmpleado/{_empleadoId}");
            ServiciosDisponibles.Clear();
            foreach (var servicio in servicios)
            {
                ServiciosDisponibles.Add(servicio);
            }
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

        private void ActualizarResumen()
        {
            if (HorarioSeleccionado == null || !ServiciosDisponibles.Any(s => s.IsSelected))
            {
                Resumen = string.Empty;
                OnPropertyChanged(nameof(Resumen));
                OnPropertyChanged(nameof(MostrarResumen));
                OnPropertyChanged(nameof(PuedeConfirmarReserva));
                return;
            }

            var serviciosSeleccionados = ServiciosDisponibles.Where(s => s.IsSelected).ToList();
            var totalDuracion = serviciosSeleccionados.Sum(s => s.Duracion);
            var totalPrecio = serviciosSeleccionados.Sum(s => s.Precio);

            Resumen = $"Empleado: {_empleadoId}\n" +
                      $"Fecha: {SelectedFecha:yyyy-MM-dd}\n" +
                      $"Hora Inicio: {HorarioSeleccionado.HoraInicio:hh\\:mm}\n" +
                      $"Duración Total: {totalDuracion} minutos\n" +
                      $"Total: {totalPrecio:C}\n" +
                      $"Servicios:\n" +
                      string.Join("\n", serviciosSeleccionados.Select(s => $"- {s.NombreServicio}"));

            OnPropertyChanged(nameof(Resumen));
            OnPropertyChanged(nameof(MostrarResumen));
            OnPropertyChanged(nameof(PuedeConfirmarReserva));
        }

        private async Task ConfirmarReserva()
        {
            // Lógica para guardar la reserva
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
