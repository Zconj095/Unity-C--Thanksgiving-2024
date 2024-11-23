using UnityEngine;

public class ExtendedFeatureExtractor
{
    public static float ExtractPitch(float[] samples, int sampleRate)
    {
        // Mocked pitch extraction for demonstration
        return 200f; // Example pitch value
    }

    public static float ExtractEnergy(float[] samples)
    {
        float sum = 0f;
        foreach (var sample in samples)
        {
            sum += sample * sample;
        }
        return sum / samples.Length;
    }

    public static float ExtractVolume(float[] samples)
    {
        float maxAmplitude = 0f;
        foreach (var sample in samples)
        {
            if (Mathf.Abs(sample) > maxAmplitude)
                maxAmplitude = Mathf.Abs(sample);
        }
        return maxAmplitude;
    }

    public static float CalculateSpeechRate(float[] samples, int sampleRate)
    {
        // Approximate number of voiced segments per second
        int voicedSegments = 0;
        for (int i = 1; i < samples.Length; i++)
        {
            if (samples[i] * samples[i - 1] < 0) // Zero-crossing detection
                voicedSegments++;
        }
        return voicedSegments / (samples.Length / (float)sampleRate);
    }

    public static float[] ExtractSpectralContent(float[] samples)
    {
        // Mock spectral content analysis
        return new float[] { 0.1f, 0.3f, 0.6f }; // Example spectrum values
    }
}
