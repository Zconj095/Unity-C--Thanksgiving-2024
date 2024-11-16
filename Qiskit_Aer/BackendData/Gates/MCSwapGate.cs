public class MCSwapGate : ControlledGate
{
    public MCSwapGate(int numCtrlQubits, string ctrlState = null)
        : base("mcswap", 2 + numCtrlQubits, null, new ControlledGate("swap", 2, null, null, 0), numCtrlQubits, ctrlState)
    {
    }
}