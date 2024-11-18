using UnityEngine;

public class EmotionalState : MonoBehaviour
{
    public float hormonalBalance = 0.85f;  // Balance of hormones affecting emotional state
    public float synapticActivity = 0.75f; // Synaptic connections influencing emotions
    public float emotionalIntensity = 0.8f; // Intensity of current emotional state

    private float emotionalStateValue;

    void Update()
    {
        emotionalStateValue = CalculateEmotionalState();
        Debug.Log("Emotional State: " + emotionalStateValue);
    }

    float CalculateEmotionalState()
    {
        float k7 = 1.0f; // Constant for emotional state
        return k7 * Mathf.Pow(hormonalBalance, 0.4f) * Mathf.Pow(synapticActivity, 0.3f) * Mathf.Pow(emotionalIntensity, 0.3f);
    }
}
