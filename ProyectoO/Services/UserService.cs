using ProyectoO.DTO;

namespace ProyectoO.Services
{
    public class UserService
    {
        // Instancia Singleton
        private static UserService _instance;
        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserService();
                return _instance;
            }
        }

        // Propiedades para el usuario actual
        public PersonaDTO CurrentUser { get; set; }
        public string CurrentRole { get; set; }
        public int CurrentIdUser { get; set; }

        private UserService() { }
    }
}
