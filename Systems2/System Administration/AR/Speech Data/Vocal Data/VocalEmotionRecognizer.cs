using UnityEngine;

public class VocalEmotionRecognizer : MonoBehaviour
{
    public string RecognizeEmotion(AudioClip clip)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        float pitch = ExtractPitch(samples);
        float energy = CalculateEnergy(samples);

        // Simple classification based on pitch and energy
        if (pitch > 300 && energy > 0.7f)
            return "Happy";
        else if (pitch < 150 && energy < 0.3f)
            return "Sad";
        else if (pitch > 300 && energy > 0.9f)
            return "Angry";
        else
            return "Neutral";
    }

    private float ExtractPitch(float[] samples)
    {
        // Simple periodicity analysis for pitch (mock example)
        return 200; // Assume a fixed pitch for demo purposes
    }

    private float CalculateEnergy(float[] samples)
    {
        float sum = 0;
        foreach (var sample in samples)
        {
            sum += sample * sample;
        }
        return sum / samples.Length;
    }
}
