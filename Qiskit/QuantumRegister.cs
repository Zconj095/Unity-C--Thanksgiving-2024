using System;
using System.Collections.Generic;

// Quantum register implementation
public class QuantumRegister
{
    private static int instancesCounter = 0;
    public string Prefix { get; private set; } = "q";
    protected List<Qubit> qubits = new List<Qubit>();

    public QuantumRegister()
    {
        instancesCounter++;
    }

    public virtual Qubit CreateQubit()
    {
        var qubit = new Qubit(this);
        qubits.Add(qubit);
        return qubit;
    }
}