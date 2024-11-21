using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class SeleccionarHorarioPage : ContentPage
    {
        public SeleccionarHorarioPage(DateTime fechaSeleccionada, EmpleadoDTO empleado, ApiService apiService, int servicioPreSeleccionadoId, AuthService authService)
        {
            InitializeComponent();
            BindingContext = new SeleccionarHorarioViewModel(fechaSeleccionada, empleado, apiService, servicioPreSeleccionadoId, authService);
        }
    }
}
