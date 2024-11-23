using UnityEngine;

public class VocalMoodSynthesizer : MonoBehaviour
{
    public AudioSource audioSource;

    public void SynthesizeMood(string mood)
    {
        float frequency = 440f; // Default frequency for neutral tone
        float duration = 1f;

        if (mood == "Calm")
            frequency = 220f; // Lower frequency for calmness
        else if (mood == "Excited")
            frequency = 660f; // Higher frequency for excitement

        float[] waveform = GenerateSineWave(frequency, duration);
        AudioClip clip = AudioClip.Create("MoodTone", waveform.Length, 1, 44100, false);
        clip.SetData(waveform, 0);
        audioSource.clip = clip;
        audioSource.Play();
    }

    private float[] GenerateSineWave(float frequency, float duration)
    {
        int sampleRate = 44100;
        int sampleCount = (int)(sampleRate * duration);
        float[] waveform = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            waveform[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
        }

        return waveform;
    }
}
