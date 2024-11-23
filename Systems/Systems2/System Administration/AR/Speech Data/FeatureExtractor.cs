using UnityEngine;

public class FeatureExtractor
{
    public static float ExtractPitch(float[] samples, int sampleRate)
    {
        // Mocked FFT-based pitch detection
        return 220.0f; // Example pitch value
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
        float sumSquares = 0f;
        foreach (var sample in samples)
        {
            sumSquares += sample * sample;
        }
        return Mathf.Sqrt(sumSquares / samples.Length); // RMS Volume
    }

    public static float CalculateSpeechRate(float[] samples, int sampleRate)
    {
        int voicedSegments = 0;
        float threshold = 0.01f; // Energy threshold
        for (int i = 1; i < samples.Length; i++)
        {
            if (Mathf.Abs(samples[i]) > threshold && samples[i] * samples[i - 1] < 0)
            {
                voicedSegments++;
            }
        }
        return (float)voicedSegments / (samples.Length / (float)sampleRate);
    }

    public static float[] ExtractSpectralContent(float[] samples)
    {
        int fftSize = 1024; // FFT size
        float[] spectrum = new float[fftSize / 2];
        // Mock implementation: Replace with actual FFT
        for (int i = 0; i < spectrum.Length; i++)
        {
            spectrum[i] = Random.Range(0f, 1f); // Mocked spectrum values
        }
        return spectrum;
    }
}
