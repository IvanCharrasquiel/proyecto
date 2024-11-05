using DB.Request;
using DB.Response;
using DB.Tools;

namespace DB.Services
{
    public class UserServices : IUServices
    {
        private readonly AppDbContext _db;

        // Inyecta AppDbContext en el constructor
        public UserServices(AppDbContext db)
        {
            _db = db;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userresponse = new UserResponse();

            // Usa el contexto inyectado en lugar de crear uno nuevo
            string spassword = Encrypt.GetSHA256(model.Password);
            var usuario = _db.Personas
                             .Where(d => d.Email == model.Email && d.ContraseñaPersona == spassword)
                             .FirstOrDefault();

            if (usuario == null) return null;

            userresponse.Email = usuario.Email;
            return userresponse;
        }
    }
}
