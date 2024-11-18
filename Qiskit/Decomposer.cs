using System;
using System.Collections.Generic;

public class Decomposer
{
    private string _gateName;
    private List<string> _decompositionGates;

    public Decomposer(string gateName)
    {
        _gateName = gateName;
        _decompositionGates = new List<string>();
    }

    // This method could be customized to decompose gates into fundamental gates
    public void Decompose()
    {
        // Example: Decompose a gate into a simple sequence of basic gates
        if (_gateName == "U3")
        {
            _decompositionGates.Add("Rz");
            _decompositionGates.Add("Rx");
            _decompositionGates.Add("Ry");
        }
        else
        {
            _decompositionGates.Add(_gateName);
        }
    }

    public List<string> GetDecomposedGates()
    {
        return _decompositionGates;
    }
}
