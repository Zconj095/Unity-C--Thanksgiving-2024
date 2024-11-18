using UnityEngine;

public class SpecialDefense : MonoBehaviour
{
    public float defensiveEnergy = 0.75f;         // Energy used to defend against special attacks
    public float auraProtection = 0.8f;           // Aura protection against special energies
    public float mentalFocus = 0.7f;              // Mental strength for focusing defense

    private float specialDefenseStrength;

    void Update()
    {
        specialDefenseStrength = CalculateSpecialDefense();
        Debug.Log("Special Defense Strength: " + specialDefenseStrength);
    }

    float CalculateSpecialDefense()
    {
        float k4 = 1.0f; // Constant for special defense strength
        return k4 * Mathf.Pow(defensiveEnergy, 0.4f) * Mathf.Pow(auraProtection, 0.3f) * Mathf.Pow(mentalFocus, 0.3f);
    }
}
