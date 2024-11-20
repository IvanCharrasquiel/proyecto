using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoO.Services;

namespace ProyectoO.Pages.Reservas
{
    public partial class HistorialReservasPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ApiService _apiService;
        private List<ReservaDTO> _reservas;

        public HistorialReservasPage(IAuthService authService, IPersonaService personaService)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _apiService = new ApiService(_personaService.BaseUrl);

            LoadReservas();
        }

        private async void LoadReservas()
        {
            try
            {
                CollectionViewReservas.IsVisible = false;
                LabelSinReservas.IsVisible = false;
                ResultLabel.Text = "Cargando reservas...";

                // Obtener reservas del cliente actual
                int idCliente = UserService.Instance.CurrentUser.IdPersona.Value;
                _reservas = await _apiService.GetAsync<List<ReservaDTO>>($"api/Reservas/Cliente/{idCliente}");

                if (_reservas != null && _reservas.Any())
                {
                    CollectionViewReservas.ItemsSource = _reservas;
                    CollectionViewReservas.IsVisible = true;
                }
                else
                {
                    LabelSinReservas.IsVisible = true;
                }

                ResultLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al cargar reservas: {ex.Message}";
            }
        }

        private async void OnReprogramarClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var reserva = button.CommandParameter as ReservaDTO;
            if (reserva != null)
            {
                // Navegar a la página de selección de horario con la reserva existente
                await Navigation.PushAsync(new ReprogramarReservaPage(_authService, _personaService, reserva));
            }
        }

        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var reserva = button.CommandParameter as ReservaDTO;
            if (reserva != null)
            {
                bool confirm = await DisplayAlert("Confirmar Cancelación", "¿Estás seguro de que deseas cancelar esta reserva?", "Sí", "No");
                if (confirm)
                {
                    try
                    {
                        await _apiService.DeleteAsync($"api/Reservas/{reserva.IdReserva}");
                        await DisplayAlert("Éxito", "Reserva cancelada correctamente.", "OK");
                        LoadReservas(); // Recargar la lista
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"No se pudo cancelar la reserva: {ex.Message}", "OK");
                    }
                }
            }
        }
    }
}
