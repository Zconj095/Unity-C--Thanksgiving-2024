using System;
using System.Collections.Generic;

public class SaveAmplitudesSquared : SaveAverageData
{
    /// <summary>
    /// Save squared statevector amplitudes (probabilities).
    /// </summary>
    /// <param name="numQubits">The number of qubits for the snapshot type.</param>
    /// <param name="parameters">The list of entries to save.</param>
    /// <param name="label">The key for retrieving saved data from results.</param>
    /// <param name="unnormalized">If true, save unnormalized accumulated probabilities over all shots.</param>
    /// <param name="perShot">If true, save a list of probability vectors for each shot of the simulation.</param>
    /// <param name="conditional">If true, save the probability vector conditional on the classical register values.</param>
    public SaveAmplitudesSquared(int numQubits, List<int> parameters, string label = "amplitudes_squared", bool unnormalized = false, bool perShot = false, bool conditional = false)
        : base("save_amplitudes_sq", numQubits, label, unnormalized, perShot, conditional, _FormatAmplitudeParams(parameters, numQubits))
    {
    }
}
