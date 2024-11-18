using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraContent : MonoBehaviour
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
            main.simulationSpeed = 0.9f; // Smooth, gentle particles for contentment
            main.startSize = 0.5f;
            main.startLifetime = 4.5f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 20f; // Moderate flow reflecting a peaceful, relaxed state
        }
    }
}
