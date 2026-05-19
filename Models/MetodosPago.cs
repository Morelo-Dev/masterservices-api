using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class MetodosPago
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
