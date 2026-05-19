using MasterServicesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MasterServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [AllowAnonymous] // ✅ Permite el acceso sin autenticación
        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            if (userLogin == null || string.IsNullOrWhiteSpace(userLogin.email) || string.IsNullOrWhiteSpace(userLogin.password))
            {
                return BadRequest("El correo electrónico y la contraseña son obligatorios.");
            }

            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(new
                {
                    Token = token,
                    Message = "Inicio de sesión exitoso."
                });
            }

            return NotFound("Usuario no encontrado o credenciales inválidas.");
        }

        //private Usuario Authenticate(LoginUser userLogin)
        //{
        //    var usuario = _context.Usuarios
        //         .Include(u => u.Rol)
        //         .FirstOrDefault(x =>x.Email.ToLower() == userLogin.email.ToLower()
        //        && x.Contrasena == userLogin.password);
        //    if (usuario != null)
        //    {
        //        return usuario;
        //    }
        //    return null;
        //}

        private string Generate(Usuario user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            // Crear los claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol?.Nombre),
                new Claim("RoleId", user.Rol?.Id.ToString()), 
            };


            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Usuario Authenticate(LoginUser userLogin)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(x => x.Email!.ToLower() == userLogin.email.ToLower());

            if (usuario != null || usuario!.Estado == true )
            {
  
                // Hashear la contraseña ingresada y compararla con la almacenada
                var hashedPassword = HashPassword(userLogin.password);
                if (usuario.Contrasena == hashedPassword)
                {
                    return usuario;
                }
            }
            return null;
        }

        // Método para hashear contraseñas
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }
}
