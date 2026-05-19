using MasterServicesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuditLogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs(
      [FromQuery] int? usuarioId,
      [FromQuery] string? tableName,
      [FromQuery] string? action,
      [FromQuery] DateTime? startDate,
      [FromQuery] DateTime? endDate)
        {
            var query = _context.AuditLogs.AsQueryable();

            // Aplicamos los filtros si se proporcionan
            if (usuarioId.HasValue)
                query = query.Where(a => a.UsuarioId == usuarioId.Value);

            if (!string.IsNullOrEmpty(tableName))
                query = query.Where(a => a.TableName.Contains(tableName));

            if (!string.IsNullOrEmpty(action))
                query = query.Where(a => a.Action.Contains(action));

            if (startDate.HasValue)
                query = query.Where(a => a.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(a => a.Timestamp <= endDate.Value);

            // Realizamos la consulta y seleccionamos los datos necesarios
            var auditLogs = await query
                .OrderByDescending(a => a.Timestamp)
                .Select(a => new
                {
                    a.Id,
                    a.UsuarioId,
                    a.UserName,
                    a.TableName,
                    a.Action,
                    a.Changes, // Obtienes Changes como string (JSON)
                    a.Timestamp
                })
                .ToListAsync();

            if (!auditLogs.Any())
                return NotFound(new MsgResult("No se encontraron registros de auditoría."));

            return Ok(auditLogs); // Retorna los registros de auditoría con Changes como string
        }

    }
}

