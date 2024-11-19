// Pages/Servicios/ConfirmarReservaPage.xaml.cs
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProyectoO.Pages.Servicios;
using ProyectoO.Pages.Reservas;
using ProyectoO.Services;

namespace ProyectoO.Pages.Servicios
{
    public partial class ConfirmarReservaPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ApiService _apiService;
        private readonly ServicioDTO _servicio;
        private readonly EmpleadoDTO _empleado;
        private readonly HorarioAtencionDTO _horario;

        public ConfirmarReservaPage(IAuthService authService, IPersonaService personaService, ServicioDTO servicio, EmpleadoDTO empleado, HorarioAtencionDTO horario)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _apiService = new ApiService(_personaService.BaseUrl);
            _servicio = servicio;
            _empleado = empleado;
            _horario = horario;

            BindingContext = new ConfirmarReservaViewModel
            {
                Servicio = _servicio,
                Empleado = _empleado,
                Fecha = DatePickerFecha.Date, 
                Horario = _horario,
                PromocionesAplicables = "", // Inicializar
                TienePromociones = false
            };

            LoadPromociones();
        }

        private async void LoadPromociones()
        {
            try
            {
                var promociones = await _apiService.GetAsync<List<PromocionDTO>>($"api/Servicios/{_servicio.IdServicio}/Promociones");
                if (promociones != null && promociones.Any())
                {
                    var descuentos = promociones.Select(p => p.Descuento).Sum();
                    ((ConfirmarReservaViewModel)BindingContext).PromocionesAplicables = $"Descuento Total: {descuentos}%";
                    ((ConfirmarReservaViewModel)BindingContext).TienePromociones = true;
                }
            }
            catch (Exception ex)
            {
                // Manejar errores si es necesario
            }
        }

        private async void OnConfirmarClicked(object sender, EventArgs e)
        {
            try
            {
                ResultLabel.IsVisible = false;

                // Crear la reserva
                var reservaRequest = new ReservaRequestDTO
                {
                    Fecha = ((ConfirmarReservaViewModel)BindingContext).Fecha,
                    HoraInicio = _horario.HoraInicio,
                    HoraFin = _horario.HoraFin,
                    IdCliente = UserService.Instance.CurrentUser.IdPersona.Value, // Asegúrate de que IdPersona no sea nulo
                    IdEmpleado = _empleado.IdEmpleado,
                    Servicios = new List<int> { _servicio.IdServicio } // Asignar el servicio seleccionado
                };

                var reserva = await _apiService.PostAsync<ReservaRequestDTO, ReservaDTO>("api/Reservas", reservaRequest);

                if (reserva != null)
                {
                    await DisplayAlert("Éxito", "Reserva confirmada correctamente.", "OK");
                    // Navegar a la página de historial de reservas o dashboard
                    Application.Current.MainPage = new NavigationPage(new HistorialReservasPage(_authService, _personaService));
                }
                else
                {
                    ResultLabel.Text = "Error al confirmar la reserva.";
                    ResultLabel.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al confirmar la reserva: {ex.Message}";
                ResultLabel.IsVisible = true;
            }
        }

        public static void NavigationToPage(ContentPage nuevaPagina)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
        }
    }



    

    


    
}
