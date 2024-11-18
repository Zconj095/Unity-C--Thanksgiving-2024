using UnityEngine;

public class NaturalTransmutations : MonoBehaviour
{
    public float biochemicalActivity = 0.7f;       // Biochemical transmutations
    public float neurochemicalActivity = 0.8f;     // Neurochemical transmutations
    public float geochemicalInfluence = 0.6f;       // Geochemical transmutation influence

    private float transmutationEffect;

    void Update()
    {
        transmutationEffect = CalculateTransmutationEffect();
        Debug.Log("Transmutation Effect: " + transmutationEffect);
    }

    float CalculateTransmutationEffect()
    {
        float k25 = 1.0f; // Constant
        return k25 * Mathf.Pow(biochemicalActivity, 0.4f) * Mathf.Pow(neurochemicalActivity, 0.3f) * Mathf.Pow(geochemicalInfluence, 0.3f);
    }
}
