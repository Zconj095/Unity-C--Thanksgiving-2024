using System;
using System.Collections.Generic;

public class SetDensityMatrix : QuantumInstruction
{
    /// <summary>
    /// Instruction to set the density matrix state of the simulator.
    /// </summary>
    /// <param name="state">A density matrix object.</param>
    public SetDensityMatrix(DensityMatrix state) : base("set_density_matrix", state.NumQubits, 0)
    {
        if (!state.IsValid())
        {
            throw new ArgumentException("The input state is not valid", nameof(state));
        }

        Parameters.Add(state.Data);
    }
}