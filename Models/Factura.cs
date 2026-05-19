using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Factura
{
    public int Id { get; set; }

    public int VentaId { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public DateTime? FechaEmision { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public string? Notas { get; set; }

    public int ClienteFiscalId { get; set; }

    public virtual ClientesFiscal ClienteFiscal { get; set; } = null!;

    public virtual ICollection<FacturaEvento> FacturaEventos { get; set; } = new List<FacturaEvento>();

    public virtual ICollection<ItemsFactura> ItemsFacturas { get; set; } = new List<ItemsFactura>();

    public virtual Venta Venta { get; set; } = null!;
}
