using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int? VentaId { get; set; }

    public decimal Monto { get; set; }

    public DateTime? Fecha { get; set; }

    public int? MetodoPagoId { get; set; }

    public string? Estado { get; set; }

    public virtual MetodosPago? MetodoPago { get; set; }

    public virtual Venta? Venta { get; set; }
}
