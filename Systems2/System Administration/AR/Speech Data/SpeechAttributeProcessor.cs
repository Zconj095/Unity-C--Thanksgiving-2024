using UnityEngine;

public class SpeechAttributeProcessor
{
    /// <summary>
    /// Calculates the tone of speech based on harmonic content.
    /// </summary>
    public static float CalculateTone(float[] samples)
    {
        float harmonicSum = 0f;
        foreach (float sample in samples)
        {
            harmonicSum += Mathf.Abs(sample);
        }
        return harmonicSum / samples.Length;
    }

    /// <summary>
    /// Calculates the density of speech based on energy and spectral balance.
    /// </summary>
    public static float CalculateDensity(float[] samples)
    {
        float energy = 0f;
        foreach (float sample in samples)
        {
            energy += sample * sample;
        }
        return energy / samples.Length;
    }

    /// <summary>
    /// Calculates clarity using the ratio of voiced to unvoiced segments.
    /// </summary>
    public static float CalculateClarity(float[] samples, int sampleRate)
    {
        int voicedCount = 0;
        int unvoicedCount = 0;

        foreach (float sample in samples)
        {
            if (Mathf.Abs(sample) > 0.01f)
                voicedCount++;
            else
                unvoicedCount++;
        }

        return (float)voicedCount / (voicedCount + unvoicedCount);
    }

    /// <summary>
    /// Calculates crispness by analyzing the high-frequency spectral content.
    /// </summary>
    public static float CalculateCrispness(float[] spectralContent)
    {
        return spectralContent[2]; // High-frequency band
    }

    /// <summary>
    /// Calculates bass by analyzing the low-frequency spectral content.
    /// </summary>
    public static float CalculateBass(float[] spectralContent)
    {
        return spectralContent[0]; // Low-frequency band
    }

    /// <summary>
    /// Calculates treble by analyzing the high-frequency spectral content.
    /// </summary>
    public static float CalculateTreble(float[] spectralContent)
    {
        return spectralContent[2]; // High-frequency band
    }

    /// <summary>
    /// Calculates ambience by estimating reverberation levels in the waveform.
    /// </summary>
    public static float CalculateAmbience(float[] samples, int sampleRate)
    {
        int silenceCount = 0;
        foreach (float sample in samples)
        {
            if (Mathf.Abs(sample) < 0.01f)
                silenceCount++;
        }

        float silenceRatio = (float)silenceCount / samples.Length;
        return silenceRatio; // Higher values indicate more ambience/reverberation
    }
}
