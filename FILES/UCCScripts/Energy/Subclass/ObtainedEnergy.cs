using UnityEngine;

public class ObtainedEnergy : MonoBehaviour
{
    public float sourcePower = 1.0f;            // Power of the energy source
    public float extractionEfficiency = 0.7f;   // Efficiency of energy extraction
    public float accessibility = 0.9f;          // Accessibility of the energy source

    private float obtainedEnergy;

    void Update()
    {
        obtainedEnergy = CalculateObtainedEnergy();
        Debug.Log("Obtained Energy: " + obtainedEnergy);
    }

    float CalculateObtainedEnergy()
    {
        float k13 = 1.0f; // Constant
        return k13 * Mathf.Pow(sourcePower, 0.5f) * Mathf.Pow(extractionEfficiency, 0.3f) * Mathf.Pow(accessibility, 0.2f);
    }
}
