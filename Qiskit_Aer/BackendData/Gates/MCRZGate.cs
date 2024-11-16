using System.Collections.Generic;
public class MCRZGate : ControlledGate
{
    public MCRZGate(double theta, int numCtrlQubits, string ctrlState = null)
        : base("mcrz", 1 + numCtrlQubits, new List<double> { theta }, new ControlledGate("crz", 2, new List<double> { theta }, null, 0), numCtrlQubits, ctrlState)
    {
    }
}