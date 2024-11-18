using UnityEngine;

public class StoredEnergy : MonoBehaviour
{
    public float storedCapacity = 100.0f;         // Total capacity for storing energy
    public float decayRate = 0.2f;                // Rate at which stored energy decays over time
    public float storageDuration = 50.0f;         // Duration for which energy is stored

    private float storedEnergy;

    void Update()
    {
        storedEnergy = CalculateStoredEnergy();
        Debug.Log("Stored Energy: " + storedEnergy);
    }

    float CalculateStoredEnergy()
    {
        float k15 = 1.0f; // Constant
        return k15 * Mathf.Pow(storedCapacity, 0.6f) * Mathf.Pow(decayRate, 0.2f) * Mathf.Pow(storageDuration, -0.1f);
    }
}
