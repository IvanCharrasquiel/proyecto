using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ProyectoO.DTO;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarServiciosPage : ContentPage
    {
        public SeleccionarServiciosPage(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada)
        {
            InitializeComponent();
            BindingContext = new SeleccionarServiciosViewModel(horarioSeleccionado, fechaSeleccionada);
        }
    }

    public class SeleccionarServiciosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ServicioDTO> ServiciosDisponibles { get; }
        private readonly HorarioDisponibleDTO _horarioSeleccionado;
        private readonly DateTime _fechaSeleccionada;

        public bool PuedeContinuar => ServiciosDisponibles.Any(s => s.IsSelected);

        public ICommand ContinuarCommand { get; }

        public SeleccionarServiciosViewModel(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada)
        {
            _horarioSeleccionado = horarioSeleccionado;
            _fechaSeleccionada = fechaSeleccionada;
            ServiciosDisponibles = new ObservableCollection<ServicioDTO>();

            ContinuarCommand = new Command(() =>
            {
                var resumenPage = new ResumenReservaPage(_horarioSeleccionado, _fechaSeleccionada, ServiciosDisponibles.Where(s => s.IsSelected).ToList());
                Application.Current.MainPage.Navigation.PushAsync(resumenPage);
            });

            CargarServicios();
        }

        private void CargarServicios()
        {
            // Simular carga de servicios
            ServiciosDisponibles.Add(new ServicioDTO { NombreServicio = "Corte de Cabello", Precio = 15, Duracion = 30, IsSelected = false });
            ServiciosDisponibles.Add(new ServicioDTO { NombreServicio = "Tinte", Precio = 25, Duracion = 60, IsSelected = false });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
