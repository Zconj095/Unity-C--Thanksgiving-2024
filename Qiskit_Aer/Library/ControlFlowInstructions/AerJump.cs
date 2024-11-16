using System;

public class AerJump : Instruction
{
    /// <summary>
    /// Jump instruction
    /// 
    /// This instruction sets a program counter to a specified mark instruction.
    /// </summary>
    public bool IsDirective { get; private set; } = true;
    public Expression ConditionExpression { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AerJump"/> class.
    /// </summary>
    /// <param name="jumpTo">The instruction to jump to.</param>
    /// <param name="numQubits">The number of qubits affected by this instruction.</param>
    /// <param name="numClbits">The number of classical bits affected by this instruction (default: 0).</param>
    public AerJump(object jumpTo, int numQubits, int numClbits = 0)
        : base("jump", numQubits, numClbits, new object[] { jumpTo })
    {
        ConditionExpression = null;
    }

    /// <summary>
    /// Sets a condition to perform this jump instruction.
    /// </summary>
    /// <param name="condition">An Expression to evaluate or a tuple-like structure for conditional jumps.</param>
    /// <returns>The current AerJump instance with the condition set.</returns>
    public AerJump SetConditional(object condition)
    {
        if (condition is Expression expr)
        {
            ConditionExpression = expr;
        }
        else if (condition is Tuple<int, int> condTuple)
        {
            SetCondition(condTuple.Item1, condTuple.Item2);
        }
        else
        {
            throw new ArgumentException("Invalid condition type. Must be Expression or tuple.");
        }

        return this;
    }
}
