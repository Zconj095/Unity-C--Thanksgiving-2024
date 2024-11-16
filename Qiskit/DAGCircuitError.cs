using System;

public class DAGCircuitError : Exception
{
    /// <summary>
    /// Base class for errors raised by the DAGCircuit object.
    /// </summary>
    public string Msg { get; }

    public DAGCircuitError(params string[] msg) : base(string.Join(" ", msg))
    {
        Msg = string.Join(" ", msg);
    }

    public override string ToString()
    {
        /// <summary>
        /// Return the message.
        /// </summary>
        return Msg;
    }
}
