using System;
using System.Collections.Generic;
using System.Linq;
public class PulseQobjInstruction
{
    public string Name { get; set; }
    public List<int> Qubits { get; set; }
    public List<int> Memory { get; set; }
    public int Conditional { get; set; }

    public PulseQobjInstruction(string name)
    {
        Name = name;
        Qubits = new List<int>();
        Memory = new List<int>();
    }
}
