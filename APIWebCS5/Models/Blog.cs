using System;
using System.Collections.Generic;

namespace APIWebCS5.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string Headline { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime DatePush { get; set; }

    public string Image { get; set; } = null!;

    public int IdAccount { get; set; }

    public virtual Account IdAccountNavigation { get; set; } = null!;
}
