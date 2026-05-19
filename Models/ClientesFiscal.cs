using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class ClientesFiscal
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string Nit { get; set; } = null!;

    public string? DireccionFiscal { get; set; }

    public string? Ciudad { get; set; }

    public string? Departamento { get; set; }

    public string? RegimenFiscal { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
