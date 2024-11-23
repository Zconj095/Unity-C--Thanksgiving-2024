using UnityEngine;

public class EmotionalEngine : MonoBehaviour
{
    public enum EmotionState
    {
        Courage, Faith, Gratitude, Happiness, Hope, Joy, Love, Serene, Serenity, Serious, Temperance, Unknown
    }

    public EmotionState currentEmotion = EmotionState.Unknown;
    public float emotionIntensity = 0.5f; // Ranges from 0.0 to 1.0

    // Update emotion dynamically with flexible inputs
    public string UpdateEmotion(float? pitch, float? energy, float? volume, float? speechRate, float[] spectralContent)
    {
        // Assign default values to missing inputs
        float validPitch = pitch ?? 0.0f;
        float validEnergy = energy ?? 0.5f; // Default to neutral energy
        float validVolume = volume ?? 0.5f; // Default to medium volume
        float validSpeechRate = speechRate ?? 1.0f; // Default to normal speech rate
        float[] validSpectralContent = spectralContent ?? new float[] { 0.0f, 0.0f, 0.0f }; // Default spectral content

        if (validSpectralContent.Length < 3)
        {
            Debug.LogWarning("Spectral content must have at least 3 elements. Filling with defaults.");
            validSpectralContent = new float[] { 0.0f, 0.0f, 0.0f };
        }

        // Classify emotion using flexible inputs
        string emotionName = ClassifyEmotion(validPitch, validEnergy, validVolume, validSpeechRate, validSpectralContent);

        // Update the internal state based on the classified emotion
        if (System.Enum.TryParse(emotionName, true, out EmotionState parsedEmotion))
        {
            currentEmotion = parsedEmotion;
        }
        else
        {
            currentEmotion = EmotionState.Unknown;
        }

        // Calculate intensity as a weighted average
        emotionIntensity = Mathf.Clamp((validEnergy + validVolume + validSpectralContent[0]) / 3.0f, 0.0f, 1.0f);

        return emotionName;
    }

    private string ClassifyEmotion(float pitch, float energy, float volume, float speechRate, float[] spectralContent)
    {
        // Ensure valid spectral content
        if (spectralContent == null || spectralContent.Length < 3)
        {
            Debug.LogWarning("Invalid spectral content provided. Defaulting to zeros.");
            spectralContent = new float[] { 0.0f, 0.0f, 0.0f };
        }

        // Refined conditions for emotion classification
        if (pitch > 300 && energy > 0.8f && volume > 0.6f && speechRate > 2.0f)
        {
            return "Courage";
        }
        if (pitch > 200 && energy > 0.7f && spectralContent[1] > 0.5f && volume > 0.5f)
        {
            return "Faith";
        }
        if (energy > 0.6f && spectralContent[0] > 0.4f && speechRate > 1.5f)
        {
            return "Gratitude";
        }
        if (pitch > 300 && energy > 0.6f && volume > 0.5f)
        {
            return "Happiness";
        }
        if (pitch > 250 && energy < 0.6f && spectralContent[2] > 0.4f && speechRate > 1.0f)
        {
            return "Hope";
        }
        if (pitch > 300 && energy > 0.7f && spectralContent[1] > 0.6f)
        {
            return "Joy";
        }
        if (pitch < 250 && energy > 0.6f && spectralContent[0] > 0.5f)
        {
            return "Love";
        }
        if (pitch < 200 && energy < 0.5f && spectralContent[1] < 0.4f)
        {
            return "Serene";
        }
        if (pitch < 200 && energy < 0.4f)
        {
            return "Serenity";
        }
        if (pitch < 150 && energy > 0.5f && spectralContent[2] > 0.6f)
        {
            return "Serious";
        }
        if (pitch < 200 && energy < 0.4f && spectralContent[0] > 0.5f)
        {
            return "Temperance";
        }

        // Default case
        return "Unknown";
    }

    public EmotionState GetCurrentEmotion()
    {
        return currentEmotion;
    }

    public float GetEmotionIntensity()
    {
        return emotionIntensity;
    }
}
