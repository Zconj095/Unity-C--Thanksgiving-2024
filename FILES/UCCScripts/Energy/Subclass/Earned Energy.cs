using UnityEngine;

public class EarnedEnergy : MonoBehaviour
{
    public float effortAmount = 0.8f;              // Amount of effort exerted
    public float successFactor = 0.7f;             // Success rate based on effort
    public float energyYield = 0.9f;               // Yield from energy earned

    private float earnedEnergy;

    void Update()
    {
        earnedEnergy = CalculateEarnedEnergy();
        Debug.Log("Earned Energy: " + earnedEnergy);
    }

    float CalculateEarnedEnergy()
    {
        float k32 = 1.0f; // Constant
        return k32 * Mathf.Pow(effortAmount, 0.4f) * Mathf.Pow(successFactor, 0.3f) * Mathf.Pow(energyYield, 0.3f);
    }
}
