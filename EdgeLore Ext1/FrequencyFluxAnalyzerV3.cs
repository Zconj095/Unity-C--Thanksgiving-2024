using System.Collections.Generic;
using UnityEngine;

public class FrequencyFluxAnalyzerV3 : MonoBehaviour
{
    public float fluxThreshold = 0.5f; // Define sensitivity
    private List<float> frequencyData = new List<float>();

    public void AddFrequency(float frequency)
    {
        frequencyData.Add(frequency);

        // Limit size to maintain performance
        if (frequencyData.Count > 1000)
            frequencyData.RemoveAt(0);
    }

    public float CalculateFlux()
    {
        if (frequencyData.Count < 2) return 0;

        float flux = Mathf.Abs(frequencyData[frequencyData.Count - 1] - frequencyData[frequencyData.Count - 2]);
        return flux > fluxThreshold ? flux : 0;
    }
}
