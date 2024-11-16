using System;
using System.Collections.Generic;

public class SaveState : SaveSingleData
{
    /// <summary>
    /// Save simulator state.
    /// The format of the saved state depends on the simulation method used.
    /// </summary>
    /// <param name="numQubits">The number of qubits.</param>
    /// <param name="label">The key for retrieving saved data from results. Defaults to the state type if null.</param>
    /// <param name="pershot">If true, save a list of states for each shot of the simulation.</param>
    /// <param name="conditional">If true, save data conditional on the current classical register values.</param>
    public SaveState(int numQubits, string label = null, bool pershot = false, bool conditional = false)
        : base("save_state", numQubits, label ?? "_method_", pershot, conditional)
    {
        // This save instruction must always be performed on the full width of qubits in a circuit.
    }
}