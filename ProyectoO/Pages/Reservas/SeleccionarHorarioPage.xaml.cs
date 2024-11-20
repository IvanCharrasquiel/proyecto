using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ProyectoO.DTO;
using ProyectoO.Helpers;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarHorarioPage : ContentPage
    {
        public SeleccionarHorarioPage(DateTime fechaSeleccionada, int empleadoId, ApiService apiService, List<ServicioDTO> serviciosSeleccionados)
        {
            InitializeComponent();
            BindingContext = new SeleccionarHorarioViewModel(fechaSeleccionada, empleadoId, apiService, serviciosSeleccionados);
        }
    }

    public class SeleccionarHorarioViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly int _empleadoId;
        private readonly DateTime _fechaSeleccionada;
        private readonly List<ServicioDTO> _serviciosSeleccionados;

        public ObservableCollection<HorarioDisponibleDTO> HorariosDisponibles { get; }

        private HorarioDisponibleDTO _horarioSeleccionado;
        public HorarioDisponibleDTO HorarioSeleccionado
        {
            get => _horarioSeleccionado;
            set
            {
                _horarioSeleccionado = value;
                OnPropertyChanged(nameof(HorarioSeleccionado));
            }
        }

        public ICommand ConfirmarCommand { get; }

        public SeleccionarHorarioViewModel(DateTime fechaSeleccionada, int empleadoId, ApiService apiService, List<ServicioDTO> serviciosSeleccionados)
        {
            _fechaSeleccionada = fechaSeleccionada;
            _empleadoId = empleadoId;
            _apiService = apiService;
            _serviciosSeleccionados = serviciosSeleccionados;

            HorariosDisponibles = new ObservableCollection<HorarioDisponibleDTO>();
            ConfirmarCommand = new Command(ConfirmarHorario);

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
            }
            catch (Exception ex)
            {
                // Manejar error
            }
        }

        private void ConfirmarHorario()
        {
            if (HorarioSeleccionado != null)
            {
                var resumenPage = new ResumenReservaPage(HorarioSeleccionado, _fechaSeleccionada, _serviciosSeleccionados);
                App.FlyoutPage.Detail.Navigation.PushAsync(resumenPage);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
