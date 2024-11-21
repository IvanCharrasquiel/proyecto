using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class ResumenReservaPage : ContentPage
    {
        public ResumenReservaPage(HorarioDisponibleDTO horarioSeleccionado, DateTime fechaSeleccionada, List<ServicioDTO> serviciosSeleccionados, EmpleadoDTO empleado, ApiService apiService, AuthService authService)
        {
            InitializeComponent();
            BindingContext = new ResumenReservaViewModel(horarioSeleccionado, fechaSeleccionada, serviciosSeleccionados, empleado, apiService, authService);
        }
    }

}
