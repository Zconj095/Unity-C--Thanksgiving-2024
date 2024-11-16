using UnityEngine;
using System;
using System.Collections.Generic;
public class AerBackendConfiguration
{
    public string BackendName { get; set; }
    public string Description { get; set; }
    public string BackendVersion { get; set; }
    public int MaxCircuits { get; set; }
    public List<string> BasisGates { get; set; }

    public static AerBackendConfiguration Default()
    {
        return new AerBackendConfiguration
        {
            BackendName = "aer_simulator",
            Description = "Default Aer Simulator",
            BackendVersion = "1.0",
            MaxCircuits = 100,
            BasisGates = new List<string> { "h", "cx", "x", "y", "z", "u1", "u2", "u3" }
        };
    }
}
