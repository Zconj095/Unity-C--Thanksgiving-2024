using UnityEngine;

public class CognitiveEngine : MonoBehaviour
{
    public EmotionalEngine emotionalEngine;

    public string MakeDecision(string situation)
    {
        var emotion = emotionalEngine.GetCurrentEmotion();
        var intensity = emotionalEngine.GetEmotionIntensity();

        // Decision logic influenced by classified emotion
        switch (emotion)
        {
            case EmotionalEngine.EmotionState.Courage:
                return intensity > 0.7f ? "Take a risky action" : "Proceed confidently";
            case EmotionalEngine.EmotionState.Faith:
                return "Trust the process";
            case EmotionalEngine.EmotionState.Happiness:
                return "Share your joy with others";
            case EmotionalEngine.EmotionState.Joy:
                return "Celebrate!";
            case EmotionalEngine.EmotionState.Serene:
            case EmotionalEngine.EmotionState.Serenity:
                return "Maintain calm and focus";
            default:
                return "Neutral decision";
        }
    }
}
