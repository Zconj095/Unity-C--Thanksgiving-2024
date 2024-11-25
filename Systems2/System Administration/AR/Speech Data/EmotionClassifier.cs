using UnityEngine;
public class EmotionClassifier : MonoBehaviour
{
    public static string ClassifyEmotion(float pitch, float energy, float volume, float speechRate, float[] spectralContent)
    {
        // Ensure spectralContent has valid length
        if (spectralContent == null || spectralContent.Length < 3)
        {
            UnityEngine.Debug.LogError("Invalid spectralContent array. Must have at least 3 elements.");
            return "UNKNOWN";
        }

        // Log inputs for debugging
        UnityEngine.Debug.Log($"Inputs -> Pitch: {pitch}, Energy: {energy}, Volume: {volume}, SpeechRate: {speechRate}, SpectralContent: [{string.Join(", ", spectralContent)}]");

        // Classify based on input thresholds
        if (pitch > 300 && energy > 0.8f && volume > 0.6f && speechRate > 2.0f)
        {
            return "COURAGE";
        }
        else if (pitch > 200 && energy > 0.7f && spectralContent[1] > 0.5f && volume > 0.5f)
        {
            return "FAITH";
        }
        else if (energy > 0.6f && spectralContent[0] > 0.4f && speechRate > 1.5f)
        {
            return "GRATITUDE";
        }
        else if (pitch > 300 && energy > 0.6f && volume > 0.5f)
        {
            return "HAPPINESS";
        }
        else if (pitch > 250 && energy < 0.6f && spectralContent[2] > 0.4f && speechRate > 1.0f)
        {
            return "HOPE";
        }
        else if (pitch > 300 && energy > 0.7f && spectralContent[1] > 0.6f)
        {
            return "JOY";
        }
        else if (pitch < 250 && energy > 0.6f && spectralContent[0] > 0.5f)
        {
            return "LOVE";
        }
        else if (pitch < 200 && energy < 0.5f && spectralContent[1] < 0.4f)
        {
            return "SERENE";
        }
        else if (pitch < 200 && energy < 0.4f)
        {
            return "SERENITY";
        }
        else if (pitch < 150 && energy > 0.5f && spectralContent[2] > 0.6f)
        {
            return "SERIOUS";
        }
        else if (pitch < 200 && energy < 0.4f && spectralContent[0] > 0.5f)
        {
            return "TEMPERANCE";
        }

        // Default to UNKNOWN if no conditions are met
        return "UNKNOWN";
    }
}
