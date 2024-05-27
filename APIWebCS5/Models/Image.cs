using System;
using System.Collections.Generic;

namespace APIWebCS5.Models;

public partial class Image
{
    public int Id { get; set; }

    public string Link { get; set; } = null!;

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();
}
