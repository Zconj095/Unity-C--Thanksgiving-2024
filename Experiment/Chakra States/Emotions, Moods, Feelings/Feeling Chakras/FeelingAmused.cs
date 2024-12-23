using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelingAmused : MonoBehaviour
{
    public GameObject RootChakra;
    public GameObject SacralChakra;
    public GameObject SolarPlexusChakra;
    public GameObject HeartChakra;
    public GameObject ThroatChakra;
    public GameObject ThirdEyeChakra;
    public GameObject CrownChakra;

    void Start()
    {
        ApplyAmusedFeeling();
    }

    public void ApplyAmusedFeeling()
    {
        ApplyAmusedToChakra(RootChakra);
        ApplyAmusedToChakra(SacralChakra);
        ApplyAmusedToChakra(SolarPlexusChakra);
        ApplyAmusedToChakra(HeartChakra);
        ApplyAmusedToChakra(ThroatChakra);
        ApplyAmusedToChakra(ThirdEyeChakra);
        ApplyAmusedToChakra(CrownChakra);
    }

    private void ApplyAmusedToChakra(GameObject chakra)
    {
        ParticleSystem particleSystem = chakra.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 1.8f; // Quick, lively energy for amusement
            main.startSize = 0.4f;
            main.startLifetime = 2.0f;

            var emission = particleSystem.emission;
            emission.rateOverTime = 40f; // High energy output for playful amusement
        }
    }
}
