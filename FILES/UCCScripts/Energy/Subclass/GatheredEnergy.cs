using UnityEngine;

public class GatheredEnergy : MonoBehaviour
{
    public float gatheringRate = 0.8f;             // Rate at which energy is gathered inward
    public float hostEnergyAbsorption = 0.9f;      // Host's ability to absorb gathered energy
    public float energyConcentration = 0.7f;       // Concentration of energy

    private float gatheredEnergy;

    void Update()
    {
        gatheredEnergy = CalculateGatheredEnergy();
        Debug.Log("Gathered Energy: " + gatheredEnergy);
    }

    float CalculateGatheredEnergy()
    {
        float k31 = 1.0f; // Constant
        return k31 * Mathf.Pow(gatheringRate, 0.4f) * Mathf.Pow(hostEnergyAbsorption, 0.3f) * Mathf.Pow(energyConcentration, 0.3f);
    }
}
