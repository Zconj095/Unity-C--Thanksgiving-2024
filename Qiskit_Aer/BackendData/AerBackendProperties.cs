using System;
using System.Collections.Generic;
public class AerBackendProperties
{
    public Dictionary<string, double> GateErrors { get; set; }
    public Dictionary<int, (double T1, double T2)> QubitProperties { get; set; }

    public AerBackendProperties()
    {
        GateErrors = new Dictionary<string, double>();
        QubitProperties = new Dictionary<int, (double, double)>();
    }
}
