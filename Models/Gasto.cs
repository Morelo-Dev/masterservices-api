using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Gasto
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public decimal Monto { get; set; }

    public DateTime? Fecha { get; set; }
}
