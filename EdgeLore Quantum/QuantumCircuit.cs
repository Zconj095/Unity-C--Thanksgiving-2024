using System.Collections.Generic;
using UnityEngine;

public class QuantumCircuit : MonoBehaviour
{
    public List<QuantumGate> Gates { get; private set; }
    public int NumQubits { get; set; }

    public QuantumCircuit(int numQubits)
    {
        NumQubits = numQubits;
        Gates = new List<QuantumGate>();
    }

    public void AddQubit()
    {
        NumQubits++;
    }


    public void AddGate(QuantumGate gate)
    {
        Gates.Add(gate);
    }

    public override string ToString()
    {
        string result = $"Quantum Circuit with {NumQubits} Qubits:\n";
        foreach (var gate in Gates)
        {
            result += gate.ToString() + "\n";
        }
        return result;
    }
}
