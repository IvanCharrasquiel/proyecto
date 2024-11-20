
using ProyectoO.DTO;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using ProyectoO.Helpers;
using ProyectoO.Pages.Reservas.ProyectoO.DTO;

namespace ProyectoO.Pages.Reservas
{
    public partial class ReprogramarReservaPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ReservaDTO _reserva;
        private readonly ApiService _apiService;

        public ReprogramarReservaPage(IAuthService authService, IPersonaService personaService, ReservaDTO reserva)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _reserva = reserva;
            _apiService = new ApiService(_personaService.BaseUrl);

            BindingContext = new ReprogramarReservaViewModel
            {
                Reserva = _reserva,
                // Inicializar otras propiedades según sea necesario
            };

            LoadDatos();
        }

        private async void LoadDatos()
        {
            try
            {
                // Implementa la lógica para cargar los datos de la reserva y permitir su reprogramación
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
            }
        }

        private async void OnGuardarCambiosClicked(object sender, EventArgs e)
        {
            // Implementa la lógica para guardar los cambios de la reserva
        }
    }

    namespace ProyectoO.DTO
    {
        public class ReprogramarReservaViewModel
        {
            public ReservaDTO Reserva { get; set; }
            // Otras propiedades necesarias
        }
    }
}
