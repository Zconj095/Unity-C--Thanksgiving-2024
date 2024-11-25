using UnityEngine;

public class ARCrossCorrelation
{
    public static float ComputeCorrelation(float[] signal1, float[] signal2)
    {
        if (signal1.Length != signal2.Length)
            throw new System.Exception("Signals must have the same length.");

        int length = signal1.Length;
        float sum = 0;

        for (int i = 0; i < length; i++)
        {
            sum += signal1[i] * signal2[i];
        }

        return sum / length; // Normalize the result
    }

    public static bool ValidateWithCorrelation(float[] referenceSignal, float[] testSignal, float threshold)
    {
        float correlation = ComputeCorrelation(referenceSignal, testSignal);
        Debug.Log($"Correlation: {correlation}");
        return correlation >= threshold;
    }
}
