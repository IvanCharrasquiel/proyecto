using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarFechaPage : ContentPage
    {
        public SeleccionarFechaPage(int empleadoId, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            InitializeComponent();
            BindingContext = new SeleccionarFechaViewModel(DateTime.Today, empleadoId, apiService, servicioPreSeleccionadoId, authService);
        }
    }
}
