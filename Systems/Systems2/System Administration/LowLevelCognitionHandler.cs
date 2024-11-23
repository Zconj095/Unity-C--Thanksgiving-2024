using System;
using System.Collections.Generic;
using UnityEngine;

public class LowLevelCognitionHandler : MonoBehaviour
{
    // Sensory Input Module
    [Serializable]
    public class SensoryInput
    {
        public string source;       // e.g., "Player", "Enemy"
        public string inputType;    // e.g., "Sound", "Movement", "Interaction"
        public float intensity;     // Strength of the input (e.g., volume, force)
    }

    // Internal State Manager
    private Dictionary<string, object> internalStates = new Dictionary<string, object>();

    // Event for external listeners
    public event Action<string, object> OnCognitionOutput;

    // Start method for initialization
    void Start()
    {
        InitializeStates();
    }

    private void InitializeStates()
    {
        internalStates["AlertLevel"] = 0f; // Ranges from 0 to 1
        internalStates["LastStimulus"] = null;
    }

    // Main method for processing sensory input
    public void ProcessSensoryInput(SensoryInput input)
    {
        Debug.Log($"Processing input: Source={input.source}, Type={input.inputType}, Intensity={input.intensity}");

        // Analyze input
        AnalyzeInput(input);

        // Generate response based on cognitive analysis
        GenerateResponse(input);
    }

    private void AnalyzeInput(SensoryInput input)
    {
        // Example: Adjust alert level based on input intensity
        float currentAlertLevel = (float)internalStates["AlertLevel"];
        float newAlertLevel = Mathf.Clamp(currentAlertLevel + input.intensity * 0.1f, 0f, 1f);

        internalStates["AlertLevel"] = newAlertLevel;
        internalStates["LastStimulus"] = input;

        Debug.Log($"Updated Alert Level: {newAlertLevel}");
    }

    private void GenerateResponse(SensoryInput input)
    {
        // Basic responses based on input type and intensity
        switch (input.inputType.ToLower())
        {
            case "sound":
                if (input.intensity > 0.5f)
                {
                    Debug.Log("High-intensity sound detected. Triggering response.");
                    OnCognitionOutput?.Invoke("PlaySound", "AlertSound");
                }
                break;

            case "movement":
                Debug.Log("Movement detected. Adjusting behavior.");
                OnCognitionOutput?.Invoke("ChangeBehavior", "Investigate");
                break;

            case "interaction":
                Debug.Log("Interaction detected. Engaging.");
                OnCognitionOutput?.Invoke("Engage", input.source);
                break;

            default:
                Debug.Log("Unknown input type. No action taken.");
                break;
        }
    }

    // Update method for periodic state monitoring
    void Update()
    {
        float alertLevel = (float)internalStates["AlertLevel"];

        // Periodically decay the alert level over time
        if (alertLevel > 0)
        {
            alertLevel = Mathf.Max(0f, alertLevel - Time.deltaTime * 0.05f);
            internalStates["AlertLevel"] = alertLevel;
        }
    }

    void SimulateInput(LowLevelCognitionHandler cognitionHandler)
    {
        var input = new LowLevelCognitionHandler.SensoryInput
        {
            source = "Player",
            inputType = "Sound",
            intensity = 0.7f
        };

        cognitionHandler.ProcessSensoryInput(input);
    }

    void HandleCognitionOutput(string action, object data)
    {
        switch (action)
        {
            case "PlaySound":
                Debug.Log($"Playing sound: {data}");
                break;
            case "ChangeBehavior":
                Debug.Log($"Changing behavior to: {data}");
                break;
            case "Engage":
                Debug.Log($"Engaging with: {data}");
                break;
        }
    }


}
