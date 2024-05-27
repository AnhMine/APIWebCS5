using System;
using System.Collections.Generic;

namespace APIWebCS5.Models;

public partial class Medium
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public int IdImage { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Image IdImageNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
