using System;
using System.Collections.Generic;

namespace MasterServicesAPI.Models;

public partial class ReportesFinanciero
{
    public int Id { get; set; }

    public string Periodo { get; set; } = null!;

    public decimal Ingresos { get; set; }

    public decimal Gastos { get; set; }

    public decimal Beneficio { get; set; }

    public DateTime? FechaGeneracion { get; set; }
}
