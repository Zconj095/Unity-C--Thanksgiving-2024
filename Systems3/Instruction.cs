using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Instruction
{
    public int Priority { get; set; }
    public string Trigger { get; set; } // e.g., "greet", "persona:assistant"
    public string ResponseTemplate { get; set; }
}

