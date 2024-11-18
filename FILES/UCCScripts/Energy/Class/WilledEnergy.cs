using UnityEngine;

public class WilledEnergy : MonoBehaviour
{
    public float sourceConnectionStrength = 0.9f;  // Strength of the connection between host and source
    public float hostConsent = 0.8f;               // Host's consent to apply energy
    public float sourceConsent = 0.7f;             // Source's consent to apply energy

    private float willedEnergyAmount;

    void Update()
    {
        willedEnergyAmount = CalculateWilledEnergy();
        Debug.Log("Willed Energy: " + willedEnergyAmount);
    }

    float CalculateWilledEnergy()
    {
        float k28 = 1.0f; // Constant
        return k28 * Mathf.Pow(sourceConnectionStrength, 0.4f) * Mathf.Pow(hostConsent, 0.3f) * Mathf.Pow(sourceConsent, 0.3f);
    }
}
