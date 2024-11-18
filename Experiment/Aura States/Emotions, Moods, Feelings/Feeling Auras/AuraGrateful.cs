using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraGratefulFeeling : MonoBehaviour
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
        ApplyGratefulAura();
    }

    public void ApplyGratefulAura()
    {
        ApplyGratefulToAura(EthericBody);
        ApplyGratefulToAura(EmotionalBody);
        ApplyGratefulToAura(MentalBody);
        ApplyGratefulToAura(AstralBody);
        ApplyGratefulToAura(EthericTemplate);
        ApplyGratefulToAura(CelestialBody);
        ApplyGratefulToAura(CausalBody);
    }

    private void ApplyGratefulToAura(GameObject auraLayer)
    {
        ParticleSystem particleSystem = auraLayer.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 1.0f; // Warm and continuous flow for gratitude
            main.startSize = 0.4f;
            main.startLifetime = 4.0f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 25f; // Soft and steady particle flow reflecting thankfulness
        }
    }
}
