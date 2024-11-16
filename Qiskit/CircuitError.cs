using System;
using System.Collections.Generic;

public class CircuitError : Exception
{
    public CircuitError(string message) : base(message) { }
}