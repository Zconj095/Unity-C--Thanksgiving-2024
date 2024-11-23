using UnityEngine;

public class VoiceCommandVerifier
{
    public bool ValidateCommand(AudioClip recordedClip, AudioClip referenceClip)
    {
        float[] recordedSignal = ConvertToFloatArray(recordedClip);
        float[] referenceSignal = ConvertToFloatArray(referenceClip);

        return ARCrossCorrelation.ValidateWithCorrelation(referenceSignal, recordedSignal, 0.8f);
    }

    private float[] ConvertToFloatArray(AudioClip clip)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);
        return samples;
    }
}
