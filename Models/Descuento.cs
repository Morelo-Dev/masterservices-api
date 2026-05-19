using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Descuento
{
    public int Id { get; set; }

    public int? VentaId { get; set; }

    public decimal MontoDescuento { get; set; }

    public string? Descripcion { get; set; }

    public virtual Venta? Venta { get; set; }
}
