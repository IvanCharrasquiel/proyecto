using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using ProyectoO.Pages.InicioSesion;
using ProyectoO.Services;
using ProyectoO.Services.Interfaces;

namespace ProyectoO
{
    public partial class App : Application
    {
        public static FlyoutPage FlyoutPage { get; set; }

        public App()
        {
            InitializeComponent();

            // Configurar los servicios
            IAuthService authService = new AuthService("https://3d8e-190-217-65-139.ngrok-free.app");
            IPersonaService personaService = new PersonaService("https://3d8e-190-217-65-139.ngrok-free.app");

            // Inicializar la página de inicio de sesión con los servicios
            MainPage = new NavigationPage(new PaginaInicioSesion(authService, personaService));
        }
    }
}
