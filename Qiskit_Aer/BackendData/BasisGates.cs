using System.Collections.Generic;

public static class BasisGates
{
    public static readonly Dictionary<string, List<string>> BASIS_GATES = new Dictionary<string, List<string>>()
    {
        { "statevector", new List<string> { "u1", "u2", "u3", "cx", "cz", "h", "x", "y", "z" } },
        { "density_matrix", new List<string> { "cx", "cz", "h", "x", "y", "z", "swap" } },
        { "unitary", new List<string> { "u1", "u2", "u3", "cx", "cz", "h", "swap" } }
    };

    public static List<string> AutomaticBasisGates()
    {
        var allGates = new HashSet<string>();
        foreach (var gates in BASIS_GATES.Values)
        {
            foreach (var gate in gates)
            {
                allGates.Add(gate);
            }
        }
        return new List<string>(allGates);
    }
}
