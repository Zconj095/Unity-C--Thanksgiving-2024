using UnityEngine;
using System.Collections.Generic;

public class TemporalFeedback : MonoBehaviour
{
    private Queue<float[]> stateHistory;
    public int memoryCapacity = 10; // Number of past states to store
    public int vectorSize = 5; // Size of each state vector

    void Start()
    {
        stateHistory = new Queue<float[]>();
    }

    public void StoreState(float[] state)
    {
        if (stateHistory.Count >= memoryCapacity)
        {
            stateHistory.Dequeue();
        }
        stateHistory.Enqueue((float[])state.Clone());
    }

    public float[] ComputeTemporalFeedback()
    {
        float[] feedback = new float[vectorSize];
        foreach (var state in stateHistory)
        {
            for (int i = 0; i < vectorSize; i++)
            {
                feedback[i] += state[i];
            }
        }

        for (int i = 0; i < vectorSize; i++)
        {
            feedback[i] /= stateHistory.Count; // Normalize by the number of stored states
        }
        return feedback;
    }
}
