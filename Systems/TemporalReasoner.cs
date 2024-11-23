using UnityEngine;

public class TemporalReasoner : MonoBehaviour
{
    public CognitiveState cognitiveState;
    public float[] externalStimuli; // Environmental stimuli affecting state
    public int vectorSize = 5;

    private float[,] temporalMemory; // Stores historical states
    private int memoryCapacity = 10; // Number of past states to remember
    private int currentMemoryIndex = 0;

    void Start()
    {
        InitializeTemporalMemory();
        GenerateStimuli();
    }

    void InitializeTemporalMemory()
    {
        temporalMemory = new float[memoryCapacity, vectorSize];
    }

    void GenerateStimuli()
    {
        externalStimuli = new float[vectorSize];
        for (int i = 0; i < vectorSize; i++)
        {
            externalStimuli[i] = Random.Range(0.0f, 1.0f); // Random external influences
        }
    }

    void Update()
    {
        PerformTemporalReasoning();
    }

    void PerformTemporalReasoning()
    {
        // Store current state in memory
        float[] currentState = cognitiveState.GetState();
        for (int i = 0; i < vectorSize; i++)
        {
            temporalMemory[currentMemoryIndex, i] = currentState[i];
        }
        currentMemoryIndex = (currentMemoryIndex + 1) % memoryCapacity;

        // Use past memory to adjust external stimuli
        float[] adjustedStimuli = new float[vectorSize];
        for (int i = 0; i < vectorSize; i++)
        {
            float historicalInfluence = 0.0f;
            for (int t = 0; t < memoryCapacity; t++)
            {
                historicalInfluence += temporalMemory[t, i];
            }
            historicalInfluence /= memoryCapacity; // Average historical influence
            adjustedStimuli[i] = externalStimuli[i] + historicalInfluence * 0.5f;
        }

        // Update cognitive state with adjusted stimuli
        cognitiveState.UpdateState(adjustedStimuli);
    }

    void OnDrawGizmos()
    {
        if (temporalMemory == null) return;

        Gizmos.color = Color.red;
        for (int t = 0; t < memoryCapacity; t++)
        {
            for (int i = 0; i < vectorSize; i++)
            {
                Gizmos.DrawCube(new Vector3(i, temporalMemory[t, i] - t * 0.1f, -t * 0.1f), Vector3.one * 0.05f);
            }
        }
    }
}
