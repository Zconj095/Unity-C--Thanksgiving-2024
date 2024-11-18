using System;
using System.Collections.Generic;

public class CircuitInstruction
{
    // Instruction name (e.g., 'X', 'H', 'CX', etc.)
    public string Name { get; set; }

    // Qubits involved in this instruction
    public List<int> Qubits { get; set; }

    // Parameters for the instruction (if any, e.g., angle for rotation gates)
    public List<double> Parameters { get; set; }

    // Constructor for the instruction
    public CircuitInstruction(string name, List<int> qubits, List<double> parameters = null)
    {
        Name = name;
        Qubits = qubits ?? new List<int>();
        Parameters = parameters ?? new List<double>();
    }

    // Example of applying the instruction (can be expanded with actual logic)
    public void ApplyInstruction()
    {
        // Example logic for applying the instruction to the qubits
        Console.WriteLine($"Applying {Name} on qubits {string.Join(", ", Qubits)}");

        if (Parameters.Count > 0)
        {
            Console.WriteLine($"With parameters: {string.Join(", ", Parameters)}");
        }
    }

    // Clone method to create a copy of the instruction
    public CircuitInstruction Clone()
    {
        return new CircuitInstruction(Name, new List<int>(Qubits), new List<double>(Parameters));
    }

    // Display the instruction details as a string
    public override string ToString()
    {
        return $"{Name} on qubits {string.Join(", ", Qubits)}" +
               (Parameters.Count > 0 ? $" with parameters {string.Join(", ", Parameters)}" : "");
    }
}
