using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;

namespace ProyectoO.Pages
{
    public partial class PaginaHistorialCitas : ContentPage
    {
        private readonly IPersonaService _personaService;

        public PaginaHistorialCitas(IPersonaService personaService)
        {
            InitializeComponent();

            _personaService = personaService;

            // Implementa la lógica de la página
        }

        // Métodos y eventos de la página...
    }
}
