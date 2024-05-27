using System;
using System.Collections.Generic;

namespace APIWebCS5.Models;

public partial class Order
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public DateTime Time { get; set; }

    public byte StatusOrder { get; set; }

    public byte StatusDelivery { get; set; }

    public byte PayMentMethod { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<DetailOrder> DetailOrders { get; set; } = new List<DetailOrder>();
}
