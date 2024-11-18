using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraFaith : MonoBehaviour
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
        ApplyFaithAura();
    }

    public void ApplyFaithAura()
    {
        ApplyFaithToAura(EthericBody);
        ApplyFaithToAura(EmotionalBody);
        ApplyFaithToAura(MentalBody);
        ApplyFaithToAura(AstralBody);
        ApplyFaithToAura(EthericTemplate);
        ApplyFaithToAura(CelestialBody);
        ApplyFaithToAura(CausalBody);
    }

    private void ApplyFaithToAura(GameObject auraLayer)
    {
        ParticleSystem particleSystem = auraLayer.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 1.3f; // Steady, continuous flow for faith
            main.startSize = 0.5f;
            main.startLifetime = 4.0f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 35f; // Balanced, continuous energy symbolizing faith and belief
        }
    }
}
