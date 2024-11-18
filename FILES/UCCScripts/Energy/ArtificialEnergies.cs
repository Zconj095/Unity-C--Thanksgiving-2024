using UnityEngine;

public class ArtificialEnergies : MonoBehaviour
{
    public float energyGenerationMethod = 0.8f;     // The method used to generate energy artificially
    public float efficiencyOfSystem = 0.7f;         // Efficiency of the artificial energy system
    public float resourceInput = 0.9f;               // Input resources for the system

    private float artificialEnergy;

    void Update()
    {
        artificialEnergy = CalculateArtificialEnergy();
        Debug.Log("Artificial Energy: " + artificialEnergy);
    }

    float CalculateArtificialEnergy()
    {
        float k27 = 1.0f; // Constant
        return k27 * Mathf.Pow(energyGenerationMethod, 0.4f) * Mathf.Pow(efficiencyOfSystem, 0.3f) * Mathf.Pow(resourceInput, 0.3f);
    }
}
