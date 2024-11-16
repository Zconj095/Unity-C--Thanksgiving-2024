using System;
using System.Collections.Generic;

// Quantum bit implementation
public class Qubit : Bit
{
    public Qubit(QuantumRegister register = null, int? index = null) : base(register, index)
    {
        if (register != null && !(register is QuantumRegister))
        {
            throw new CircuitError($"Qubit needs a QuantumRegister and {register.GetType().Name} was provided");
        }
    }
}
