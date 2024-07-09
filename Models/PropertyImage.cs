using System;
using System.Collections.Generic;

namespace API_BienesRaices.Models;

public partial class PropertyImage
{
    public int IdPropertyImage { get; set; }

    public int IdProperty { get; set; }

    public byte[] File { get; set; } = null!;

    public string Enabled { get; set; } = null!;

    public virtual Property IdPropertyNavigation { get; set; } = null!;
}
