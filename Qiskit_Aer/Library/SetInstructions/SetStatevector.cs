using System;
using System.Collections.Generic;

public class SetStatevector : QuantumInstruction
{
    /// <summary>
    /// Instruction to set the statevector state of the simulator.
    /// </summary>
    /// <param name="state">The statevector to set.</param>
    public SetStatevector(Statevector state) : base("set_statevector", state.NumQubits, 0)
    {
        if (state == null || !state.IsValid())
        {
            throw new ArgumentException("The input statevector is not valid.", nameof(state));
        }
        Parameters.Add(state.Data);
    }
}