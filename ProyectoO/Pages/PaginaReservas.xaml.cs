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

            // Implementa la l�gica de la p�gina
        }

        // M�todos y eventos de la p�gina...
    }
}
