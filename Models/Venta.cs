using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal Total { get; set; }

    public int? MetodoPagoId { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public string? TipoDocumento { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? IvaTotal { get; set; }

    public decimal? DescuentoTotal { get; set; }

    public string? Moneda { get; set; }

    public string? CondicionPago { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Descuento> Descuentos { get; set; } = new List<Descuento>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual MetodosPago? MetodoPago { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
