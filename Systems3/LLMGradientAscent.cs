using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class LLMGradientAscent
{
    public static float[] ApplyGradientAscent(float[] parameters, float[] gradients, float learningRate)
    {
        float[] updatedParameters = new float[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            // Use Lerp for smooth parameter updates
            updatedParameters[i] = Mathf.Lerp(parameters[i], parameters[i] + gradients[i], learningRate);
        }

        return updatedParameters;
    }

    public static float ReLU(float x)
    {
        return Mathf.Max(0, x);
    }

    public static float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }

    public static float CombinedActivation(float x)
    {
        return ReLU(x) * Sigmoid(x); // Combines ReLU and Sigmoid outputs
    }

}
