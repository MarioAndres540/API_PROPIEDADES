using System;
using System.Collections.Generic;

namespace API_BienesRaices.Models;

public partial class Property
{
    public int IdProperty { get; set; }

    public string Name { get; set; } = null!;

    public string Addres { get; set; } = null!;

    public decimal Price { get; set; }

    public int CodeInternational { get; set; }

    public int Year { get; set; }

    public int IdOwner { get; set; }

    public virtual Owner IdOwnerNavigation { get; set; } = null!;

    public virtual ICollection<PropertyImage> PropertyImages { get; } = new List<PropertyImage>();

    public virtual ICollection<PropertyTrace> PropertyTraces { get; } = new List<PropertyTrace>();
}
