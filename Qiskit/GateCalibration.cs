using System;
using System.Collections.Generic;

public class GateCalibration
{
    public string GateName { get; set; }
    public List<int> Qubits { get; set; }
    public List<double> Parameters { get; set; }
    public List<string> Schedule { get; set; }

    public GateCalibration(string gateName, List<int> qubits, List<double> parameters, List<string> schedule)
    {
        GateName = gateName;
        Qubits = qubits;
        Parameters = parameters;
        Schedule = schedule;
    }
}
