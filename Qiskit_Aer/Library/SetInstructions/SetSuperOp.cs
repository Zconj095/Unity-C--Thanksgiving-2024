public class SetSuperOp : QuantumInstruction
{
    /// <summary>
    /// Instruction to set the superop state of the simulator.
    /// </summary>
    /// <param name="state">A CPTP quantum channel.</param>
    public SetSuperOp(SuperOp state) : base("set_superop", state.NumQubits, 0)
    {
        if (state == null || !state.IsCPTP())
        {
            throw new ArgumentException("The input quantum channel is not CPTP.", nameof(state));
        }
        Parameters.Add(state.Data);
    }
}