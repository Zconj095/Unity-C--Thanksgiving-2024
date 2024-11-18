using UnityEngine;

public class MentalState : MonoBehaviour
{
    public float cognitiveState = 0.8f;     // Cognitive clarity and mental strength
    public float emotionalState = 0.7f;     // Influence of emotions on the mind
    public float psychologicalBalance = 0.9f; // Psychological balance

    private float mentalStateValue;

    void Update()
    {
        mentalStateValue = CalculateMentalState();
        Debug.Log("Mental State: " + mentalStateValue);
    }

    float CalculateMentalState()
    {
        float k6 = 1.0f; // Constant for mental state
        return k6 * Mathf.Pow(cognitiveState, 0.4f) * Mathf.Pow(emotionalState, 0.3f) * Mathf.Pow(psychologicalBalance, 0.3f);
    }
}
