public class MCZGate : ControlledGate
{
    public MCZGate(int numCtrlQubits, string ctrlState = null)
        : base("mcz", 1 + numCtrlQubits, null, new ControlledGate("cz", 2, null, null, 0), numCtrlQubits, ctrlState)
    {
    }
}