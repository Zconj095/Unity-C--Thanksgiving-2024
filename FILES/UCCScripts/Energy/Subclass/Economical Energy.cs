using UnityEngine;

public class EconomicalEnergy : MonoBehaviour
{
    public float environmentalCompatibility = 1.0f; // Compatibility of the host with the environment
    public float passiveAbsorption = 0.6f;          // Energy absorbed passively from the environment
    public float environmentalAdaptation = 0.8f;    // Host's adaptation to the environment

    private float economicalEnergy;

    void Update()
    {
        economicalEnergy = CalculateEconomicalEnergy();
        Debug.Log("Economical Energy: " + economicalEnergy);
    }

    float CalculateEconomicalEnergy()
    {
        float k14 = 1.0f; // Constant
        return k14 * Mathf.Pow(environmentalCompatibility, 0.4f) * Mathf.Pow(passiveAbsorption, 0.3f) * Mathf.Pow(environmentalAdaptation, 0.3f);
    }
}
