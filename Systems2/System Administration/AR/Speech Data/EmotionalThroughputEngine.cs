using UnityEngine;
using System.Collections.Generic;

public class EmotionalThroughputEngine
{
    private Dictionary<string, float> emotionPropagation = new Dictionary<string, float>();

    public void PropagateEmotion(string emotion, float intensity)
    {
        if (emotionPropagation.ContainsKey(emotion))
        {
            emotionPropagation[emotion] = Mathf.Max(emotionPropagation[emotion], intensity);
        }
        else
        {
            emotionPropagation[emotion] = intensity;
        }
    }

    public string GetStrongestEmotion()
    {
        float maxIntensity = 0f;
        string strongestEmotion = "NONE";

        foreach (var emotion in emotionPropagation)
        {
            if (emotion.Value > maxIntensity)
            {
                maxIntensity = emotion.Value;
                strongestEmotion = emotion.Key;
            }
        }

        return $"{strongestEmotion} at intensity {maxIntensity:F2}";
    }
}
