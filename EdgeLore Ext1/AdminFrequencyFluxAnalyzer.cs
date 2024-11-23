using System.Collections.Generic;
using UnityEngine;

public class AdminFrequencyFluxAnalyzer : MonoBehaviour
{
    // Data points for analysis
    private List<float> dataPoints = new List<float>();
    
    // Frequency thresholds (set based on your requirements)
    public float upperThreshold = 1.0f;
    public float lowerThreshold = 0.1f;
    
    // Analysis period
    public float analysisPeriod = 1.0f; // Analyze data every 1 second
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Simulate data input (this should be replaced with actual input source)
        float simulatedInput = Mathf.Sin(Time.time);
        AddData(simulatedInput);

        if (timer >= analysisPeriod)
        {
            AnalyzeData();
            timer = 0f; // Reset timer
        }
    }

    // Add new data to the analysis queue
    public void AddData(float value)
    {
        dataPoints.Add(value);

        // Optional: Limit size to prevent memory overload
        if (dataPoints.Count > 1000)
            dataPoints.RemoveAt(0);
    }

    // Analyze data for flux patterns
    private void AnalyzeData()
    {
        if (dataPoints.Count == 0) return;

        // Calculate metrics (e.g., mean, variance)
        float mean = 0f;
        foreach (float point in dataPoints)
        {
            mean += point;
        }
        mean /= dataPoints.Count;

        float variance = 0f;
        foreach (float point in dataPoints)
        {
            variance += Mathf.Pow(point - mean, 2);
        }
        variance /= dataPoints.Count;

        // Compare variance with thresholds
        if (variance > upperThreshold)
        {
            Debug.Log("High flux detected!");
        }
        else if (variance < lowerThreshold)
        {
            Debug.Log("Low flux detected!");
        }
        else
        {
            Debug.Log("Flux within normal range.");
        }

        // Clear data after analysis (optional)
        dataPoints.Clear();
    }
}
