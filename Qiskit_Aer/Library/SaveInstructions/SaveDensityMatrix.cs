using System.Collections.Generic;

public class SaveDensityMatrix : SaveAverageData
{
    /// <summary>
    /// Save a reduced density matrix.
    /// </summary>
    /// <param name="numQubits">The number of qubits for the save instruction.</param>
    /// <param name="label">The key for retrieving saved data from results.</param>
    /// <param name="unnormalized">If true, save the unnormalized accumulated or conditional accumulated density matrix over all shots.</param>
    /// <param name="pershot">If true, save a list of density matrices for each shot of the simulation rather than the average over all shots.</param>
    /// <param name="conditional">If true, save the average or pershot data conditional on the current classical register values.</param>
    public SaveDensityMatrix(
        int numQubits,
        string label = "density_matrix",
        bool unnormalized = false,
        bool pershot = false,
        bool conditional = false)
        : base(
            "save_density_matrix",
            numQubits,
            label,
            unnormalized: unnormalized,
            pershot: pershot,
            conditional: conditional)
    {
    }
}