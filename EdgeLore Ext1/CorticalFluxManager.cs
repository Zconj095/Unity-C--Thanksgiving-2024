using System.Collections.Generic;
using UnityEngine;

public class CorticalFluxManager : MonoBehaviour
{
    // Parameters for managing flux and thresholds
    public float baseThreshold = 1.0f;  // Initial threshold
    public float thresholdAdaptRate = 0.1f; // Rate of adjustment
    public int windowSize = 50; // Number of data points for mean calculation

    private List<float> fluxValues = new List<float>();
    private float dynamicThreshold;

    void Start()
    {
        dynamicThreshold = baseThreshold;
    }

    void Update()
    {
        // Simulate flux input (replace with real cortical data)
        float simulatedFlux = Mathf.Sin(Time.time) + Random.Range(-0.1f, 0.1f);
        AddFluxValue(simulatedFlux);

        float corticalMean = CalculateCorticalMean();
        AdjustThreshold(corticalMean);

        if (corticalMean > dynamicThreshold)
        {
            Debug.Log("Threshold exceeded: Taking action.");
            HandleThresholdExceed();
        }
    }

    public void AddFluxValue(float flux)
    {
        fluxValues.Add(flux);

        // Limit size of stored values to window size
        if (fluxValues.Count > windowSize)
            fluxValues.RemoveAt(0);
    }

    private float CalculateCorticalMean()
    {
        if (fluxValues.Count == 0) return 0f;

        float sum = 0f;
        foreach (var flux in fluxValues)
        {
            sum += flux;
        }

        return sum / fluxValues.Count;
    }

    private void AdjustThreshold(float corticalMean)
    {
        // Dynamically adjust threshold based on cortical mean
        if (corticalMean > dynamicThreshold)
        {
            dynamicThreshold += thresholdAdaptRate * Time.deltaTime;
        }
        else
        {
            dynamicThreshold -= thresholdAdaptRate * Time.deltaTime;
        }

        // Clamp the threshold to prevent extreme values
        dynamicThreshold = Mathf.Clamp(dynamicThreshold, baseThreshold * 0.5f, baseThreshold * 2f);

        Debug.Log($"Dynamic Threshold: {dynamicThreshold}");
    }

    private void HandleThresholdExceed()
    {
        // Example reaction to threshold being exceeded
        Debug.Log("Action triggered by exceeding dynamic threshold.");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Visualize flux values as points
        for (int i = 0; i < fluxValues.Count; i++)
        {
            Gizmos.DrawSphere(new Vector3(i * 0.1f, fluxValues[i], 0), 0.05f);
        }

        // Draw dynamic threshold line
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(0, dynamicThreshold, 0), new Vector3(fluxValues.Count * 0.1f, dynamicThreshold, 0));
    }

}
