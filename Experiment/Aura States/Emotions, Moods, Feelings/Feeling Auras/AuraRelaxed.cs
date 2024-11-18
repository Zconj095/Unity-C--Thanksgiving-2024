using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraRelaxedFeeling : MonoBehaviour
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
        ApplyRelaxedAura();
    }

    public void ApplyRelaxedAura()
    {
        ApplyRelaxedToAura(EthericBody);
        ApplyRelaxedToAura(EmotionalBody);
        ApplyRelaxedToAura(MentalBody);
        ApplyRelaxedToAura(AstralBody);
        ApplyRelaxedToAura(EthericTemplate);
        ApplyRelaxedToAura(CelestialBody);
        ApplyRelaxedToAura(CausalBody);
    }

    private void ApplyRelaxedToAura(GameObject auraLayer)
    {
        ParticleSystem particleSystem = auraLayer.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 1.0f; // Smooth and slow-moving flow for relaxation
            main.startSize = 0.4f;
            main.startLifetime = 4.5f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 20f; // Gentle and fluid particle flow reflecting contentment and calmness
        }
    }
}
