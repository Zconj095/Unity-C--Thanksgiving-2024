using System.Collections.Generic;
using UnityEngine;

public class FrequencyPatternRecognizer : MonoBehaviour
{
    // Predefined frequency templates (normalized for simplicity)
    private readonly Dictionary<string, List<float>> templates = new Dictionary<string, List<float>>()
    {
        {"Relaxation", new List<float> {0.2f, 0.5f, 0.7f, 0.1f, 0.1f}}, // Example template
        {"Focus", new List<float> {0.1f, 0.2f, 0.5f, 0.7f, 0.3f}},
        {"Stress", new List<float> {0.4f, 0.3f, 0.1f, 0.8f, 0.5f}}
    };

    // Threshold for correlation
    public float correlationThreshold = 0.8f;

    // Simulated recognized pattern
    private List<float> recognizedPattern = new List<float>();

    void Start()
    {
        // Simulate recognized pattern (e.g., from brainwave processing)
        recognizedPattern = SimulateRecognizedPattern();

        // Check correlation against templates
        CheckPatternCorrelation();
    }

    private List<float> SimulateRecognizedPattern()
    {
        // Simulated pattern, typically from frequency processing logic
        return new List<float> {0.2f, 0.4f, 0.6f, 0.2f, 0.1f};
    }

    private void CheckPatternCorrelation()
    {
        foreach (var template in templates)
        {
            float correlation = PatternCorrelation.ComputeCosineSimilarity(recognizedPattern, template.Value);

            Debug.Log($"Correlation with {template.Key}: {correlation}");

            if (correlation >= correlationThreshold)
            {
                Debug.Log($"Pattern matches {template.Key} with correlation {correlation}!");
                // Trigger action or state change here
            }
        }
    }
}
