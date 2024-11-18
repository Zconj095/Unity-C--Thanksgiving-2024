using UnityEngine;

public class NaturalEnergies : MonoBehaviour
{
    public float energySourceStrength = 0.9f;    // Strength of natural energy sources (e.g., solar, wind)
    public float energyFlowBalance = 0.8f;       // Balance in energy flow
    public float environmentalHarmony = 0.7f;    // Environmental harmony affecting energy

    private float naturalEnergyLevel;

    void Update()
    {
        naturalEnergyLevel = CalculateNaturalEnergy();
        Debug.Log("Natural Energy Level: " + naturalEnergyLevel);
    }

    float CalculateNaturalEnergy()
    {
        float k26 = 1.0f; // Constant
        return k26 * Mathf.Pow(energySourceStrength, 0.4f) * Mathf.Pow(energyFlowBalance, 0.3f) * Mathf.Pow(environmentalHarmony, 0.3f);
    }
}
