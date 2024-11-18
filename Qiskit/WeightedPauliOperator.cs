using System;
using System.Collections.Generic;

public class WeightedPauliOperator
{
    public List<string> PauliStrings { get; set; }

    public WeightedPauliOperator()
    {
        PauliStrings = new List<string> { "X", "Y", "Z" };  // Example Pauli strings
    }
}
