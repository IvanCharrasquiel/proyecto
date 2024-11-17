using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using ProyectoO.Services;

namespace ProyectoO.Pages
{
    public partial class Inicial : ContentPage
    {
        private readonly IPersonaService _personaService;

        public Inicial(IPersonaService personaService)
        {
            InitializeComponent();

            _personaService = personaService;
        }

        private async void OnIrDashboardClicked(object sender, EventArgs e)
        {
            // Navegar al Dashboard correspondiente
            FlyoutPage flyout = new FlyoutPage
            {
                Flyout = new Maestro(_personaService),
                Detail = new NavigationPage(new Dashboard.DashboardGeneral(_personaService))
            };
            App.FlyoutPage = flyout; // Definido en App.xaml.cs
            Application.Current.MainPage = flyout;
        }
    }
}
