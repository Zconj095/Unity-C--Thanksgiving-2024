using System.Collections.Generic;

public class SaveStatevector : SaveSingleData
{
    /// <summary>
    /// Save statevector instruction.
    /// </summary>
    /// <param name="numQubits">The number of qubits.</param>
    /// <param name="label">The key for retrieving saved data from results. Default is "statevector".</param>
    /// <param name="pershot">If true, save a list of statevectors for each shot of the simulation. Default is false.</param>
    /// <param name="conditional">If true, save data conditional on the current classical register values. Default is false.</param>
    public SaveStatevector(int numQubits, string label = "statevector", bool pershot = false, bool conditional = false)
        : base("save_statevector", numQubits, label, pershot, conditional)
    {
    }
}