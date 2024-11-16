using System;
using System.Collections.Generic;
public class CircuitData
{
    public List<object> Qubits { get; set; }

    public CircuitData(List<object> qubits)
    {
        Qubits = qubits;
    }
}