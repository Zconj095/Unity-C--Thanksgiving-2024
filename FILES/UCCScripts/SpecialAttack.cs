using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float energyControl = 0.8f;           // Control over energy for special attacks
    public float rangeFactor = 0.7f;             // Effectiveness over different ranges (long/medium)
    public float mentalFocus = 0.9f;             // Mental clarity for powerful attacks

    private float specialAttackPower;

    void Update()
    {
        specialAttackPower = CalculateSpecialAttack();
        Debug.Log("Special Attack Power: " + specialAttackPower);
    }

    float CalculateSpecialAttack()
    {
        float k3 = 1.0f; // Constant for special attack power
        return k3 * Mathf.Pow(energyControl, 0.5f) * Mathf.Pow(rangeFactor, 0.3f) * Mathf.Pow(mentalFocus, 0.2f);
    }
}
