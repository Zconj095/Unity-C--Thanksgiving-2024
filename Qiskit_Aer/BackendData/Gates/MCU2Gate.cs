using System.Collections.Generic;
public class MCU2Gate : ControlledGate
{
    public MCU2Gate(double theta, double lambda, int numCtrlQubits, string ctrlState = null)
        : base("mcu2", 1 + numCtrlQubits, new List<double> { theta, lambda }, new ControlledGate("u2", 2, new List<double> { theta, lambda }, null, 0), numCtrlQubits, ctrlState)
    {
    }
}