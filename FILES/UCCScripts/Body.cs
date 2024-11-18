using UnityEngine;

public class Body : MonoBehaviour
{
    public float physicalHealth = 0.9f;        // Physical health status
    public float stamina = 0.8f;               // Stamina level affecting physical actions
    public float biologicalState = 0.7f;       // Biological influences on body performance

    private float bodyState;

    void Update()
    {
        bodyState = CalculateBodyState();
        Debug.Log("Body State: " + bodyState);
    }

    float CalculateBodyState()
    {
        float k10 = 1.0f; // Constant for body state
        return k10 * Mathf.Pow(physicalHealth, 0.4f) * Mathf.Pow(stamina, 0.3f) * Mathf.Pow(biologicalState, 0.3f);
    }
}
