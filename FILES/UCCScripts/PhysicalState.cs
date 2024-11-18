using UnityEngine;

public class PhysicalState : MonoBehaviour
{
    public float healthLevel = 0.9f;          // Health status of the body
    public float fatigueLevel = 0.7f;         // Current fatigue status
    public float biologicalFactors = 0.8f;    // Biological influences on physical state

    private float physicalStateValue;

    void Update()
    {
        physicalStateValue = CalculatePhysicalState();
        Debug.Log("Physical State: " + physicalStateValue);
    }

    float CalculatePhysicalState()
    {
        float k8 = 1.0f; // Constant for physical state
        return k8 * Mathf.Pow(healthLevel, 0.4f) * Mathf.Pow(fatigueLevel, 0.3f) * Mathf.Pow(biologicalFactors, 0.3f);
    }
}
