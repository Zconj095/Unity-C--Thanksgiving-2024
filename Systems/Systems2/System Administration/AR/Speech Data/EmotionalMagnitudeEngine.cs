using UnityEngine;

public class EmotionalMagnitudeEngine
{
    public float CalculateMagnitude(float pitch, float energy, float[] spectralContent)
    {
        float spectralSum = 0;
        foreach (float value in spectralContent)
        {
            spectralSum += value;
        }

        return (pitch * 0.3f + energy * 0.5f + spectralSum * 0.2f);
    }
}
