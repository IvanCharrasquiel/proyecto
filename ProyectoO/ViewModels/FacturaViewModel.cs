using ProyectoO.DTO;
using ProyectoO.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoO.ViewModels
{
    public class FacturaViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly int _idReserva;

        private FacturaDTO _factura;
        public FacturaDTO Factura
        {
            get => _factura;
            set
            {
                _factura = value;
                OnPropertyChanged(nameof(Factura));
            }
        }

        public bool MostrarBotonPagar => Factura != null && Factura.Estado == "Pendiente";

        public ICommand PagarFacturaCommand { get; }

        public FacturaViewModel(int idReserva, ApiService apiService)
        {
            _idReserva = idReserva;
            _apiService = apiService;

            PagarFacturaCommand = new Command(async () => await PagarFactura());

            _ = CargarFactura();
        }

        private async Task CargarFactura()
        {
            try
            {
                Factura = await _apiService.GetAsync<FacturaDTO>($"api/Facturas/Reserva/{_idReserva}");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar la factura: {ex.Message}", "OK");
            }
        }

        private async Task PagarFactura()
        {
            try
            {
                // Aquí puedes implementar la lógica de pago.
                // Por simplicidad, actualizaremos el estado de la factura.

                Factura.Estado = "Pagada";
                await _apiService.PutAsync<FacturaDTO, object>($"api/Facturas/{Factura.IdFactura}", Factura);

                OnPropertyChanged(nameof(MostrarBotonPagar));

                await Application.Current.MainPage.DisplayAlert("Éxito", "Factura pagada exitosamente.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al pagar la factura: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
