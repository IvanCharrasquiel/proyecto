using ProyectoO.Services.Interfaces;

namespace ProyectoO.Pages;

public partial class PaginaServicios : ContentPage
{
    private readonly IPersonaService _personaService;

    public PaginaServicios(IPersonaService personaService)
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

    private void OnVerservicioClicked(object sender, EventArgs e)
    {
        NavigationToPage(new PaginaGeneralServicios(_personaService));
        OcultarDetalles();
    }
}
