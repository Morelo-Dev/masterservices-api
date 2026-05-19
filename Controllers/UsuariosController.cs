using MasterServicesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MasterServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        [Authorize(Policy = "PuedeLeer")]
        public async Task<ActionResult<MsgResult>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.Include(r => r.Rol).ToListAsync();

            return usuarios.Any()
                ? Ok(new MsgResult("Usuarios obtenidos correctamente.", usuarios))
                : Ok(new MsgResult("No se encontraron usuarios."));
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsgResult>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            return usuario != null
                ? Ok(new MsgResult("Usuario encontrado.", usuario))
                : NotFound(new MsgResult("Usuario no encontrado."));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "PuedeActualizar")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
                return Ok(new MsgResult("Usuario no encontrado."));

            if (usuarioExistente!.Estado == false && usuario.Estado == false)
                return Ok(new MsgResult("El usuario ya está eliminado."));

            List<string> cambios = new List<string>();
            bool passwordChanged = false;

            if (!string.IsNullOrEmpty(usuario.Nombre) && usuario.Nombre != usuarioExistente.Nombre)
            {
                cambios.Add($"Nombre cambiado de '{usuarioExistente.Nombre}' a '{usuario.Nombre}'");
                usuarioExistente.Nombre = usuario.Nombre;
            }

            if (!string.IsNullOrEmpty(usuario.Email) && usuario.Email != usuarioExistente.Email)
            {
                cambios.Add($"Email cambiado de '{usuarioExistente.Email}' a '{usuario.Email}'");
                usuarioExistente.Email = usuario.Email;
            }

            if (!string.IsNullOrEmpty(usuario.Contrasena))
            {
                usuarioExistente.Contrasena = HashPassword(usuario.Contrasena);
                passwordChanged = true;
                cambios.Add("Contraseña actualizada.");
            }

            usuarioExistente.Estado = usuario.Estado ?? usuarioExistente.Estado;

            _context.Entry(usuarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Obtener el usuario actual desde el claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuarioLogueado = userIdClaim != null
                ? await _context.Usuarios.FindAsync(int.Parse(userIdClaim))
                : new Usuario { Id = 1, Nombre = "Usuario Predeterminado" }; // Valor predeterminado

            // Registrar auditoría de cambios
            if (cambios.Any())
            {
                var auditLog = new AuditLog
                {
                    UsuarioId = usuarioLogueado.Id, // Usuario logueado o predeterminado
                    UserName = usuarioLogueado.Nombre,
                    TableName = "usuarios",
                    Action = "Actualización",
                    Changes = string.Join("; ", cambios),
                    Timestamp = DateTime.UtcNow
                };

                _context.AuditLogs.Add(auditLog);
                await _context.SaveChangesAsync();
            }

            return Ok(new MsgResult("Usuario actualizado correctamente.", new
            {
                usuarioExistente.Id,
                usuarioExistente.Nombre,
                usuarioExistente.Email,
                usuarioExistente.Estado
            }));
        }
        [HttpGet("notificaciones")]
        public async Task<IActionResult> GetAuditNotifications()
        {
            var auditLogs = await _context.AuditLogs
                .OrderByDescending(a => a.Timestamp)
                .Select(a => new
                {
                    a.Id,
                    a.UserName,
                    a.Action,
                    a.TableName,
                    a.Changes,
                    a.Timestamp
                })
                .ToListAsync();

            if (!auditLogs.Any())
                return NotFound("No hay notificaciones disponibles.");

            return Ok(auditLogs);
        }

        private async Task SendNotificationFromAuditLogs(IEnumerable<AuditLog> auditLogs)
        {
            foreach (var log in auditLogs)
            {
                // Crear el mensaje basado en los detalles de la auditoría
                string message = $"Se realizó la acción '{log.Action}' en la tabla '{log.TableName}' " +
                                 $"por el usuario '{log.UserName}' a las {log.Timestamp}. " +
                                 $"Detalles: {log.Changes}";

                // Guardar el log en la auditoría (esto ya se hace en tu lógica normal)
                log.AdditionalInfo = message;
            }

            await _context.SaveChangesAsync();  // Guardar los cambios en la auditoría
        }


        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<MsgResult>> PostUsuario(Usuario usuario)
        {
            // Validar el nombre
            if (string.IsNullOrEmpty(usuario.Nombre) || usuario.Nombre.Length < 3)
            {
                return BadRequest(new MsgResult("El nombre del usuario es obligatorio y debe tener al menos 3 caracteres."));
            }

            // Validar el correo electrónico
            if (string.IsNullOrEmpty(usuario.Email) || !Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest(new MsgResult("El correo electrónico es obligatorio y debe tener un formato válido."));
            }

            // Validar la contraseña
            if (string.IsNullOrEmpty(usuario.Contrasena) || usuario.Contrasena.Length < 8)
            {
                return BadRequest(new MsgResult("La contraseña es obligatoria y debe tener al menos 8 caracteres."));
            }

            // Validar rol
            if (usuario.RolId <= 0 || !_context.Roles.Any(r => r.Id == usuario.RolId))
            {
                return BadRequest(new MsgResult("El rol seleccionado no es válido."));
            }

            // Verificar si ya existe un usuario con el mismo correo
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                return BadRequest(new MsgResult("Ya existe un usuario con este correo electrónico."));
            }

            // Si la contraseña no es nula, hacer el hash
            if (!string.IsNullOrEmpty(usuario.Contrasena))
            {
                usuario.Contrasena = HashPassword(usuario.Contrasena);
            }

            // Agregar el usuario al contexto y guardar los cambios
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Responder con el mensaje de éxito
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, new MsgResult("Usuario creado correctamente.", usuario));
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

        // Método para validar los campos del usuario
        private bool ValidateUser(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || usuario.Nombre.Length < 3)
                return false;

            if (string.IsNullOrEmpty(usuario.Email) || !Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            if (!string.IsNullOrEmpty(usuario.Contrasena))
            {
                if (usuario.Contrasena.Length < 8 ||
                    !Regex.IsMatch(usuario.Contrasena, @"[A-Z]") ||
                    !Regex.IsMatch(usuario.Contrasena, @"[0-9]") ||
                    !Regex.IsMatch(usuario.Contrasena, @"[\W_]"))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
