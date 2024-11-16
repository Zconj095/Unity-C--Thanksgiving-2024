using System.Collections.Generic;

public class SaveUnitary : SaveSingleData
{
    /// <summary>
    /// Instruction to save the unitary simulator state.
    /// </summary>
    /// <param name="numQubits">The number of qubits for the save instruction.</param>
    /// <param name="label">The key for retrieving saved data from results. Default is "unitary".</param>
    /// <param name="pershot">If true, save a list of unitaries for each shot of the simulation. Default is false.</param>
    public SaveUnitary(int numQubits, string label = "unitary", bool pershot = false)
        : base("save_unitary", numQubits, label, pershot, conditional: false)
    {
    }
}