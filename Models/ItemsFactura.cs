using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class ItemsFactura
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public int DetalleVentaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal IvaPorcentaje { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public virtual DetalleVenta DetalleVenta { get; set; } = null!;

    public virtual Factura Factura { get; set; } = null!;
}
