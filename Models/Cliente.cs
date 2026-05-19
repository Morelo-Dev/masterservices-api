using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Departamento { get; set; }

    public string? CodigoPostal { get; set; }

    public virtual ICollection<ClientesFiscal> ClientesFiscals { get; set; } = new List<ClientesFiscal>();

    public virtual ICollection<ServiciosCliente> ServiciosClientes { get; set; } = new List<ServiciosCliente>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
