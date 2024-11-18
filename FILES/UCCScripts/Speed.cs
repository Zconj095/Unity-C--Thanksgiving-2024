using UnityEngine;

public class Speed : MonoBehaviour
{
    public float agility = 0.9f;          // Agility of the character
    public float mobility = 0.8f;         // Mobility and movement flexibility
    public float dexterity = 0.7f;        // Dexterity, affecting speed in action

    private float speed;

    void Update()
    {
        speed = CalculateSpeed();
        Debug.Log("Speed: " + speed);
    }

    float CalculateSpeed()
    {
        float k5 = 1.0f; // Constant for speed
        return k5 * Mathf.Pow(agility, 0.4f) * Mathf.Pow(mobility, 0.3f) * Mathf.Pow(dexterity, 0.3f);
    }
}
