using System;
using System.Collections.Generic;

public class SaveStabilizer : SaveSingleData
{
    /// <summary>
    /// Save Stabilizer instruction.
    /// </summary>
    /// <param name="numQubits">The number of qubits.</param>
    /// <param name="label">The key for retrieving saved data from results.</param>
    /// <param name="pershot">If true, save a list of StabilizerStates for each shot of the simulation.</param>
    /// <param name="conditional">If true, save data conditional on the current classical register values.</param>
    public SaveStabilizer(int numQubits, string label = "stabilizer", bool pershot = false, bool conditional = false)
        : base("save_stabilizer", numQubits, label, pershot, conditional)
    {
        // This save instruction must always be performed on the full width of qubits in a circuit.
    }
}