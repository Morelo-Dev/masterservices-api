using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Proveedore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Contacto { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? TipoServicio { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<ProductosServicio> ProductosServicios { get; set; } = new List<ProductosServicio>();
}
