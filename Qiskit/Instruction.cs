using System;
using System.Collections.Generic;

public abstract class Instruction
{
    public string Name { get; }
    public int NumQubits { get; }
    public int NumClassicalBits { get; }
    public List<object> Args { get; }
    public QuantumCircuit Definition { get; set; }

    // Constructor for creating an instruction
    protected Instruction(string name, int numQubits, int numClassicalBits, List<object> args)
    {
        Name = name;
        NumQubits = numQubits;
        NumClassicalBits = numClassicalBits;
        Args = args;
    }

    // Virtual method to define the instruction (you can also make it abstract if not providing a default)
    public virtual void Define()
    {
        // Default implementation can be empty or generic
        Console.WriteLine("Define method not implemented in base Instruction.");
    }
}
