using System;
using System.Collections.Generic;

// Ancilla register implementation
public class AncillaRegister : QuantumRegister
{
    private static int ancillaInstancesCounter = 0;

    public AncillaRegister()
    {
        Prefix = "a";
        ancillaInstancesCounter++;
    }

    public override Qubit CreateQubit()
    {
        var qubit = new AncillaQubit(this);
        qubits.Add(qubit);
        return qubit;
    }
}