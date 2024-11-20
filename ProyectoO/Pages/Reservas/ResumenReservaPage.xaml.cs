using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ProyectoO.DTO;

namespace ProyectoO.Pages.Reservas
{
    public partial class ResumenReservaPage : ContentPage
    {
        public ResumenReservaPage(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada, List<ServicioDTO> serviciosSeleccionados)
        {
            InitializeComponent();
            BindingContext = new ResumenReservaViewModel(horarioSeleccionado, fechaSeleccionada, serviciosSeleccionados);
        }
    }

    public class ResumenReservaViewModel : INotifyPropertyChanged
    {
        public DateTime FechaSeleccionada { get; }
        public HorarioDisponibleDTO HorarioSeleccionado { get; }
        public List<ServicioDTO> ServiciosSeleccionados { get; }
        public decimal PrecioTotal => ServiciosSeleccionados.Sum(s => s.Precio);

        public ICommand ConfirmarReservaCommand { get; }

        public ResumenReservaViewModel(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada, List<ServicioDTO> serviciosSeleccionados)
        {
            HorarioSeleccionado = horarioSeleccionado;
            FechaSeleccionada = fechaSeleccionada;
            ServiciosSeleccionados = serviciosSeleccionados;

            ConfirmarReservaCommand = new Command(async () => await ConfirmarReserva());
        }

        private async Task ConfirmarReserva()
        {
            // Lógica para guardar la reserva
            await Application.Current.MainPage.DisplayAlert("Reserva Confirmada", "Tu reserva ha sido realizada con éxito.", "OK");
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
