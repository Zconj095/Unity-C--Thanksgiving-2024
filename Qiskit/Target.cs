using System;
using System.Collections.Generic;

public class Target
{
    // Target system parameters, such as the number of qubits and gates supported
    public int NumQubits { get; set; }
    public List<string> SupportedGates { get; set; }
    
    // Constructor to initialize the target system
    public Target(int numQubits, List<string> supportedGates)
    {
        NumQubits = numQubits;
        SupportedGates = supportedGates;
    }

    // Example method to check if a gate is supported by the target system
    public bool IsGateSupported(string gateName)
    {
        return SupportedGates.Contains(gateName);
    }

    // Example method to get the qubit count for the target system
    public int GetQubitCount()
    {
        return NumQubits;
    }

    // Method to provide additional information about the target system
    public void PrintTargetInfo()
    {
        Console.WriteLine($"Target System with {NumQubits} qubits.");
        Console.WriteLine("Supported Gates: " + string.Join(", ", SupportedGates));
    }
}
