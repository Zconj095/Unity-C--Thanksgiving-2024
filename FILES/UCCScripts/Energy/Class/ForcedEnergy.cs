using UnityEngine;

public class ForcedEnergy : MonoBehaviour
{
    public float sourcePressure = 0.9f;           // Pressure applied to the energy source
    public float sourceResistance = 0.8f;         // Resistance from the energy source
    public float appliedForce = 1.0f;             // Force applied to the source to extract energy

    private float forcedEnergy;

    void Update()
    {
        forcedEnergy = CalculateForcedEnergy();
        Debug.Log("Forced Energy: " + forcedEnergy);
    }

    float CalculateForcedEnergy()
    {
        float k29 = 1.0f; // Constant
        return k29 * Mathf.Pow(sourcePressure, 0.4f) * Mathf.Pow(sourceResistance, 0.3f) * Mathf.Pow(appliedForce, 0.3f);
    }
}
