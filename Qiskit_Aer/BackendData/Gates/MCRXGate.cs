using System.Collections.Generic;
public class MCRXGate : ControlledGate
{
    public MCRXGate(double theta, int numCtrlQubits, string ctrlState = null)
        : base("mcrx", 1 + numCtrlQubits, new List<double> { theta }, new ControlledGate("crx", 2, new List<double> { theta }, null, 0), numCtrlQubits, ctrlState)
    {
    }
}