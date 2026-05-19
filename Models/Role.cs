using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MasterServicesAPI.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
    [JsonIgnore]

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
