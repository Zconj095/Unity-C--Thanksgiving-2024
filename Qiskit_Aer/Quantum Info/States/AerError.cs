using System;

public class AerError : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AerError"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public AerError(params string[] message) : base(string.Join(" ", message))
    {
    }

    /// <summary>
    /// Returns a string representation of the error message.
    /// </summary>
    /// <returns>The error message as a string.</returns>
    public override string ToString()
    {
        return Message;
    }
}
