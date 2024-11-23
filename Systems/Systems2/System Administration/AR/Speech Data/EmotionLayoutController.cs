using UnityEngine;

public class EmotionLayoutController : MonoBehaviour
{
    public EmotionalLayoutManager layoutManager;

    public void ProcessEmotionData(float pitch, float energy, float volume, float speechRate, float[] spectralContent)
    {
        string classifiedEmotion = EmotionClassifier.ClassifyEmotion(pitch, energy, volume, speechRate, spectralContent);
        layoutManager.UpdateEmotionLayout(classifiedEmotion, pitch, energy, spectralContent);
    }
}
