using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? Contrasena { get; set; } = null!;

    public int? RolId { get; set; }
    public bool? Estado { get; set; } = true;

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<FacturaEvento> FacturaEventos { get; set; } = new List<FacturaEvento>();
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual Role? Rol { get; set; }
}
