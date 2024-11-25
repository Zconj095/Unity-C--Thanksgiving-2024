using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class RecursiveLayer
{
    public float[] Forward(float[] input, float[] feedback, Func<float, float, float> adjustmentFunc)
    {
        float[] adjusted = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            adjusted[i] = adjustmentFunc(input[i], feedback[i]);
        }
        return adjusted;
    }
}
