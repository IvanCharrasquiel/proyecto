using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarServiciosPage : ContentPage
    {
        public SeleccionarServiciosPage(DateTime fechaSeleccionada, int empleadoId, ApiService apiService, HorarioDisponibleDTO horarioSeleccionado, int servicioPreSeleccionadoId, AuthService authService)
        {
            InitializeComponent();
            BindingContext = new SeleccionarServiciosViewModel(fechaSeleccionada, empleadoId, apiService, horarioSeleccionado, servicioPreSeleccionadoId, authService);
        }
    }
}
