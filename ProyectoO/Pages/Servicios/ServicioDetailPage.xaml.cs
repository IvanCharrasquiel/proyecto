// Pages/Servicios/ServicioDetailPage.xaml.cs
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoO.Pages.Servicios
{
    public partial class ServicioDetailPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ApiService _apiService;
        private readonly ServicioDTO _servicio;

        public ServicioDetailPage(IAuthService authService, IPersonaService personaService, ServicioDTO servicio)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _apiService = new ApiService(_personaService.BaseUrl);
            _servicio = servicio;

            BindingContext = _servicio;

            LoadPromociones();
        }

        private async void LoadPromociones()
{
    try
    {
        var promociones = await _apiService.GetAsync<List<PromocionDTO>>($"api/Promocion/PorServicio/{_servicio.IdServicio}");
        if (promociones == null || !promociones.Any())
        {
            ResultLabel.Text = "No hay promociones disponibles para este servicio.";
            ResultLabel.IsVisible = true;
            _servicio.Promociones = new List<PromocionDTO>();
        }
        else
        {
            ResultLabel.IsVisible = false;
            _servicio.Promociones = promociones;
        }

        // Refrescar el binding
        BindingContext = null;
        BindingContext = _servicio;
    }
    catch (Exception ex)
    {
        ResultLabel.Text = "Hubo un problema al cargar las promociones. Inténtalo de nuevo más tarde.";
        ResultLabel.IsVisible = true;
        Console.WriteLine($"Error: {ex.Message}");
    }
}



        private async void OnReservarClicked(object sender, EventArgs e)
        {
            try
            {
                // Navegar a la página de selección de empleado
                await Navigation.PushAsync(new SeleccionarEmpleadoPage(_authService, _personaService, _servicio));
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al proceder con la reserva: {ex.Message}";
                ResultLabel.IsVisible = true;
            }
        }
    }
}
