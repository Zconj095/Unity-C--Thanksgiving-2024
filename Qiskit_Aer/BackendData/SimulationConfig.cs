using System;
using System.Collections.Generic;
using UnityEngine;
public class SimulationConfig
{
    public string Method { get; set; } = "automatic";
    public string Device { get; set; } = "CPU";

    public SimulationConfig MapLegacyMethod(string legacyMethod)
    {
        var legacyMap = new Dictionary<string, (string, string)>
        {
            { "statevector_cpu", ("statevector", "CPU") },
            { "density_matrix_gpu", ("density_matrix", "GPU") }
        };

        if (legacyMap.ContainsKey(legacyMethod))
        {
            var mapped = legacyMap[legacyMethod];
            this.Method = mapped.Item1;
            this.Device = mapped.Item2;
        }

        return this;
    }
}
