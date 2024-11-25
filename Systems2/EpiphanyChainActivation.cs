using System.Collections.Generic;
using UnityEngine;

public class EpiphanyChainActivation : MonoBehaviour
{
    [System.Serializable]
    public class MemoryNode
    {
        public string Name;
        public float ActivationThreshold; // Threshold for activation
        public float CurrentActivation; // Current activation level
        public float ForgetRate; // Rate at which activation decays
        public Vector3 Position; // Position in space
        public Color BaseColor; // Default color of the node
        public Color ActivatedColor; // Color during activation
    }

    public List<MemoryNode> MemoryNodes; // List of memory nodes
    public Dictionary<(int, int), float> ConnectionWeights; // Weighted connections
    public LineRenderer ConnectionLinePrefab; // For visualizing connections

    private List<LineRenderer> activeConnections = new List<LineRenderer>();

    void Start()
    {
        // Initialize connection weights
        ConnectionWeights = new Dictionary<(int, int), float>();
        for (int i = 0; i < MemoryNodes.Count; i++)
        {
            for (int j = i + 1; j < MemoryNodes.Count; j++)
            {
                ConnectionWeights[(i, j)] = Random.Range(0.1f, 0.5f); // Random initial weights
            }
        }
    }

    void Update()
    {
        DecayActivation();
        VisualizeConnections();
    }

    public void TriggerActivation(int nodeIndex, float inputSignal)
    {
        if (nodeIndex < 0 || nodeIndex >= MemoryNodes.Count)
        {
            Debug.LogWarning("Invalid node index.");
            return;
        }

        var node = MemoryNodes[nodeIndex];
        node.CurrentActivation += inputSignal;

        if (node.CurrentActivation >= node.ActivationThreshold)
        {
            ActivateNode(nodeIndex);
        }
    }

    private void ActivateNode(int nodeIndex)
    {
        var node = MemoryNodes[nodeIndex];
        Debug.Log($"Node Activated: {node.Name}");

        // Visual representation
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = node.Position;
        var renderer = sphere.GetComponent<Renderer>();
        renderer.material.color = node.ActivatedColor;

        // Trigger connections
        foreach (var connection in ConnectionWeights)
        {
            var (a, b) = connection.Key;
            if (a == nodeIndex || b == nodeIndex)
            {
                int targetNodeIndex = (a == nodeIndex) ? b : a;
                float weight = connection.Value;
                TriggerActivation(targetNodeIndex, weight * node.CurrentActivation);
            }
        }

        // Strengthen connections (neuroplasticity)
        foreach (var connection in ConnectionWeights.Keys)
        {
            if (connection.Item1 == nodeIndex || connection.Item2 == nodeIndex)
            {
                ConnectionWeights[connection] += 0.01f; // Increment weight slightly
            }
        }
    }

    private void DecayActivation()
    {
        foreach (var node in MemoryNodes)
        {
            node.CurrentActivation = Mathf.Max(0, node.CurrentActivation - node.ForgetRate * Time.deltaTime);
        }
    }

    private void VisualizeConnections()
    {
        // Clear old connections
        foreach (var line in activeConnections)
        {
            Destroy(line.gameObject);
        }
        activeConnections.Clear();

        // Draw connections based on weights
        foreach (var connection in ConnectionWeights)
        {
            var (a, b) = connection.Key;
            float weight = connection.Value;

            var line = Instantiate(ConnectionLinePrefab);
            line.startColor = MemoryNodes[a].BaseColor;
            line.endColor = MemoryNodes[b].BaseColor;
            line.startWidth = weight * 0.1f;
            line.endWidth = weight * 0.1f;

            line.SetPosition(0, MemoryNodes[a].Position);
            line.SetPosition(1, MemoryNodes[b].Position);

            activeConnections.Add(line);
        }
    }
}
