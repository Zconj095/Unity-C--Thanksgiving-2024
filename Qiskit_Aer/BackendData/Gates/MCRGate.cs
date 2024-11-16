using System.Collections.Generic;
public class MCRGate : ControlledGate
{
    public MCRGate(double theta, double phi, int numCtrlQubits, string ctrlState = null)
        : base("mcr", 1 + numCtrlQubits, new List<double> { theta, phi }, new ControlledGate("r", 2, new List<double> { theta, phi }, null, 0), numCtrlQubits, ctrlState)
    {
    }
}