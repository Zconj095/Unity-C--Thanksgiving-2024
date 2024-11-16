using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AerQuantumCircuit : MonoBehaviour
{
    public List<QuantumGate> Gates { get; private set; }
    public string Name { get; set; }
    public int NumQubits { get; set; }
    public int NumClbits { get; set; }

    public AerQuantumCircuit(string name, int numQubits, int numClbits)
    {
        Name = name;
        NumQubits = numQubits;
        NumClbits = numClbits;
        Gates = new List<QuantumGate>();
    }

    public void AddGate(QuantumGate gate)
    {
        Gates.Add(gate);
    }

    public void ReplaceGate(QuantumGate oldGate, List<QuantumGate> newGates)
    {
        var index = Gates.IndexOf(oldGate);
        if (index >= 0)
        {
            Gates.RemoveAt(index);
            Gates.InsertRange(index, newGates);
        }
    }

    public AerQuantumCircuit Copy()
    {
        var copy = new AerQuantumCircuit(Name, NumQubits, NumClbits);
        foreach (var gate in Gates)
            copy.Gates.Add(gate.Copy());
        return copy;
    }
}

public class QuantumGate
{
    public string Name { get; private set; }
    public List<int> Qubits { get; private set; }
    public List<int> Clbits { get; private set; }
    public List<object> Parameters { get; private set; }

    public QuantumGate(string name, List<int> qubits, List<int> clbits, List<object> parameters)
    {
        Name = name;
        Qubits = qubits;
        Clbits = clbits;
        Parameters = parameters ?? new List<object>();
    }

    public QuantumGate Copy()
    {
        return new QuantumGate(Name, new List<int>(Qubits), new List<int>(Clbits), new List<object>(Parameters));
    }
}
