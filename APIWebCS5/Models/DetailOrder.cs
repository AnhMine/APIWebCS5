using System;
using System.Collections.Generic;

namespace APIWebCS5.Models;

public partial class DetailOrder
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public byte Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
