using System;
using System.Collections.Generic;

public class GaussianSquare : Pulse
{
    public GaussianSquare(Dictionary<string, float> parameters) : base(parameters)
    {
    }

    // Gaussian square pulse logic here
    public float CalculateWidth()
    {
        return Parameters["width"];
    }
}
