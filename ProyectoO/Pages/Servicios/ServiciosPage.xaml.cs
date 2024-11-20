// Pages/Servicios/ServiciosPage.xaml.cs
using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoO.Pages.Servicios
{
    public partial class ServiciosPage : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;
        private readonly ApiService _apiService;
        private List<ServicioDTO> _allServicios;

        public ServiciosPage(IAuthService authService, IPersonaService personaService)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
            _apiService = new ApiService(_personaService.BaseUrl);

            LoadServicios();
        }

        private async void LoadServicios()
        {
            try
            {
                CollectionViewServicios.IsVisible = false;
                LabelSinServicios.IsVisible = false;
                ResultLabel.Text = "Cargando servicios...";

                _allServicios = await _apiService.GetAsync<List<ServicioDTO>>("api/Servicios");

                if (_allServicios != null && _allServicios.Any())
                {
                    CollectionViewServicios.ItemsSource = _allServicios;
                    CollectionViewServicios.IsVisible = true;
                }
                else
                {
                    LabelSinServicios.IsVisible = true;
                }

                ResultLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al cargar servicios: {ex.Message}";
            }
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = e.NewTextValue.ToLower();
            var filtered = _allServicios.Where(s => s.NombreServicio.ToLower().Contains(keyword) || s.Descripcion.ToLower().Contains(keyword)).ToList();
            CollectionViewServicios.ItemsSource = filtered.Any() ? filtered : null;
            LabelSinServicios.IsVisible = !filtered.Any();
        }

        private void OnServicioSelected(object sender, SelectionChangedEventArgs e)
        {
            var servicio = e.CurrentSelection.FirstOrDefault() as ServicioDTO;
            if (servicio != null)
            {
                NavigationToPage(new ServicioDetailPage(_authService, _personaService, servicio));
                CollectionViewServicios.SelectedItem = null; 
            }
        }
        

        public static void NavigationToPage(ContentPage nuevaPagina)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
        }
        public static void OcultarDetalles()
        {
            App.FlyoutPage.IsPresented = false;
        }
    }
}
