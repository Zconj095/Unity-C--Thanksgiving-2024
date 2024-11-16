using System;

public class AerMark : Instruction
{
    /// <summary>
    /// Mark instruction
    /// 
    /// This instruction is a destination of a jump instruction.
    /// Conditional is not allowed in the Aer controller.
    /// </summary>
    public bool IsDirective { get; private set; } = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="AerMark"/> class.
    /// </summary>
    /// <param name="name">The name of the mark.</param>
    /// <param name="numQubits">The number of qubits associated with this mark.</param>
    /// <param name="numClbits">The number of classical bits associated with this mark (default: 0).</param>
    public AerMark(string name, int numQubits, int numClbits = 0)
        : base("mark", numQubits, numClbits, new object[] { name })
    {
    }
}