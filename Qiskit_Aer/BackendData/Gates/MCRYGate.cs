using System.Collections.Generic;
public class MCRYGate : ControlledGate
{
    public MCRYGate(double theta, int numCtrlQubits, string ctrlState = null)
        : base("mcry", 1 + numCtrlQubits, new List<double> { theta }, new ControlledGate("cry", 2, new List<double> { theta }, null, 0), numCtrlQubits, ctrlState)
    {
    }
}