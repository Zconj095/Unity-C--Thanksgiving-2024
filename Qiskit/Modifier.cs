using System;
using System.Collections.Generic;
using System.Linq;

public class Modifier
{
    public string Name { get; set; }  // Name of the modifier, e.g., 'Conjugate' or 'Inverse'

    public Modifier(string name)
    {
        Name = name;
    }

    // You can add more properties or methods that define how this modifier behaves
}
