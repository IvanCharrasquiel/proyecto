using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.ViewModels;
using Microsoft.Maui.Controls;

namespace ProyectoO.Pages.Factura
{
    public partial class FacturaPage : ContentPage
    {
        public FacturaPage(int idReserva, ApiService apiService)
        {
            InitializeComponent();
            BindingContext = new FacturaViewModel(idReserva, apiService);
        }
    }
}
