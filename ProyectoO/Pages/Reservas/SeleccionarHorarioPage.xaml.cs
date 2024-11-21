using ProyectoO.Helpers;
using ProyectoO.Services;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarHorarioPage : ContentPage
    {
        public SeleccionarHorarioPage(DateTime fechaSeleccionada, int empleadoId, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            InitializeComponent();
            BindingContext = new SeleccionarHorarioViewModel(fechaSeleccionada, empleadoId, apiService, servicioPreSeleccionadoId, authService);
        }
    }
}
