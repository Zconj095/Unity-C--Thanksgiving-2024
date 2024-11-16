using System;
using System.Collections.Generic;

public class SetStabilizer : QuantumInstruction
{
    /// <summary>
    /// Instruction to set the Clifford stabilizer state of the simulator.
    /// </summary>
    /// <param name="state">A Clifford operator.</param>
    public SetStabilizer(Clifford state) : base("set_stabilizer", state.NumQubits, 0)
    {
        if (state == null)
        {
            throw new ArgumentNullException(nameof(state), "Clifford state cannot be null.");
        }
        
        Parameters.Add(state.ToDictionary());
    }
}