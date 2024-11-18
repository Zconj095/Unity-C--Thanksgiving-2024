using System;
using System.Collections.Generic;
using System.Linq;

public class QuantumInstruction
{
    // Name of the quantum gate (e.g., "H", "X", "CNOT")
    public string GateName { get; set; }

    // List of qubits on which the operation is applied
    public List<Qubit> Qubits { get; set; }

    // Optional parameters for gates that require them (e.g., RZ, RX)
    public List<double> Parameters { get; set; }

    // Constructor for basic gates without parameters (e.g., "H", "X", "CNOT")
    public QuantumInstruction(string gateName, List<Qubit> qubits)
    {
        GateName = gateName;
        Qubits = qubits ?? new List<Qubit>();
        Parameters = new List<double>(); // Default to no parameters
    }

    // Constructor for gates with parameters (e.g., "RZ", "RX")
    public QuantumInstruction(string gateName, List<Qubit> qubits, List<double> parameters)
    {
        GateName = gateName;
        Qubits = qubits ?? new List<Qubit>();
        Parameters = parameters ?? new List<double>();
    }

    // Add a qubit to the instruction
    public void AddQubit(Qubit qubit)
    {
        if (!Qubits.Contains(qubit))
        {
            Qubits.Add(qubit);
        }
    }

    // Add a parameter to the instruction (for gates like RX, RZ)
    public void AddParameter(double parameter)
    {
        Parameters.Add(parameter);
    }

    // Get a description of the instruction (for logging or debugging)
    public string GetDescription()
    {
        var qubitIndices = Qubits.Select(q => q.Index).ToList();
        string qubitString = string.Join(", ", qubitIndices);
        string paramString = Parameters.Count > 0 ? $"({string.Join(", ", Parameters)})" : "";

        return $"{GateName} on qubits {qubitString} {paramString}";
    }

    // Apply the quantum instruction to a quantum state (simplified)
    public void ApplyToState(QuantumState state)
    {
        // This would be where you apply the operation to a quantum state.
        // This is a placeholder method, and would need to be expanded
        // depending on how you manage quantum states.
        Console.WriteLine($"Applying {GetDescription()} to quantum state.");
    }
}
