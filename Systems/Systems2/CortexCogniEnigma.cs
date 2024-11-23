using System.Collections.Generic;
using UnityEngine;

public class CortexCogniEnigma : MonoBehaviour
{
    // Represents the cortex node's neural pathways (connections).
    private Dictionary<string, float> neuralPathways;

    // Represents the cortex node's memory and adaptive state.
    private List<string> memory;
    private float adaptationThreshold = 0.5f;

    // Environmental stimuli detection.
    public Transform environmentSource;

    // Visualization parameters.
    public Color activeColor = Color.cyan;
    public Color dormantColor = Color.gray;
    private Renderer objectRenderer;

    void Start()
    {
        // Initialize neural pathways and memory.
        neuralPathways = new Dictionary<string, float>();
        memory = new List<string>();
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = dormantColor;

        // Start the learning cycle.
        InvokeRepeating(nameof(AnalyzeEnvironment), 1f, 1f);
    }

    void AnalyzeEnvironment()
    {
        if (environmentSource == null) return;

        // Simulate sensing the environment and extracting stimuli.
        string stimuli = DetectStimuli();

        if (string.IsNullOrEmpty(stimuli))
        {
            // Dormant state, decrease activation.
            objectRenderer.material.color = Color.Lerp(objectRenderer.material.color, dormantColor, 0.1f);
            return;
        }

        // Log and process stimuli.
        Debug.Log($"Stimuli detected: {stimuli}");
        ProcessStimuli(stimuli);
    }

    string DetectStimuli()
    {
        // Example: Detect nearby objects and create a "stimuli signature."
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        if (hitColliders.Length == 0) return null;

        string stimuliSignature = "";
        foreach (var hit in hitColliders)
        {
            stimuliSignature += hit.gameObject.name + "-";
        }

        return stimuliSignature.TrimEnd('-');
    }

    void ProcessStimuli(string stimuli)
    {
        // Encode stimuli into the neural pathways and adapt.
        if (!neuralPathways.ContainsKey(stimuli))
        {
            neuralPathways[stimuli] = Random.Range(0.1f, 1f); // Initialize a pathway strength.
            memory.Add(stimuli);
        }
        else
        {
            neuralPathways[stimuli] += 0.1f; // Reinforce existing pathway.
        }

        Adapt(stimuli);
        UpdateAppearance(neuralPathways[stimuli]);
    }

    void Adapt(string stimuli)
    {
        // If the pathway strength exceeds the threshold, adapt and evolve.
        if (neuralPathways[stimuli] >= adaptationThreshold)
        {
            Debug.Log($"Enigma solved: Adapted to {stimuli}");
            neuralPathways[stimuli] = 0.1f; // Reset strength to simulate evolution.
        }
    }

    void UpdateAppearance(float intensity)
    {
        // Update the visual representation based on the activation intensity.
        objectRenderer.material.color = Color.Lerp(dormantColor, activeColor, intensity);
    }

    void OnDrawGizmos()
    {
        // Visualize environmental detection range.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
