using UnityEngine;

public class MCSXGate : ControlledGate
{
    public MCSXGate(int numCtrlQubits, string ctrlState = null)
        : base("mcsx", 1 + numCtrlQubits, null, new ControlledGate("csx", 2, null, null, 0), numCtrlQubits, ctrlState)
    {
    }
}