using UnityEngine;

public class CognitiveState : MonoBehaviour
{
    public float[] stateVector; // Represents the cognitive state (e.g., focus, attention)
    public int vectorSize = 5;  // Size of the cognitive state vector
    public float decayFactor = 0.95f; // How much previous states decay over time

    void Start()
    {
        InitializeState();
    }

    void InitializeState()
    {
        stateVector = new float[vectorSize];
        for (int i = 0; i < vectorSize; i++)
        {
            stateVector[i] = Random.Range(0.0f, 1.0f); // Random initial states
        }
    }

    public void UpdateState(float[] inputVector)
    {
        // Apply temporal reasoning: decay old state and add influence of input
        for (int i = 0; i < vectorSize; i++)
        {
            stateVector[i] = decayFactor * stateVector[i] + (1 - decayFactor) * inputVector[i];
        }
    }

    public float[] GetState()
    {
        return stateVector;
    }

    void OnDrawGizmos()
    {
        if (stateVector == null) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < vectorSize; i++)
        {
            Gizmos.DrawCube(new Vector3(i, stateVector[i], 0), Vector3.one * 0.1f);
        }
    }
}
