using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class ServiciosCliente
{
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public string? Servicio { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
