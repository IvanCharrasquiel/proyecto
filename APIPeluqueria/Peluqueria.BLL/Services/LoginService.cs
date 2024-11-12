using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Peluqueria.BLL.Services.Contrats;
using Peluqueria.DAL.Repositories.Contrats;
using Peluqueria.DTO;
using Peluqueria.Model;
using AutoMapper;

namespace Peluqueria.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IGenericRepository<Persona> _personaRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginService(IGenericRepository<Persona> personaRepository, IMapper mapper, IConfiguration configuration)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        // Generar token JWT para el usuario autenticado
        public async Task<string> GenerarToken(SesionDTO sesionDto)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, sesionDto.Email),
                new Claim("IdPersona", sesionDto.IdPersona.ToString()),
                new Claim("Nombre", sesionDto.Nombre),
                new Claim("TipoUsuario", sesionDto.TipoUsuario) // Añadir TipoUsuario al token
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        // Validar credenciales y devolver el token JWT
        public async Task<string> ValidarCredenciales(LoginDTO loginDto)
        {
            // Buscar la persona por el correo electrónico
            var persona = await _personaRepository.Obtener(p => p.Email == loginDto.Email);
            if (persona == null)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas");
            }

            // Determinar si la persona es un Cliente o Empleado y validar la contraseña
            string tipoUsuario;
            bool passwordValido = false;

            if (persona.Clientes.Any()) // Si hay una relación con Cliente
            {
                var cliente = persona.Clientes.First();
                passwordValido = PasswordHelper.VerifyPassword(cliente.Contraseña, loginDto.Password);
                tipoUsuario = "Cliente";
            }
            else if (persona.Empleados.Any()) // Si hay una relación con Empleado
            {
                var empleado = persona.Empleados.First();
                passwordValido = PasswordHelper.VerifyPassword(empleado.Contraseña, loginDto.Password);
                tipoUsuario = "Empleado";
            }
            else
            {
                throw new InvalidOperationException("Tipo de usuario no identificado");
            }

            if (!passwordValido)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas");
            }

            // Mapear a SesionDTO y asignar TipoUsuario
            var sesionDto = _mapper.Map<SesionDTO>(persona);
            sesionDto.TipoUsuario = tipoUsuario;

            // Generar y devolver el token JWT
            return await GenerarToken(sesionDto);
        }


        // Método para cerrar sesión (opcional)
        public Task CerrarSesion(int idUsuario)
        {
            // Lógica para cerrar sesión si la necesitas (ej. invalidar token o sesión)
            return Task.CompletedTask;
        }
    }
}
