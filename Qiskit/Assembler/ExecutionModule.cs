using System;
using System.Collections.Generic;
using System.Linq;

// Define a module for circuit execution
public class ExecutionModule
{
    public List<QuantumCircuit> CircuitList;
    public Dictionary<string, object> Configuration;
    public Dictionary<string, object> HeaderInfo;

    public ExecutionModule(List<QuantumCircuit> circuits, Dictionary<string, object> config, Dictionary<string, object> header)
    {
        CircuitList = circuits;
        Configuration = config;
        HeaderInfo = header;
    }
}
