using System.Collections.Generic;
using UnityEngine;

public class CognitiveFusionSimulator : MonoBehaviour
{
    // Define a structure for Cognitive States
    [System.Serializable]
    public class CognitiveState
    {
        public string Name;
        public float Intensity; // Value between 0.0 and 1.0
        public Color StateColor; // Visual representation
    }

    [System.Serializable]
    public class FusedState
    {
        public string FusedName;
        public float CombinedIntensity;
        public Color BlendedColor;
    }

    // List of base cognitive states
    public List<CognitiveState> CognitiveStates;

    // Resultant fused state
    public FusedState FusedCognitiveState;

    private MeshRenderer visualRepresentation;

    void Start()
    {
        // Initialize a visual representation in Unity scene
        GameObject representation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        visualRepresentation = representation.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Continuously update fusion based on simulated inputs
        SimulateFusion();
        UpdateVisualRepresentation();
    }

    private void SimulateFusion()
    {
        float totalIntensity = 0f;
        Color blendedColor = Color.black;

        // Calculate fused intensity and color blending
        foreach (var state in CognitiveStates)
        {
            totalIntensity += state.Intensity;
            blendedColor += state.StateColor * state.Intensity;
        }

        if (CognitiveStates.Count > 0)
        {
            blendedColor /= CognitiveStates.Count; // Normalize color
        }

        FusedCognitiveState = new FusedState
        {
            FusedName = "Fused Emotion State",
            CombinedIntensity = Mathf.Clamp(totalIntensity / CognitiveStates.Count, 0, 1),
            BlendedColor = blendedColor
        };
    }

    private void UpdateVisualRepresentation()
    {
        // Update the Unity visual representation (e.g., sphere color and scale)
        if (visualRepresentation != null)
        {
            visualRepresentation.material.color = FusedCognitiveState.BlendedColor;
            visualRepresentation.transform.localScale = Vector3.one * FusedCognitiveState.CombinedIntensity * 2.0f;
        }
    }
}
