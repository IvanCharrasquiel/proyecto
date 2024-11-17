using ProyectoO.DTO;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using ProyectoO.Services;

namespace ProyectoO.Pages.Dashboard
{
    public partial class DashboardGeneral : ContentPage
    {
        private readonly IPersonaService _personaService;
        
        public DashboardGeneral(IPersonaService personaService)
        {
            InitializeComponent();

            _personaService = personaService;

            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var currentUser = UserService.Instance.CurrentUser;
            if (currentUser != null)
            {
                WelcomeLabel.Text = $"Bienvenido {currentUser.Nombre} {currentUser.Apellido}!";

                if (UserService.Instance.CurrentRole.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
                {
                    // Agregar contenido específico para Empleados
                    DynamicContent.Children.Add(new Label
                    {
                        Text = "Panel de Empleado",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20
                    });

                    // Crear y agregar el botón "Gestionar Servicios"
                    var gestionarServiciosButton = new Button
                    {
                        Text = "Gestionar Servicios"
                    };
                    gestionarServiciosButton.Clicked += OnGestionarServiciosClicked;
                    DynamicContent.Children.Add(gestionarServiciosButton);

                    // Puedes agregar más botones o controles según tus necesidades
                }
                else if (UserService.Instance.CurrentRole.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                {
                    // Agregar contenido específico para Clientes
                    DynamicContent.Children.Add(new Label
                    {
                        Text = "Panel de Cliente",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20
                    });

                    // Crear y agregar el botón "Reservar Cita"
                    var reservarCitaButton = new Button
                    {
                        Text = "Reservar Cita"
                    };
                    reservarCitaButton.Clicked += OnReservarCitaClicked;
                    DynamicContent.Children.Add(reservarCitaButton);

                    // Crear y agregar el botón "Ver Historial de Citas"
                    var verHistorialCitasButton = new Button
                    {
                        Text = "Ver Historial de Citas"
                    };
                    verHistorialCitasButton.Clicked += OnVerHistorialCitasClicked;
                    DynamicContent.Children.Add(verHistorialCitasButton);

                    // Puedes agregar más botones o controles según tus necesidades
                }
                else
                {
                    // Manejar roles desconocidos
                    DynamicContent.Children.Add(new Label
                    {
                        Text = "Rol desconocido. Contacta al administrador.",
                        TextColor = Colors.Red
                    });
                }
            }
        }

        private void OnGestionarServiciosClicked(object sender, EventArgs e)
        {
            NavigationToPage(new PaginaServicios(_personaService));
            OcultarDetalles();
        }

        private async void OnReservarCitaClicked(object sender, EventArgs e)
        {
            // Navegar a la página de reserva de citas
            await Navigation.PushAsync(new PaginaReservas(_personaService));
        }

        private async void OnVerHistorialCitasClicked(object sender, EventArgs e)
        {
            // Navegar a la página de historial de citas
            await Navigation.PushAsync(new PaginaHistorialCitas(_personaService));
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
