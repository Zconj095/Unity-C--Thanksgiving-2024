using UnityEngine;
using System.Collections.Generic;

public class TemporalMemory : MonoBehaviour
{
    public int memoryCapacity = 10; // Maximum number of past states to store
    private Queue<float[]> memoryQueue; // Stores the past states

    public int vectorSize = 5; // Size of the state vector

    void Start()
    {
        memoryQueue = new Queue<float[]>();
    }

    public void StoreState(float[] state)
    {
        if (memoryQueue.Count >= memoryCapacity)
        {
            memoryQueue.Dequeue(); // Remove the oldest state
        }
        memoryQueue.Enqueue((float[])state.Clone()); // Store a copy of the state
    }

    public float[] RetrieveStateClosestTo(float[] queryState)
    {
        // Retrieve the state most similar to the queryState
        float[] closestState = null;
        float smallestDistance = float.MaxValue;

        foreach (var state in memoryQueue)
        {
            float distance = ComputeDistance(state, queryState);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestState = state;
            }
        }

        return closestState;
    }

    private float ComputeDistance(float[] state1, float[] state2)
    {
        // Compute Euclidean distance between two states
        float sum = 0.0f;
        for (int i = 0; i < vectorSize; i++)
        {
            sum += Mathf.Pow(state1[i] - state2[i], 2);
        }
        return Mathf.Sqrt(sum);
    }

    public List<float[]> GetAllStates()
    {
        return new List<float[]>(memoryQueue);
    }
}
