using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;

namespace ProyectoO.Pages
{
    public partial class PaginaReservas : ContentPage
    {
        private readonly IPersonaService _personaService;

        public PaginaReservas(IPersonaService personaService)
        {
            InitializeComponent();

            _personaService = personaService;

            // Implementa la lógica de la página
        }

        // Métodos y eventos de la página...
    }
}
