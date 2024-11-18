using System;
using System.Collections.Generic;

public class CircuitInst
{
    public string Name { get; set; }
    public List<float> Params { get; set; }
    public List<int> Qubits { get; set; }

    public CircuitInst(string name, List<float> parameters, List<int> qubits)
    {
        Name = name;
        Params = parameters;
        Qubits = qubits;
    }

    // Method for validating a circuit instruction
    public bool Validate()
    {
        return Params.Count > 0 && Qubits.Count > 0;
    }

    // Method to convert to a string representation (for debugging purposes)
    public override string ToString()
    {
        return $"{Name}({string.Join(", ", Params)}) on qubits {string.Join(", ", Qubits)}";
    }
}
