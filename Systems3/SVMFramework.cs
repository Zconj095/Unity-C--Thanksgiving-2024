using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class SVMFramework
{
    public float[] SupportVectors { get; private set; }
    public float Bias { get; private set; }

    public SVMFramework(float[] supportVectors, float bias)
    {
        SupportVectors = supportVectors;
        Bias = bias;
    }

    public float Classify(float[] input)
    {
        if (input.Length != SupportVectors.Length)
        {
            throw new Exception("Input dimensions must match support vector dimensions.");
        }

        float sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            sum += input[i] * SupportVectors[i];
        }

        return sum + Bias;
    }
}
