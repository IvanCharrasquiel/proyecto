using ProyectoO.DTO;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using ProyectoO.Services;
using ProyectoO.Pages.Dashboard;
using ProyectoO.Pages.Servicios;

namespace ProyectoO.Pages.InicioSesion
{
    public partial class PaginaInicioSesion : ContentPage
    {
        private readonly IAuthService _authService;
        private readonly IPersonaService _personaService;

        public PaginaInicioSesion(IAuthService authService, IPersonaService personaService)
        {
            InitializeComponent();

            _authService = authService;
            _personaService = personaService;
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro.Registro(_authService));
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var contraseña = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseña))
            {
                ResultLabel.Text = "Por favor, ingresa tu correo y contrase?a.";
                return;
            }

            try
            {
                ResultLabel.Text = "Iniciando sesión...";

                    var loginResponse = await _authService.LoginAsync(email, contraseña);

                    if (loginResponse != null)
                    {
                        // Almacenar el rol del usuario
                        UserService.Instance.CurrentRole = loginResponse.Role;

                        // Obtener los datos de la Persona usando IdPersona
                        var persona = await _personaService.GetPersonaByIdAsync(loginResponse.IdPersona);

                        if (persona != null)
                        {
                            // Almacenar la persona en el UserService
                            UserService.Instance.CurrentUser = persona;

                            // Almacenar el token en SecureStorage si es necesario
                            await SecureStorage.SetAsync("IsLoggedIn", "true");
                            await SecureStorage.SetAsync("UserRole", loginResponse.Role);
                            await SecureStorage.SetAsync("AuthToken", loginResponse.Token);

                            // Establecer el encabezado de autorización para futuras solicitudes
                            _authService.SetAuthorizationHeader(loginResponse.Token);

                            // Navegar al Dashboard correspondiente
                            FlyoutPage flyout = new FlyoutPage
                            {
                                Flyout = new Maestro(_personaService,_authService),
                                Detail = new NavigationPage(GetDashboardPage(loginResponse.Role))
                            };
                            App.FlyoutPage = flyout; // Definido en App.xaml.cs
                            Application.Current.MainPage = flyout;
                        }
                        else
                        {
                            ResultLabel.Text = "No se encontró la información de la persona.";
                        }
                    }
                    else
                    {
                        ResultLabel.Text = "Credenciales inválidas.";
                    }
                }
                catch (Exception ex)
                {
                    // Añade más detalles al mensaje de error
                    ResultLabel.Text = $"Error al iniciar sesión: {ex.Message}";
                }
        }

        //private ContentPage GetDashboardPage(string role)
        //{
        //    if (role.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return new Dashboard.DashboardGeneral(_personaService);
        //    }
        //    else if (role.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return new ClienteDashboard.ClienteDashboardPage(_personaService);
        //    }
        //    else
        //    {
        //        // Página por defecto o manejar roles desconocidos
        //        return new ContentPage
        //        {
        //            Content = new Label
        //            {
        //                Text = "Rol desconocido. Contacta al administrador.",
        //                TextColor = Colors.Red,
        //                HorizontalOptions = LayoutOptions.Center,
        //                VerticalOptions = LayoutOptions.Center
        //            }
        //        };
        //    }
        //}

        private ContentPage GetDashboardPage(string role)
        {
            switch (role.ToLower())
            {
                case "empleado":
                    return new DashboardGeneral(_personaService); // Navegar al dashboard general del empleado
                case "cliente":
                    return new SeleccionarEmpleadoPage(new AuthService(_personaService.BaseUrl), _personaService, new ServicioDTO()); // Flujo inicial para clientes
                default:
                    return new DashboardGeneral(_personaService); // Página genérica por defecto
            }
        }

    }
}
