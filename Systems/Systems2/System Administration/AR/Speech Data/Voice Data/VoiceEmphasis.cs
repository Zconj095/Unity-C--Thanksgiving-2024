using UnityEngine;

public class VoiceEmphasis : MonoBehaviour
{
    public string DetectEmphasis(AudioClip clip)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        // Analyze amplitude changes
        float[] amplitude = AnalyzeAmplitude(samples);

        // Detect emphasis based on peaks
        return DetectPeaks(amplitude);
    }

    private float[] AnalyzeAmplitude(float[] samples)
    {
        int windowSize = 1024; // Analyze in chunks
        float[] amplitudes = new float[samples.Length / windowSize];

        for (int i = 0; i < amplitudes.Length; i++)
        {
            float sum = 0;
            for (int j = 0; j < windowSize; j++)
            {
                sum += Mathf.Abs(samples[i * windowSize + j]);
            }
            amplitudes[i] = sum / windowSize;
        }

        return amplitudes;
    }

    private string DetectPeaks(float[] amplitudes)
    {
        for (int i = 1; i < amplitudes.Length - 1; i++)
        {
            if (amplitudes[i] > amplitudes[i - 1] && amplitudes[i] > amplitudes[i + 1])
            {
                return "Emphasis Detected";
            }
        }
        return "No Emphasis";
    }
}
