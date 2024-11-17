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
            var contrase�a = PasswordEntry.Text;

            // Validaci�n b�sica de campos
            if (string.IsNullOrWhiteSpace(cedula) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contrase�a) || string.IsNullOrWhiteSpace(direccion))
            {
                ResultLabel.Text = "Por favor, completa todos los campos.";
                return;
            }

            // Intentar convertir 'cedula' a entero
            if (!int.TryParse(cedula, out int cedulaInt))
            {
                ResultLabel.Text = "La c�dula debe ser un n�mero v�lido.";
                return;
            }

            try
            {
                var registroRequest = new RegistroRequestDTO
                {
                    Contrase�a = contrase�a.Trim(),
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

                ResultLabel.Text = "Registro exitoso. Puedes iniciar sesi�n ahora.";

                // Opcional: Navegar a la p�gina de inicio de sesi�n
                await Navigation.PopAsync(); // Regresa a la p�gina anterior (Inicio de Sesi�n)
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error al registrar: {ex.Message}";
            }
        }
    }
}
