using System.Collections.Generic;
using UnityEngine;

public class HiddenTimeField
{
    public float TimeStamp { get; set; } // Time variable
    public Dictionary<string, float> Variables { get; set; } // Hidden variables

    public HiddenTimeField(float timeStamp)
    {
        TimeStamp = timeStamp;
        Variables = new Dictionary<string, float>();
    }
}
