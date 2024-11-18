using System;
using System.Collections.Generic;

public class Z3BoolRef
{
    public string Name { get; set; }

    public Z3BoolRef(string name)
    {
        Name = name;
    }

    public static Z3BoolRef operator &(Z3BoolRef a, Z3BoolRef b)
    {
        return new Z3BoolRef($"{a.Name} AND {b.Name}");
    }

    public static Z3BoolRef operator |(Z3BoolRef a, Z3BoolRef b)
    {
        return new Z3BoolRef($"{a.Name} OR {b.Name}");
    }

    public static Z3BoolRef operator !(Z3BoolRef a)
    {
        return new Z3BoolRef($"NOT {a.Name}");
    }

    public override string ToString()
    {
        return Name;
    }
}
