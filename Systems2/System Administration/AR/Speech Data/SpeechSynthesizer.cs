using UnityEngine;

public class SpeechSynthesizer : MonoBehaviour
{
    /// <summary>
    /// Synthesizes speech as an AudioClip.
    /// </summary>
    public static AudioClip SynthesizeSpeech(string text, int sampleRate = 44100, float amplitude = 0.5f)
    {
        float[] samples = GenerateSpeechWaveform(text, sampleRate, amplitude);

        AudioClip clip = AudioClip.Create("SynthesizedSpeech", samples.Length, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }

    /// <summary>
    /// Generates waveform data for given text input.
    /// </summary>
    private static float[] GenerateSpeechWaveform(string text, int sampleRate, float amplitude)
    {
        int samplesPerCharacter = sampleRate / 10; // Approximate duration for each character
        int totalSamples = samplesPerCharacter * text.Length;
        float[] samples = new float[totalSamples];

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            float frequency = GetCharacterFrequency(c);
            int startIndex = i * samplesPerCharacter;

            for (int j = 0; j < samplesPerCharacter; j++)
            {
                float t = (float)j / sampleRate;
                samples[startIndex + j] = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * t);
            }
        }

        return samples;
    }

    /// <summary>
    /// Maps characters to frequencies for simple speech synthesis.
    /// </summary>
    private static float GetCharacterFrequency(char c)
    {
        // Basic mapping of characters to frequencies
        switch (char.ToLower(c))
        {
            case 'a': return 440f; // A (440 Hz)
            case 'e': return 660f; // E (660 Hz)
            case 'i': return 880f; // I (880 Hz)
            case 'o': return 550f; // O (550 Hz)
            case 'u': return 330f; // U (330 Hz)
            default: return 220f;  // Default consonant frequency
        }
    }
}
