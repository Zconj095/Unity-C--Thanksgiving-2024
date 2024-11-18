using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraCheerfulFeeling : MonoBehaviour
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
        ApplyCheerfulAura();
    }

    public void ApplyCheerfulAura()
    {
        ApplyCheerfulToAura(EthericBody);
        ApplyCheerfulToAura(EmotionalBody);
        ApplyCheerfulToAura(MentalBody);
        ApplyCheerfulToAura(AstralBody);
        ApplyCheerfulToAura(EthericTemplate);
        ApplyCheerfulToAura(CelestialBody);
        ApplyCheerfulToAura(CausalBody);
    }

    private void ApplyCheerfulToAura(GameObject auraLayer)
    {
        ParticleSystem particleSystem = auraLayer.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 1.6f; // Lively and energetic for cheerfulness
            main.startSize = 0.5f;
            main.startLifetime = 3.0f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 45f; // High energy output to reflect joy and positivity
        }
    }
}
