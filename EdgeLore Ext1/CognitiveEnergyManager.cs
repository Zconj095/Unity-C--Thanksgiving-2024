using UnityEngine;
using System.Collections.Generic;

public class CognitiveEnergyManager : MonoBehaviour
{
    // Cognitive relay energy levels
    private float inputEnergyLevel = 100.0f; // Energy for inputs (max 100)
    private float outputEnergyLevel = 100.0f; // Energy for outputs (max 100)

    // Emotional readings and drives
    private Dictionary<string, float> emotionalReadings;
    private float motorCognitiveDrive = 0.0f;
    private float emotionalDrive = 0.0f;

    // High-gear states
    private bool isHighGear = false;
    private float highGearMultiplier = 1.5f;

    // Energy drain rates
    private float inputEnergyDrainRate = 5.0f;
    private float outputEnergyDrainRate = 4.0f;

    void Start()
    {
        // Initialize emotional readings
        emotionalReadings = new Dictionary<string, float>
        {
            { "Happiness", 0.8f },
            { "Anger", 0.4f },
            { "Sadness", 0.5f },
            { "Fear", 0.3f }
        };

        Debug.Log("Cognitive Energy Manager Initialized.");
    }

    void Update()
    {
        if (isHighGear)
        {
            SimulateHighGear();
        }

        // Monitor energy levels
        MonitorEnergyLevels();
    }

    // Activate high gear, increasing energy drain and drives
    public void ActivateHighGear()
    {
        isHighGear = true;
        Debug.Log("High Gear Activated: Energy drain and drives amplified.");
    }

    // Deactivate high gear
    public void DeactivateHighGear()
    {
        isHighGear = false;
        Debug.Log("High Gear Deactivated: Normal operations resumed.");
    }

    // Simulate high gear effects
    private void SimulateHighGear()
    {
        motorCognitiveDrive *= highGearMultiplier;
        emotionalDrive *= highGearMultiplier;
        DrainEnergy(inputEnergyDrainRate * highGearMultiplier, outputEnergyDrainRate * highGearMultiplier);
    }

    // Measure and update motor cognitive and emotional drives
    public void UpdateDrives()
    {
        motorCognitiveDrive = CalculateMotorCognitiveDrive();
        emotionalDrive = CalculateEmotionalDrive();

        Debug.Log($"Updated Drives: Motor Cognitive Drive = {motorCognitiveDrive}, Emotional Drive = {emotionalDrive}");
    }

    // Calculate motor cognitive drive based on input energy and emotional intensity
    private float CalculateMotorCognitiveDrive()
    {
        float intensitySum = 0.0f;
        foreach (var emotion in emotionalReadings.Values)
        {
            intensitySum += emotion;
        }
        return inputEnergyLevel * (intensitySum / emotionalReadings.Count);
    }

    // Calculate emotional drive based on output energy and emotional state correlations
    private float CalculateEmotionalDrive()
    {
        float correlationSum = 0.0f;
        foreach (var emotion in emotionalReadings)
        {
            // Example correlation: higher happiness, lower sadness affect drive
            float correlation = (emotion.Key == "Happiness") ? 1.2f : (emotion.Key == "Sadness" ? -0.8f : 0.5f);
            correlationSum += emotion.Value * correlation;
        }
        return outputEnergyLevel * Mathf.Clamp(correlationSum, 0.0f, 1.0f);
    }

    // Drain energy from inputs and outputs
    private void DrainEnergy(float inputDrain, float outputDrain)
    {
        inputEnergyLevel -= inputDrain * Time.deltaTime;
        outputEnergyLevel -= outputDrain * Time.deltaTime;

        inputEnergyLevel = Mathf.Clamp(inputEnergyLevel, 0.0f, 100.0f);
        outputEnergyLevel = Mathf.Clamp(outputEnergyLevel, 0.0f, 100.0f);

        Debug.Log($"Energy Levels: Input = {inputEnergyLevel}, Output = {outputEnergyLevel}");
    }

    // Monitor energy levels and log warnings if critical
    private void MonitorEnergyLevels()
    {
        if (inputEnergyLevel <= 10.0f)
        {
            Debug.LogWarning("Input Energy critically low!");
        }
        if (outputEnergyLevel <= 10.0f)
        {
            Debug.LogWarning("Output Energy critically low!");
        }
    }

    // Display system state
    public void DisplaySystemState()
    {
        Debug.Log("=== System State ===");
        Debug.Log($"Input Energy Level: {inputEnergyLevel}");
        Debug.Log($"Output Energy Level: {outputEnergyLevel}");
        Debug.Log($"Motor Cognitive Drive: {motorCognitiveDrive}");
        Debug.Log($"Emotional Drive: {emotionalDrive}");
        Debug.Log($"High Gear State: {isHighGear}");
    }
}
