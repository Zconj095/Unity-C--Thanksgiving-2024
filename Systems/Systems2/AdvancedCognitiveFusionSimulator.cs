using System.Collections.Generic;
using UnityEngine;

public class AdvancedCognitiveFusionSimulator : MonoBehaviour
{
    [System.Serializable]
    public class CognitiveState
    {
        public string Name;
        public float Intensity; // Between 0 and 1
        public Vector3 EmotionVector; // For n-dimensional representation
        public Color StateColor; // For visualization
    }

    [System.Serializable]
    public class FusedState
    {
        public Vector3 FusedVector;
        public float OverallIntensity;
        public Color BlendedColor;
    }

    public List<CognitiveState> CognitiveStates;
    public FusedState FusedCognitiveState;

    private MeshRenderer visualRepresentation;

    void Start()
    {
        // Create a 3D representation
        GameObject representation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        visualRepresentation = representation.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        SimulateFusion();
        UpdateVisualRepresentation();
    }

    private void SimulateFusion()
    {
        Vector3 accumulatedVector = Vector3.zero;
        float totalIntensity = 0f;
        Color blendedColor = Color.black;

        foreach (var state in CognitiveStates)
        {
            accumulatedVector += state.EmotionVector * state.Intensity;
            totalIntensity += state.Intensity;
            blendedColor += state.StateColor * state.Intensity;
        }

        // Normalize the results
        if (totalIntensity > 0)
        {
            accumulatedVector /= totalIntensity;
            blendedColor /= totalIntensity;
        }

        FusedCognitiveState = new FusedState
        {
            FusedVector = accumulatedVector,
            OverallIntensity = Mathf.Clamp(totalIntensity / CognitiveStates.Count, 0, 1),
            BlendedColor = blendedColor
        };
    }

    private void UpdateVisualRepresentation()
    {
        if (visualRepresentation != null)
        {
            visualRepresentation.material.color = FusedCognitiveState.BlendedColor;
            visualRepresentation.transform.localScale = Vector3.one * FusedCognitiveState.OverallIntensity * 2.0f;
            visualRepresentation.transform.position = FusedCognitiveState.FusedVector;
        }
    }
}
