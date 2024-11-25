using UnityEngine;

public class VoiceSynthesizer : MonoBehaviour
{
    public AudioSource audioSource;

    public void SynthesizeText(string text)
    {
        // Simple phoneme-to-waveform mapping (mock example)
        string[] phonemes = MapTextToPhonemes(text);
        float[] waveform = GenerateWaveform(phonemes);

        // Convert waveform to audio clip
        AudioClip clip = AudioClip.Create("SynthesizedVoice", waveform.Length, 1, 44100, false);
        clip.SetData(waveform, 0);
        audioSource.clip = clip;
        audioSource.Play();
    }

    private string[] MapTextToPhonemes(string text)
    {
        // Simplified example: Convert text to a series of "phonemes"
        return text.ToLower().Split(' ');
    }

    private float[] GenerateWaveform(string[] phonemes)
    {
        int sampleRate = 44100;
        float duration = 0.5f;
        int samplesPerPhoneme = (int)(sampleRate * duration);
        float[] waveform = new float[phonemes.Length * samplesPerPhoneme];

        for (int i = 0; i < phonemes.Length; i++)
        {
            for (int j = 0; j < samplesPerPhoneme; j++)
            {
                // Simple sine wave for each "phoneme"
                waveform[i * samplesPerPhoneme + j] = Mathf.Sin(2 * Mathf.PI * 440 * j / sampleRate);
            }
        }

        return waveform;
    }
}
