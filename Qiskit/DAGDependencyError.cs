using System;

public class DAGDependencyError : Exception
{
    /// <summary>
    /// Base class for errors raised by the DAGDependency object.
    /// </summary>
    public string Msg { get; }

    public DAGDependencyError(params string[] msg) : base(string.Join(" ", msg))
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
