using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraNeutralFeeling : MonoBehaviour
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
        ApplyNeutralAura();
    }

    public void ApplyNeutralAura()
    {
        ApplyNeutralToAura(EthericBody);
        ApplyNeutralToAura(EmotionalBody);
        ApplyNeutralToAura(MentalBody);
        ApplyNeutralToAura(AstralBody);
        ApplyNeutralToAura(EthericTemplate);
        ApplyNeutralToAura(CelestialBody);
        ApplyNeutralToAura(CausalBody);
    }

    private void ApplyNeutralToAura(GameObject auraLayer)
    {
        ParticleSystem particleSystem = auraLayer.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 1.0f; // Balanced and steady flow to reflect neutrality
            main.startSize = 0.4f;
            main.startLifetime = 4.0f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 20f; // Calm and even particle flow symbolizing emotional balance
        }
    }
}
