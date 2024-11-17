using ProyectoO.DTO;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;

namespace ProyectoO.Pages.Registro
{
    public partial class Registro : ContentPage
    {
        private readonly IAuthService _authService;

        public Registro(IAuthService authService)
        {
            InitializeComponent();

            _authService = authService;
        }

        private async void OnRegistrarClicked(object sender, EventArgs e)
        {
            var cedula = CedulaEntry.Text;
            var nombre = NombreEntry.Text;
            var apellido = ApellidoEntry.Text;
            var telefono = TelefonoEntry.Text;
            var direccion = DireccionEntry.Text;
            var email = EmailEntry.Text;
            var contraseña = PasswordEntry.Text;

            // Validación básica de campos
            if (string.IsNullOrWhiteSpace(cedula) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseña) || string.IsNullOrWhiteSpace(direccion))
            {
                ResultLabel.Text = "Por favor, completa todos los campos.";
                return;
            }

            // Intentar convertir 'cedula' a entero
            if (!int.TryParse(cedula, out int cedulaInt))
            {
                ResultLabel.Text = "La cédula debe ser un número válido.";
                return;
            }

            try
            {
                var registroRequest = new RegistroRequestDTO
                {
                    Contraseña = contraseña.Trim(),
                    Persona = new PersonaDTO
                    {
                        IdPersona = 0, 
                        Cedula = cedulaInt,
                        Nombre = nombre.Trim(),
                        Apellido = apellido.Trim(),
                        Telefono = telefono.Trim(),
                        Email = email.Trim(),
                        Direccion = direccion.Trim(),
                        FotoPerfil = "defecto.png" 
                    }
                };

                await _authService.RegisterAsync(registroRequest);

                ResultLabel.Text = "Registro exitoso. Puedes iniciar sesión ahora.";

                // Opcional: Navegar a la página de inicio de sesión
                await Navigation.PopAsync(); // Regresa a la página anterior (Inicio de Sesión)
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al registrar: {ex.Message}";
            }
        }
    }
}
