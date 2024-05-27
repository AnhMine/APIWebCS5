using System;
using System.Collections.Generic;

namespace APIWebCS5.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Status { get; set; } = null!;

    public byte Stock { get; set; }

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public string Color { get; set; } = null!;

    public string Hair { get; set; } = null!;

    public string Popular { get; set; } = null!;

    public bool Sex { get; set; }

    public string Size { get; set; } = null!;

    public string StatusHair { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<DetailOrder> DetailOrders { get; set; } = new List<DetailOrder>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();
}
