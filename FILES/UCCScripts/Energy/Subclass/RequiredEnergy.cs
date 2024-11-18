using UnityEngine;

public class RequiredEnergy : MonoBehaviour
{
    public float basicEnergyRequirement = 50.0f;   // Minimum energy required for survival
    public float maintenanceCost = 20.0f;          // Energy required for basic maintenance
    public float physicalActivity = 1.0f;          // Level of physical activity (affects energy needs)

    private float requiredEnergy;

    void Update()
    {
        requiredEnergy = CalculateRequiredEnergy();
        Debug.Log("Required Energy: " + requiredEnergy);
    }

    float CalculateRequiredEnergy()
    {
        float k16 = 1.0f; // Constant
        return k16 * Mathf.Pow(basicEnergyRequirement, 0.5f) * Mathf.Pow(maintenanceCost, 0.3f) * Mathf.Pow(physicalActivity, 0.2f);
    }
}
