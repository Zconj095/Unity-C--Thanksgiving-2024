using UnityEngine;

[System.Serializable]
public class BeliefEmotionalClarity
{
    [SerializeField] private string currentEmotion;
    [SerializeField] private int intensity;

    public string CurrentEmotion => currentEmotion;
    public int Intensity => intensity;

    public void RecognizeEmotion(string emotion, int intensity)
    {
        this.currentEmotion = emotion;
        this.intensity = intensity;
        Debug.Log($"Recognized emotion: {emotion} with intensity: {intensity}");
    }

    public void ControlEmotion()
    {
        if (intensity > 0)
        {
            intensity = 0;
            Debug.Log($"Controlled emotion: {currentEmotion}. Intensity set to 0.");
        }
        else
        {
            Debug.LogWarning($"Emotion {currentEmotion} is not currently active.");
        }
    }
}
