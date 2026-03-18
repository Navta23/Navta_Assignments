using System;
using System.Collections.Generic;

namespace DBFirstEFinAsp.netcoreDemo.Models;

public partial class View3
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? City { get; set; }

    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? Freight { get; set; }
}
