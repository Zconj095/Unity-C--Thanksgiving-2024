using UnityEngine;
using System.Collections.Generic;

public class EmotionCognitionManagerV2 : MonoBehaviour
{
    // Data structures for emotions
    private Dictionary<string, float> recognizedEmotions; // Emotions with their magnitudes
    private Dictionary<string, float> activatedEmotions; // Activated emotions with magnitudes
    private Dictionary<string, float> emotionCorrelations; // Correlation of emotions with cognition

    // Cognition-related fields
    private float cognitionState = 0.0f; // Current cognition state
    private float cognitionThreshold = 0.5f; // Threshold for significant cognitive shift

    void Start()
    {
        // Initialize emotion dictionaries
        recognizedEmotions = new Dictionary<string, float>();
        activatedEmotions = new Dictionary<string, float>();
        emotionCorrelations = new Dictionary<string, float>();

        // Example initialization (can be replaced by actual emotion data)
        InitializeEmotions();

        Debug.Log("Emotion-Cognition Manager Initialized.");
    }

    // Initialize recognized emotions, correlations, and thresholds
    private void InitializeEmotions()
    {
        recognizedEmotions.Add("Happiness", 0.8f);
        recognizedEmotions.Add("Anger", 0.3f);
        recognizedEmotions.Add("Sadness", 0.5f);
        recognizedEmotions.Add("Fear", 0.4f);

        emotionCorrelations.Add("Happiness", 0.6f); // Positive effect on cognition
        emotionCorrelations.Add("Anger", -0.7f); // Negative effect
        emotionCorrelations.Add("Sadness", -0.5f);
        emotionCorrelations.Add("Fear", -0.6f);
    }

    // Measure and display the magnitude of each recognized emotion
    public void MeasureEmotionMagnitudes()
    {
        Debug.Log("=== Recognized Emotions ===");
        foreach (var emotion in recognizedEmotions)
        {
            Debug.Log($"Emotion: {emotion.Key}, Magnitude: {emotion.Value}");
        }
    }

    // Activate a subset of emotions and compute their effects
    public void ActivateEmotions(List<string> emotionsToActivate)
    {
        activatedEmotions.Clear();

        foreach (string emotion in emotionsToActivate)
        {
            if (recognizedEmotions.ContainsKey(emotion))
            {
                activatedEmotions.Add(emotion, recognizedEmotions[emotion]);
                Debug.Log($"Activated Emotion: {emotion}, Magnitude: {recognizedEmotions[emotion]}");
            }
            else
            {
                Debug.LogWarning($"Emotion {emotion} not recognized.");
            }
        }

        ComputeCognitiveShift();
    }

    // Compute the cognitive shift based on activated emotions
    private void ComputeCognitiveShift()
    {
        float totalShift = 0.0f;

        foreach (var emotion in activatedEmotions)
        {
            if (emotionCorrelations.ContainsKey(emotion.Key))
            {
                float correlation = emotionCorrelations[emotion.Key];
                totalShift += emotion.Value * correlation;
            }
        }

        cognitionState += totalShift;
        cognitionState = Mathf.Clamp(cognitionState, -1.0f, 1.0f); // Keep cognition state within bounds
        Debug.Log($"Cognitive Shift Applied. New Cognition State: {cognitionState}");
    }

    // Display current system state
    public void DisplaySystemState()
    {
        Debug.Log("=== System State ===");
        Debug.Log("Recognized Emotions: " + string.Join(", ", recognizedEmotions));
        Debug.Log("Activated Emotions: " + string.Join(", ", activatedEmotions));
        Debug.Log($"Cognition State: {cognitionState}");
        Debug.Log($"Cognition Threshold: {cognitionThreshold}");
    }

    // Check if cognition state crosses a threshold
    public bool IsCognitionThresholdCrossed()
    {
        bool crossed = Mathf.Abs(cognitionState) > cognitionThreshold;
        Debug.Log($"Cognition Threshold Crossed: {crossed}");
        return crossed;
    }
}
