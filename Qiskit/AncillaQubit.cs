using System;
using System.Collections.Generic;

// Ancilla qubit implementation
public class AncillaQubit : Qubit
{
    public AncillaQubit(QuantumRegister register = null, int? index = null) : base(register, index) { }
}