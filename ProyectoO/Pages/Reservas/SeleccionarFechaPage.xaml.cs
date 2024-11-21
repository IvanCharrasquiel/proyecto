using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarFechaPage : ContentPage
    {
        public SeleccionarFechaPage(EmpleadoDTO empleado, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            InitializeComponent();
            BindingContext = new SeleccionarFechaViewModel(DateTime.Today, empleado, apiService, servicioPreSeleccionadoId, authService);
        }
    }

}
