using UnityEngine;

public class ChiBalance : MonoBehaviour
{
    public float lightEnergy = 0.5f;          // Light energy component
    public float darkEnergy = 0.5f;           // Dark energy component
    public float chiBalanceFactor = 0.8f;     // Balance factor for Chi

    private float chiBalance;

    void Update()
    {
        chiBalance = CalculateChiBalance();
        Debug.Log("Chi Balance: " + chiBalance);
    }

    float CalculateChiBalance()
    {
        float k21 = 1.0f; // Constant
        return k21 * Mathf.Pow(lightEnergy, 0.5f) * Mathf.Pow(darkEnergy, 0.5f) * Mathf.Pow(chiBalanceFactor, 0.5f);
    }
}
