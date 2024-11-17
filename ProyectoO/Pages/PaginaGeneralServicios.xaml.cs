using ProyectoO.Services.Interfaces;

namespace ProyectoO.Pages;

public partial class PaginaGeneralServicios : ContentPage
{
    private readonly IPersonaService _personaService;

    public PaginaGeneralServicios(IPersonaService personaService)
    {
        InitializeComponent();
        _personaService = personaService;

        // Implementa la lógica de la página
    }

    public static void NavigationToPage(ContentPage nuevaPagina)
    {
        App.FlyoutPage.Detail.Navigation.PushAsync(nuevaPagina);
    }


    public static void OcultarDetalles()
    {
        App.FlyoutPage.IsPresented = false;
    }

    private void OnClickedPaginaReserva(object sender, EventArgs e)
    {
        NavigationToPage(new PaginaReservas(_personaService));
        OcultarDetalles();
    }
}