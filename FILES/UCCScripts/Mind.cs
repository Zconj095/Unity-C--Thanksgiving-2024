using UnityEngine;

public class Mind : MonoBehaviour
{
    public float cognitiveState = 0.8f;       // Cognitive clarity
    public float emotionalState = 0.7f;       // Emotional state affecting the mind
    public float consciousnessLevel = 0.9f;   // Depth of consciousness and self-awareness

    private float mindState;

    void Update()
    {
        mindState = CalculateMindState();
        Debug.Log("Mind State: " + mindState);
    }

    float CalculateMindState()
    {
        float k9 = 1.0f; // Constant for mind state
        return k9 * Mathf.Pow(cognitiveState, 0.5f) * Mathf.Pow(emotionalState, 0.3f) * Mathf.Pow(consciousnessLevel, 0.2f);
    }
}
