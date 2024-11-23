using UnityEngine;

public class VoiceVerifier : MonoBehaviour
{
    private float[] storedVoiceFeatures = { 220, 0.7f }; // Mock: Stored pitch and energy

    public bool VerifyVoice(AudioClip clip)
    {
        float[] features = ExtractVoiceFeatures(clip);
        return CompareFeatures(features, storedVoiceFeatures);
    }

    private float[] ExtractVoiceFeatures(AudioClip clip)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        // Example: Extract pitch and average energy
        float pitch = EstimatePitch(samples);
        float energy = EstimateEnergy(samples);

        return new float[] { pitch, energy };
    }

    private float EstimatePitch(float[] samples)
    {
        // Mock pitch estimation: Measure periodicity
        return 220; // Assume a fixed pitch for demo
    }

    private float EstimateEnergy(float[] samples)
    {
        float energy = 0f;
        foreach (var sample in samples)
        {
            energy += sample * sample;
        }
        return energy / samples.Length;
    }

    private bool CompareFeatures(float[] inputFeatures, float[] storedFeatures)
    {
        float pitchDiff = Mathf.Abs(inputFeatures[0] - storedFeatures[0]);
        float energyDiff = Mathf.Abs(inputFeatures[1] - storedFeatures[1]);
        return pitchDiff < 50 && energyDiff < 0.1f;
    }
}
