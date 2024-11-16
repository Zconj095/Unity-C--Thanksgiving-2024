using System;
using System.Collections.Generic;

public class SetUnitary : QuantumInstruction
{
    /// <summary>
    /// Instruction to set the unitary state of the simulator.
    /// </summary>
    /// <param name="state">A unitary operator.</param>
    public SetUnitary(Operator state) : base("set_unitary", state.NumQubits, 0)
    {
        if (!state.IsUnitary())
        {
            throw new ArgumentException("The input matrix is not unitary.", nameof(state));
        }
        Parameters.Add(state.Data);
    }
}
