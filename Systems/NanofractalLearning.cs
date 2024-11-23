using System;
using System.Collections.Generic;
using UnityEngine;
public class NanofractalLearning : MonoBehaviour
{
    public float PredictNextDistortion(float[] fractalFeatures, float[] weights)
    {
        // Simple weighted sum prediction
        float prediction = 0f;
        for (int i = 0; i < fractalFeatures.Length; i++)
        {
            prediction += fractalFeatures[i] * weights[i];
        }

        return prediction;
    }
}
