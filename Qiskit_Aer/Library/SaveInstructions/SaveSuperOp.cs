using System.Collections.Generic;

public class SaveSuperOp : SaveSingleData
{
    /// <summary>
    /// Instruction to save a SuperOp matrix.
    /// </summary>
    /// <param name="numQubits">The number of qubits for the save instruction.</param>
    /// <param name="label">The key for retrieving saved data from results. Default is "superop".</param>
    /// <param name="pershot">If true, save a list of SuperOp matrices for each shot of the simulation. Default is false.</param>
    public SaveSuperOp(int numQubits, string label = "superop", bool pershot = false)
        : base("save_superop", numQubits, label, pershot, conditional: false)
    {
    }
}