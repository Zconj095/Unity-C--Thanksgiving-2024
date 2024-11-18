using UnityEngine;

public class Soul : MonoBehaviour
{
    public float soulEssence = 0.85f;        // Essence of the soul
    public float internalEnergy = 0.75f;     // Internal energy representing spiritual force
    public float existentialAwareness = 0.8f; // Awareness and connection to soul’s purpose

    private float soulState;

    void Update()
    {
        soulState = CalculateSoulState();
        Debug.Log("Soul State: " + soulState);
    }

    float CalculateSoulState()
    {
        float k11 = 1.0f; // Constant for soul state
        return k11 * Mathf.Pow(soulEssence, 0.5f) * Mathf.Pow(internalEnergy, 0.3f) * Mathf.Pow(existentialAwareness, 0.2f);
    }
}
