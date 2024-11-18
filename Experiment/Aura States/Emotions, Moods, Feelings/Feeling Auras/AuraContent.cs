using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraContentFeeling : MonoBehaviour
{
    public GameObject EthericBody;
    public GameObject EmotionalBody;
    public GameObject MentalBody;
    public GameObject AstralBody;
    public GameObject EthericTemplate;
    public GameObject CelestialBody;
    public GameObject CausalBody;

    void Start()
    {
        ApplyContentAura();
    }

    public void ApplyContentAura()
    {
        ApplyContentToAura(EthericBody);
        ApplyContentToAura(EmotionalBody);
        ApplyContentToAura(MentalBody);
        ApplyContentToAura(AstralBody);
        ApplyContentToAura(EthericTemplate);
        ApplyContentToAura(CelestialBody);
        ApplyContentToAura(CausalBody);
    }

    private void ApplyContentToAura(GameObject auraLayer)
    {
        ParticleSystem particleSystem = auraLayer.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 0.9f; // Slow and smooth flow for contentment
            main.startSize = 0.4f;
            main.startLifetime = 4.5f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 20f; // Gentle and soft flow to reflect a relaxed state
        }
    }
}
