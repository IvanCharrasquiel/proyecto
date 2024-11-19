// Pages/Servicios/SeleccionarEmpleadoPage.xaml.cs
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
    public partial class SeleccionarEmpleadoPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ApiService _apiService;
        private readonly ServicioDTO _servicio;
        private List<EmpleadoDTO> _empleadosDisponibles;

        public SeleccionarEmpleadoPage(IAuthService authService, IPersonaService personaService, ServicioDTO servicio)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _apiService = new ApiService(_personaService.BaseUrl);
            _servicio = servicio;

            LoadEmpleados();
        }

        private async void LoadEmpleados()
        {
            try
            {
                CollectionViewEmpleados.IsVisible = false;
                LabelSinEmpleados.IsVisible = false;
                ResultLabel.Text = "Cargando empleados...";

                // Obtener empleados que ofrecen este servicio
                _empleadosDisponibles = await _apiService.GetAsync<List<EmpleadoDTO>>($"api/Servicios/{_servicio.IdServicio}/Empleados");

                if (_empleadosDisponibles != null && _empleadosDisponibles.Any())
                {
                    CollectionViewEmpleados.ItemsSource = _empleadosDisponibles;
                    CollectionViewEmpleados.IsVisible = true;
                }
                else
                {
                    LabelSinEmpleados.IsVisible = true;
                }

                ResultLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al cargar empleados: {ex.Message}";
            }
        }


        private void OnEmpleadoSelected(object sender, SelectionChangedEventArgs e)
        {
            var empleado = e.CurrentSelection.FirstOrDefault() as EmpleadoDTO;
            if (empleado != null)
            {
                NavigationToPage(new SeleccionarHorarioPage(_authService, _personaService, _servicio, empleado));
                CollectionViewEmpleados.SelectedItem = null; // Deseleccionar
            }
        }

        public static void NavigationToPage(ContentPage nuevaPagina)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
        }
    }
}