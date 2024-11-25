using UnityEngine;

public class EmotionalAmbienceEngine
{
    private string ambientEmotion = "NEUTRAL";
    private float intensity = 0.5f;

    public void SetAmbience(string emotion, float newIntensity)
    {
        ambientEmotion = emotion;
        intensity = Mathf.Clamp(newIntensity, 0f, 1f);
    }

    public string GetAmbience()
    {
        return $"{ambientEmotion} at intensity {intensity:F2}";
    }

    public void ModulateAmbience(float delta)
    {
        intensity = Mathf.Clamp(intensity + delta, 0f, 1f);
    }
}
