using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using ProyectoO.Services;
using ProyectoO.Pages.Servicios;
using ProyectoO.Pages.Reservas;

namespace ProyectoO.Pages
{
    public partial class Maestro : ContentPage
    {
        private readonly IPersonaService _personaService;
        private readonly IAuthService _authService;

        public Maestro(IPersonaService personaService, IAuthService authService)
        {
            InitializeComponent();

            _personaService = personaService;
            _authService = authService;
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            var currentUser = UserService.Instance.CurrentUser;
            if (currentUser != null)
            {
                NombreLabel.Text = $"{currentUser.Nombre} {currentUser.Apellido}";
                EmailLabel.Text = currentUser.Email;

                // Actualizar la foto de perfil si existe
                if (!string.IsNullOrEmpty(currentUser.FotoPerfil))
                {
                    // Construir la URL completa de la foto de perfil
                    string baseUrl = _personaService.BaseUrl;
                    PerfilImage.Source = $"{baseUrl}/fotos/{currentUser.FotoPerfil}";
                }
                else
                {
                    // Fallback a una imagen por defecto si no hay foto de perfil
                    PerfilImage.Source = "default_profile.png";
                }
            }
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            // Navegar al Dashboard General
            await App.FlyoutPage.Detail.Navigation.PushAsync(new Dashboard.DashboardGeneral(_personaService));
            OcultarDetalles();
        }

        
        private async void OnServicesClicked(object sender, EventArgs e)
        {
            // Navegar a la página de Servicios
            await App.FlyoutPage.Detail.Navigation.PushAsync(new ServiciosPage(_authService,_personaService));
            OcultarDetalles();
        }

        private async void OnVerPerfilClicked(object sender, EventArgs e)
        {
            // Navegar a la página de Perfil
            await App.FlyoutPage.Detail.Navigation.PushAsync(new Perfil.PaginaPerfil(_personaService));
            OcultarDetalles();
        }

        private async void OnCerrarSesionClicked(object sender, EventArgs e)
        {
            // Limpiar el UserService
            UserService.Instance.CurrentUser = null;
            UserService.Instance.CurrentRole = null;

            // Limpiar la información almacenada en SecureStorage
            SecureStorage.Remove("IsLoggedIn");
            SecureStorage.Remove("UserRole");
            SecureStorage.Remove("AuthToken");

            // Navegar de vuelta a la página de inicio de sesión
            await App.FlyoutPage.Detail.Navigation.PushAsync(new InicioSesion.PaginaInicioSesion(new AuthService(_personaService.BaseUrl), _personaService));

            // Limpiar el stack de navegación
            await App.FlyoutPage.Detail.Navigation.PopToRootAsync();

            // Actualizar la página principal
            Application.Current.MainPage = new NavigationPage(new InicioSesion.PaginaInicioSesion(new AuthService(_personaService.BaseUrl), _personaService));
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
