using System.ComponentModel;
using System.Windows.Input;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarFechaPage : ContentPage
    {
        public SeleccionarFechaPage()
        {
            InitializeComponent();
            BindingContext = new SeleccionarFechaViewModel();
        }
    }

    public class SeleccionarFechaViewModel : INotifyPropertyChanged
    {
        private DateTime _fechaSeleccionada = DateTime.Today;
        public DateTime FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set
            {
                if (_fechaSeleccionada != value)
                {
                    _fechaSeleccionada = value;
                    PuedeContinuar = true;
                    OnPropertyChanged(nameof(FechaSeleccionada));
                    OnPropertyChanged(nameof(PuedeContinuar));
                }
            }
        }

        public bool PuedeContinuar { get; private set; }

        public ICommand ContinuarCommand { get; }

        public SeleccionarFechaViewModel()
        {
            ContinuarCommand = new Command(() =>
            {
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
