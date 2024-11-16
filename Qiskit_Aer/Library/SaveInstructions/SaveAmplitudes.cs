using System;
using System.Collections.Generic;

public class SaveAmplitudes : SaveSingleData
{
    /// <summary>
    /// Save complex statevector amplitudes.
    /// </summary>
    /// <param name="numQubits">The number of qubits for the snapshot type.</param>
    /// <param name="parameters">The list of entries to save.</param>
    /// <param name="label">The key for retrieving saved data from results.</param>
    /// <param name="perShot">If true, save a list of amplitude vectors for each shot of the simulation.</param>
    /// <param name="conditional">If true, save the amplitude vector conditional on the classical register values.</param>
    public SaveAmplitudes(int numQubits, List<int> parameters, string label = "amplitudes", bool perShot = false, bool conditional = false)
        : base("save_amplitudes", numQubits, label, perShot, conditional, _FormatAmplitudeParams(parameters, numQubits))
    {
    }
}