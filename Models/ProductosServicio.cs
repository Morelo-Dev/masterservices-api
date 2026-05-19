using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class ProductosServicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public string? UnidadMedida { get; set; }

    public int? CategoriaId { get; set; }

    public int? ProveedorId { get; set; }

    public string? CodigoInterno { get; set; }

    public bool? Estado { get; set; } = true;
    public bool? ExcluidoIva { get; set; } = false;
    public int? IVA { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    [JsonIgnore]
    public virtual Proveedore? Proveedor { get; set; }
}
