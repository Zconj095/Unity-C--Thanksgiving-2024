using System.Collections.Generic;
using UnityEngine;

public class SubcorticalFluxManager : MonoBehaviour
{
    private FrequencyFluxAnalyzerV3 fluxAnalyzer;
    private VectorChainManager chainManager;

    void Start()
    {
        fluxAnalyzer = gameObject.AddComponent<FrequencyFluxAnalyzerV3>();
        chainManager = gameObject.AddComponent<VectorChainManager>();
    }

    void Update()
    {
        // Example: Add simulated data
        float simulatedFrequency = Mathf.Sin(Time.time * 2f); // Simulated sub-frequency
        fluxAnalyzer.AddFrequency(simulatedFrequency);

        Vector3 simulatedVector = new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time), 0);
        chainManager.AddVector(simulatedVector);
    }
}
