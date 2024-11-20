using ProyectoO.DTO;
using ProyectoO.Helpers;
using ProyectoO.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProyectoO.ViewModels
{
    public class ServiciosViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private List<ServicioDTO> _allServicios;

        public ObservableCollection<ServicioDTO> Servicios { get; set; } = new ObservableCollection<ServicioDTO>();

        private string _resultText;
        public string ResultText
        {
            get => _resultText;
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        public ServiciosViewModel(IAuthService authService, IPersonaService personaService)
        {
            _apiService = new ApiService(personaService.BaseUrl);
            LoadServicios();
        }

        public async void LoadServicios()
        {
            try
            {
                ResultText = "Cargando servicios...";
                var serviciosList = await _apiService.GetAsync<List<ServicioDTO>>("api/Servicios");

                _allServicios = serviciosList ?? new List<ServicioDTO>();

                Servicios.Clear();
                foreach (var servicio in _allServicios)
                {
                    Servicios.Add(servicio);
                }

                ResultText = string.Empty;
            }
            catch (Exception ex)
            {
                ResultText = $"Error al cargar servicios: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

