using ProyectoO.DTO;
using ProyectoO.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using ProyectoO.Services;

namespace ProyectoO.Pages.Perfil
{
    public partial class PaginaPerfil : ContentPage
    {
        private readonly IPersonaService _personaService;

        public PaginaPerfil(IPersonaService personaService)
        {
            InitializeComponent();

            _personaService = personaService;

            LoadUserData();
        }

        private void LoadUserData()
        {
            var currentUser = UserService.Instance.CurrentUser;
            if (currentUser != null)
            {
                NombreEntry.Text = currentUser.Nombre;
                ApellidoEntry.Text = currentUser.Apellido;
                CedulaEntry.Text = currentUser.Cedula.ToString();
                TelefonoEntry.Text = currentUser.Telefono;
                EmailEntryPerfil.Text = currentUser.Email;
                DireccionEntry.Text = currentUser.Direccion;

                // Cargar foto de perfil
                if (!string.IsNullOrEmpty(currentUser.FotoPerfil))
                {
                    // Construir la URL completa de la foto de perfil
                    string baseUrl = _personaService.BaseUrl;
                    FotoPerfilImage.Source = $"{baseUrl}/fotos/{currentUser.FotoPerfil}";
                }
                else
                {
                    FotoPerfilImage.Source = "default_profile.png";
                }
            }
        }

        private async void OnCambiarFotoClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecciona una foto de perfil",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    // Mostrar la foto seleccionada localmente
                    FotoPerfilImage.Source = ImageSource.FromFile(result.FullPath);

                    // Validar que IdPersona no sea nulo
                    if (UserService.Instance.CurrentUser.IdPersona.HasValue)
                    {
                        string fotoUrl = await _personaService.UploadProfilePictureAsync(
                            UserService.Instance.CurrentUser.IdPersona.Value,
                            result.FullPath);

                        // Actualizar la URL en el UserService
                        UserService.Instance.CurrentUser.FotoPerfil = fotoUrl;

                        // Actualizar la foto en la interfaz de usuario
                        FotoPerfilImage.Source = fotoUrl;

                        await DisplayAlert("Éxito", "Foto de perfil actualizada correctamente.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "IdPersona es nulo. No se puede actualizar la foto de perfil.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cambiar la foto de perfil: {ex.Message}", "OK");
            }
        }


        private async void OnGuardarCambiosClicked(object sender, EventArgs e)
        {
            var currentUser = UserService.Instance.CurrentUser;
            if (currentUser != null)
            {
                currentUser.Nombre = NombreEntry.Text;
                currentUser.Apellido = ApellidoEntry.Text;
                if (int.TryParse(CedulaEntry.Text, out int cedula))
                {
                    currentUser.Cedula = cedula;
                }
                else
                {
                    await DisplayAlert("Error", "Cédula inválida.", "OK");
                    return;
                }
                currentUser.Telefono = TelefonoEntry.Text;
                currentUser.Direccion = DireccionEntry.Text;

                try
                {
                    // Actualizar la información en el servidor
                    await _personaService.UpdatePersonaAsync(currentUser);

                    // Mostrar mensaje de éxito
                    await DisplayAlert("Éxito", "Información actualizada correctamente.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo actualizar la información: {ex.Message}", "OK");
                }
            }
        }
    }
}
