using UnityEngine;
using System.Collections.Generic;

public class MemoryRecallSystem : MonoBehaviour
{
    private MEMORYRECALLLSTM lstm;
    private List<float[]> memory;

    void Start()
    {
        int inputSize = 4;  // Example input size
        int hiddenSize = 8; // Example hidden size
        lstm = new MEMORYRECALLLSTM(inputSize, hiddenSize);
        memory = new List<float[]>();

        // Encode some example sequences
        EncodeSequence(new float[] { 1, 0, 0, 1 });
        EncodeSequence(new float[] { 0, 1, 1, 0 });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            float[] query = new float[] { 1, 0, 0, 1 }; // Example query
            float[] recalled = RecallMemory(query);
            Debug.Log($"Recalled Memory: {string.Join(", ", recalled)}");
        }
    }

    public void EncodeSequence(float[] sequence)
    {
        memory.Add(lstm.Forward(sequence));
    }

    public float[] RecallMemory(float[] query)
    {
        float[] queryState = lstm.Forward(query);
        float[] closestMemory = null;
        float closestDistance = float.MaxValue;

        foreach (var mem in memory)
        {
            float distance = ComputeDistance(queryState, mem);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMemory = mem;
            }
        }

        return closestMemory;
    }

    private float ComputeDistance(float[] a, float[] b)
    {
        float distance = 0f;
        for (int i = 0; i < a.Length; i++)
        {
            distance += Mathf.Pow(a[i] - b[i], 2);
        }
        return Mathf.Sqrt(distance);
    }

    private float previousChange = float.MaxValue;

    public float[] RecallMemory2(float[] query)
    {
        float[] queryState = lstm.Forward(query);
        float[] closestMemory = null;
        float closestDistance = float.MaxValue;

        foreach (var mem in memory)
        {
            float distance = ComputeDistance(queryState, mem);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMemory = mem;
            }
        }

        float change = ComputeDistance(queryState, closestMemory);

        // Log the difference between current and previous state
        Debug.Log($"Change from previous state: {Mathf.Abs(previousChange - change)}");
        previousChange = change;

        return closestMemory;
        UpdateVisualization(closestMemory);

    }

    private GameObject recallSphere;

    private void UpdateVisualization(float[] recalledMemory)
    {
        if (recallSphere == null)
        {
            recallSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            recallSphere.transform.localScale = Vector3.one * 0.5f;
        }

        // Map memory values to sphere position or color
        recallSphere.transform.position = new Vector3(recalledMemory[0], recalledMemory[1], recalledMemory[2]);
        Renderer renderer = recallSphere.GetComponent<Renderer>();
        renderer.material.color = new Color(recalledMemory[3], 0.5f, 0.5f, 1.0f);
    }

}
