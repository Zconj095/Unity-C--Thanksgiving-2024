using System;
using System.Collections.Generic;
using System.Linq;

public interface IOperation
{
    string Name { get; }
    List<int> Qubits { get; }
    bool HasClassicalBits { get; }
}
