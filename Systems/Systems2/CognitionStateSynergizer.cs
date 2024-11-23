using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CognitionStateSynergizer : MonoBehaviour
{
    public List<CoreCognitiveState> CognitiveStates;
    public Color SynergizedColor;
    public float SynergizedIntensity;

    void Start()
    {
        // Initialize example states
        CognitiveStates = new List<CoreCognitiveState>
        {
            new CoreCognitiveState("Focus", 0.8f, Color.blue),
            new CoreCognitiveState("Memory", 0.6f, Color.green),
            new CoreCognitiveState("Emotion", 0.4f, Color.red)
        };

        SynergizeStates();
    }

    void Update()
    {
        // Trigger synergy recalculation on a key press (e.g., "S")
        if (Input.GetKeyDown(KeyCode.S))
        {
            SynergizeStates();
        }
    }

    public void SynergizeStates()
    {
        float totalIntensity = 0;
        Color blendedColor = Color.black;

        foreach (var state in CognitiveStates)
        {
            // Use Reflection to dynamically get state properties
            Type stateType = state.GetType();
            PropertyInfo intensityProp = stateType.GetProperty("Intensity");
            PropertyInfo colorProp = stateType.GetProperty("StateColor");

            if (intensityProp != null && colorProp != null)
            {
                float intensity = (float)intensityProp.GetValue(state);
                Color color = (Color)colorProp.GetValue(state);

                totalIntensity += intensity;
                blendedColor += color * intensity;
            }
        }

        SynergizedIntensity = Mathf.Clamp01(totalIntensity / CognitiveStates.Count);
        SynergizedColor = blendedColor / CognitiveStates.Count;

        Debug.Log($"Synergized Intensity: {SynergizedIntensity}");
        Debug.Log($"Synergized Color: {SynergizedColor}");

        UpdateVisualization();
    }

    private void UpdateVisualization()
    {
        // Create a sphere to represent the synergized state
        GameObject synergizedSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        synergizedSphere.transform.position = new Vector3(0, 1, 0);
        synergizedSphere.transform.localScale = Vector3.one * SynergizedIntensity * 2.0f;

        Renderer renderer = synergizedSphere.GetComponent<Renderer>();
        renderer.material.color = SynergizedColor;

        // Destroy the sphere after 1 second to avoid clutter
        Destroy(synergizedSphere, 1f);
    }
}
