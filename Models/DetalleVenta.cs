using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class DetalleVenta
{
    public int Id { get; set; }

    public int? VentaId { get; set; }

    public int? ProductoServicioId { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public string? Descripcion { get; set; }

    public decimal? PrecioUnitarioSinIva { get; set; }

    public decimal? IvaPorcentaje { get; set; }

    public decimal? IvaValor { get; set; }

    public decimal? SubtotalSinIva { get; set; }

    public decimal? SubtotalConIva { get; set; }

    public decimal? DescuentoPorcentaje { get; set; }

    public decimal? DescuentoValor { get; set; }

    public virtual ICollection<ItemsFactura> ItemsFacturas { get; set; } = new List<ItemsFactura>();

    public virtual ProductosServicio? ProductoServicio { get; set; }

    public virtual Venta? Venta { get; set; }
}
