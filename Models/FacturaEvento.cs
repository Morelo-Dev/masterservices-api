using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class FacturaEvento
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public string TipoEvento { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? Fecha { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Factura Factura { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
