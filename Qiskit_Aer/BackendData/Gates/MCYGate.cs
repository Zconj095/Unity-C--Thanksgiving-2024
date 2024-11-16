using UnityEngine;

public class MCYGate : ControlledGate
{
    public MCYGate(int numCtrlQubits, string ctrlState = null)
        : base("mcy", 1 + numCtrlQubits, null, new ControlledGate("cy", 2, null, null, 0), numCtrlQubits, ctrlState)
    {
    }
}