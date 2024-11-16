using System;
using System.Collections.Generic;

// Base class for Qubits and similar entities
public class Bit
{
    protected QuantumRegister register;
    protected int? index;

    public Bit(QuantumRegister register = null, int? index = null)
    {
        this.register = register;
        this.index = index;
    }
}