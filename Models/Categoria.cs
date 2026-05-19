using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MasterServicesAPI.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductosServicio> ProductosServicios { get; set; } = new List<ProductosServicio>();
}
