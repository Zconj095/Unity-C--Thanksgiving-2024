using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class InstructionSet
{
    public string Name { get; set; }
    public List<string> Instructions { get; set; }

    public InstructionSet(string name, List<string> instructions)
    {
        Name = name;
        Instructions = instructions;
    }
}
