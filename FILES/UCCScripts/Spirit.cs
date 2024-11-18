using UnityEngine;

public class Spirit : MonoBehaviour
{
    public float willpower = 0.9f;          // Character's inner willpower
    public float motivation = 0.85f;        // Motivation to push through challenges
    public float perseverance = 0.8f;       // Perseverance to continue despite setbacks

    private float spiritStrength;

    void Update()
    {
        spiritStrength = CalculateSpiritStrength();
        Debug.Log("Spirit Strength: " + spiritStrength);
    }

    float CalculateSpiritStrength()
    {
        float k12 = 1.0f; // Constant for spirit strength
        return k12 * Mathf.Pow(willpower, 0.4f) * Mathf.Pow(motivation, 0.3f) * Mathf.Pow(perseverance, 0.3f);
    }
}
