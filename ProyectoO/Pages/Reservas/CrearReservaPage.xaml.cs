using ProyectoO.Helpers;
using ProyectoO.ViewModels;

namespace ProyectoO.Pages.Reservas
{
    public partial class CrearReservaPage : ContentPage
    {
        public CrearReservaPage(ApiService apiService, int empleadoId)
        {
            InitializeComponent();
            BindingContext = new CrearReservaViewModel(apiService, empleadoId);
        }
    }
}
