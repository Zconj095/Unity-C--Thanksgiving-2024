using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class ActivationAdjuster
{
    public static float AdjustReLU(float x, float distortion)
    {
        return Math.Max(0, x - distortion); // Adjust ReLU based on distortion
    }

    public static float AdjustSigmoid(float x, float distortion)
    {
        float sigmoid = 1 / (1 + Mathf.Exp(-x));
        return sigmoid * (1 - distortion); // Scale sigmoid by distortion
    }
}
