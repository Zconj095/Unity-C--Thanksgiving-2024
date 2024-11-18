using System;
using System.Collections.Generic;

public class Pulse
{
    public Dictionary<string, float> Parameters { get; set; }

    public Pulse(Dictionary<string, float> parameters)
    {
        Parameters = parameters;
    }
}
