using Microsoft.AspNetCore.Mvc;
using APIProyecto.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Contraseña))
                return BadRequest(new { Message = "Por favor, ingresa las credenciales." });

            // Primero, intentamos encontrar al usuario en la tabla de Clientes
            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdPersonaNavigation.Email == loginDTO.Email);

            if (cliente != null)
            {
                // Verificamos la contraseña
                if (BCrypt.Net.BCrypt.Verify(loginDTO.Contraseña, cliente.Contraseña))
                {
                    var token = GenerateJwtToken(cliente.IdPersonaNavigation.Email, "Cliente");
                    return Ok(new
                    {
                        Role = "Cliente",
                        Id = cliente.IdCliente,
                        Nombre = cliente.IdPersonaNavigation.Nombre,
                        IdPersona = cliente.IdPersona,
                        Token = token // Incluir el token en la respuesta
                    });
                }
            }

            // Si no es cliente, intentamos encontrar al usuario en la tabla de Empleados
            var empleado = await _context.Empleados
                .Include(e => e.IdPersonaNavigation)
                .FirstOrDefaultAsync(e => e.IdPersonaNavigation.Email == loginDTO.Email);

            if (empleado != null)
            {
                // Verificamos la contraseña
                if (BCrypt.Net.BCrypt.Verify(loginDTO.Contraseña, empleado.Contraseña))
                {
                    var token = GenerateJwtToken(empleado.IdPersonaNavigation.Email, "Empleado");
                    return Ok(new
                    {
                        Role = "Empleado",
                        Id = empleado.IdEmpleado,
                        Nombre = empleado.IdPersonaNavigation.Nombre,
                        IdPersona = empleado.IdPersona,
                        Token = token // Incluir el token en la respuesta
                    });
                }
            }

            // Si no se encuentra el usuario en ninguna de las tablas
            return Unauthorized(new { Message = "Credenciales incorrectas." });
        }

        private string GenerateJwtToken(string email, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
