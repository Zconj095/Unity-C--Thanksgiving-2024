using System.Collections.Generic;
public class SaveStatevectorDict : SaveSingleData
{
    /// <summary>
    /// Save statevector as ket-form dictionary instruction.
    /// </summary>
    /// <param name="numQubits">The number of qubits.</param>
    /// <param name="label">The key for retrieving saved data from results. Default is "statevector_dict".</param>
    /// <param name="pershot">If true, save a list of statevectors for each shot of the simulation. Default is false.</param>
    /// <param name="conditional">If true, save data conditional on the current classical register values. Default is false.</param>
    public SaveStatevectorDict(int numQubits, string label = "statevector_dict", bool pershot = false, bool conditional = false)
        : base("save_statevector_dict", numQubits, label, pershot, conditional)
    {
    }
}