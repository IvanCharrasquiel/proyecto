// Pages/Servicios/SeleccionarHorarioPage.xaml.cs
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
    public partial class SeleccionarHorarioPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ApiService _apiService;
        private readonly ServicioDTO _servicio;
        private readonly EmpleadoDTO _empleado;
        private List<HorarioAtencionDTO> _horariosDisponibles;

        public SeleccionarHorarioPage(IAuthService authService, IPersonaService personaService, ServicioDTO servicio, EmpleadoDTO empleado)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _apiService = new ApiService(_personaService.BaseUrl);
            _servicio = servicio;
            _empleado = empleado;

        }

        private async void LoadHorarios(DateTime fecha)
        {
            try
            {

                // Obtener horarios disponibles para el empleado y la fecha
                _horariosDisponibles = await _apiService.GetAsync<List<HorarioAtencionDTO>>($"api/Empleados/{_empleado.IdEmpleado}/HorariosDisponibles?fecha={fecha:yyyy-MM-dd}&idServicio={_servicio.IdServicio}");

                if (_horariosDisponibles != null && _horariosDisponibles.Any())
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            LoadHorarios(e.NewDate);
        }

        private void OnHorarioSelected(object sender, SelectionChangedEventArgs e)
        {
            var horario = e.CurrentSelection.FirstOrDefault() as HorarioAtencionDTO;
            if (horario != null)
            {
            }
        }

        public static void NavigationToPage(ContentPage nuevaPagina)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
        }
    }
}
