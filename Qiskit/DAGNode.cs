using System;
using System.Collections.Generic;
using System.Linq;

public class DAGNode
{
    public IOperation Operation { get; set; }
    public List<int> Qubits { get; set; }

    public DAGNode(IOperation operation, List<int> qubits)
    {
        Operation = operation;
        Qubits = qubits;
    }
}
