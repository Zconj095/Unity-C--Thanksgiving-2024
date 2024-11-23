using System.Collections.Generic;
using UnityEngine;
public enum CognitiveStatev2
{
    Idle,
    Alert,
    Processing,
    DecisionMaking,
    Action,
    Recovery
}

public class CognitiveStatev2Manager : MonoBehaviour
{
    public CognitiveStatev2 currentState = CognitiveStatev2.Idle;

    // Vector-based data inputs (e.g., sensory data or spatial coordinates)
    public List<Vector3> vectorLocations = new List<Vector3>();
    
    // Reaction time multiplier
    public float reactionTime = 1.0f;

    void Update()
    {
        // Simulate input data and transitions
        SimulateInput();
        UpdateState();
        HandleReactions();
    }

    private void SimulateInput()
    {
        // Example: Simulate random vector inputs
        vectorLocations.Add(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
        
        // Limit size of vector data for memory optimization
        if (vectorLocations.Count > 100)
            vectorLocations.RemoveAt(0);
    }

    private void UpdateState()
    {
        // Example logic for state transitions
        switch (currentState)
        {
            case CognitiveStatev2.Idle:
                if (vectorLocations.Count > 10)
                    currentState = CognitiveStatev2.Alert;
                break;

            case CognitiveStatev2.Alert:
                if (vectorLocations.Count > 20)
                    currentState = CognitiveStatev2.Processing;
                break;

            case CognitiveStatev2.Processing:
                if (vectorLocations.Count > 50)
                    currentState = CognitiveStatev2.DecisionMaking;
                break;

            case CognitiveStatev2.DecisionMaking:
                currentState = CognitiveStatev2.Action; // Example automatic transition
                break;

            case CognitiveStatev2.Action:
                currentState = CognitiveStatev2.Recovery;
                break;

            case CognitiveStatev2.Recovery:
                if (vectorLocations.Count < 10)
                    currentState = CognitiveStatev2.Idle;
                break;
        }
    }

    private void HandleReactions()
    {
        // Define reactions based on state
        switch (currentState)
        {
            case CognitiveStatev2.Alert:
                Debug.Log("In Alert state: Gathering data...");
                break;

            case CognitiveStatev2.Processing:
                Debug.Log("In Processing state: Analyzing vectors...");
                AnalyzeVectors();
                break;

            case CognitiveStatev2.DecisionMaking:
                Debug.Log("In Decision-Making state: Making decisions...");
                MakeDecision();
                break;

            case CognitiveStatev2.Action:
                Debug.Log("In Action state: Executing action...");
                ExecuteAction();
                break;

            case CognitiveStatev2.Recovery:
                Debug.Log("In Recovery state: Resetting system...");
                ResetSystem();
                break;

            default:
                Debug.Log("In Idle state: Monitoring...");
                break;
        }
    }

    private void AnalyzeVectors()
    {
        // Analyze vector data (e.g., calculate mean position)
        if (vectorLocations.Count == 0) return;

        Vector3 sum = Vector3.zero;
        foreach (Vector3 vec in vectorLocations)
        {
            sum += vec;
        }
        Vector3 mean = sum / vectorLocations.Count;
        Debug.Log($"Mean vector location: {mean}");
    }

    private void MakeDecision()
    {
        // Make decisions based on vector data (example: choose dominant direction)
        if (vectorLocations.Count == 0) return;

        Vector3 decisionVector = vectorLocations[Random.Range(0, vectorLocations.Count)];
        Debug.Log($"Decision vector chosen: {decisionVector}");
    }

    private void ExecuteAction()
    {
        // Example: Translate GameObject to a vector location
        if (vectorLocations.Count == 0) return;

        Vector3 target = vectorLocations[0];
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * reactionTime);
    }

    private void ResetSystem()
    {
        // Reset states and clear data
        vectorLocations.Clear();
    }

    void OnDrawGizmos()
    {
        // Visualize vector locations
        Gizmos.color = Color.green;
        foreach (var vec in vectorLocations)
        {
            Gizmos.DrawSphere(vec, 0.5f);
        }

        // Display current state
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        UnityEditor.Handles.Label(transform.position, $"State: {currentState}");
    }
}
