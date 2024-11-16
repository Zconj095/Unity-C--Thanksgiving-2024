using System.Collections.Generic;
public class MCU3Gate : ControlledGate
{
    public MCU3Gate(double theta, double phi, double lambda, int numCtrlQubits, string ctrlState = null)
        : base("mcu3", 1 + numCtrlQubits, new List<double> { theta, phi, lambda }, new ControlledGate("u3", 2, new List<double> { theta, phi, lambda }, null, 0), numCtrlQubits, ctrlState)
    {
    }
}