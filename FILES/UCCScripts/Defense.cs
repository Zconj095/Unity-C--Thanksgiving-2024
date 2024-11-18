using UnityEngine;

public class Defense : MonoBehaviour
{
    public float defenseSkill = 0.6f;            // Skill and ability to defend
    public float physicalCondition = 0.8f;      // Physical condition of the character
    public float mentalReadiness = 0.7f;        // Mental readiness for defense

    private float defenseStrength;

    void Update()
    {
        defenseStrength = CalculateDefenseStrength();
        Debug.Log("Defense Strength: " + defenseStrength);
    }

    float CalculateDefenseStrength()
    {
        float k2 = 1.0f; // Constant for defense strength
        return k2 * Mathf.Pow(defenseSkill, 0.4f) * Mathf.Pow(physicalCondition, 0.3f) * Mathf.Pow(mentalReadiness, 0.3f);
    }
}
