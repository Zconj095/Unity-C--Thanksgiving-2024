using UnityEngine;

public class Attack : MonoBehaviour
{
    public float physicalCondition = 0.7f;   // Physical health and conditioning
    public float mentalFocus = 0.8f;          // Mental state influence on attack
    public float baseAttackStrength = 1.0f;   // Base attack strength of the character

    private float attackStrength;

    void Update()
    {
        attackStrength = CalculateAttackStrength();
        Debug.Log("Attack Strength: " + attackStrength);
    }

    float CalculateAttackStrength()
    {
        float k1 = 1.0f; // Constant for attack strength
        return k1 * Mathf.Pow(physicalCondition, 0.5f) * Mathf.Pow(mentalFocus, 0.3f) * Mathf.Pow(baseAttackStrength, 0.2f);
    }
}
