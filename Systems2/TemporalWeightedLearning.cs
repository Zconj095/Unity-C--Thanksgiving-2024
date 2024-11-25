using System.Collections.Generic;
using UnityEngine;

public class TemporalWeightedLearning : MonoBehaviour
{
    [System.Serializable]
    public class LearningNode
    {
        public string Name;
        public float TemporalWeight; // Weight based on time
        public float ActivationValue; // Node's influence
        public float Timestamp; // Time of last activation
        public Vector3 Position; // For visualization
        public Color BaseColor; // Default color
        public Color HighlightColor; // Highlight during activation
    }

    public List<LearningNode> Nodes; // Nodes in the system
    public Dictionary<(int, int), float> ConnectionWeights; // Weights between nodes
    public float DecayRate = 0.01f; // Temporal decay rate
    public float ReinforcementFactor = 0.1f; // Reinforcement for reactivated nodes

    public LineRenderer ConnectionRendererPrefab; // For visualizing connections
    private List<LineRenderer> activeConnections = new List<LineRenderer>();

    void Start()
    {
        ConnectionWeights = new Dictionary<(int, int), float>();

        // Initialize connections
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = i + 1; j < Nodes.Count; j++)
            {
                ConnectionWeights[(i, j)] = Random.Range(0.1f, 0.5f); // Random initial weights
            }
        }
    }

    void Update()
    {
        ApplyTemporalDecay();
        VisualizeConnections();
    }

    public void ActivateNode(int nodeIndex)
    {
        if (nodeIndex < 0 || nodeIndex >= Nodes.Count)
        {
            Debug.LogWarning("Invalid node index.");
            return;
        }

        var node = Nodes[nodeIndex];
        float currentTime = Time.time;

        // Reinforce temporal weight based on activation
        float timeSinceLastActivation = currentTime - node.Timestamp;
        float reinforcement = Mathf.Exp(-timeSinceLastActivation * DecayRate) * ReinforcementFactor;
        node.TemporalWeight += reinforcement;
        node.TemporalWeight = Mathf.Clamp01(node.TemporalWeight);

        node.Timestamp = currentTime;

        Debug.Log($"Node Activated: {node.Name}, Temporal Weight: {node.TemporalWeight}");

        PropagateActivation(nodeIndex);
    }

    private void ApplyTemporalDecay()
    {
        float currentTime = Time.time;

        foreach (var node in Nodes)
        {
            float timeSinceLastActivation = currentTime - node.Timestamp;
            node.TemporalWeight *= Mathf.Exp(-timeSinceLastActivation * DecayRate);
            node.TemporalWeight = Mathf.Clamp01(node.TemporalWeight);
        }
    }

    private void PropagateActivation(int nodeIndex)
    {
        foreach (var connection in ConnectionWeights)
        {
            var (a, b) = connection.Key;
            if (a == nodeIndex || b == nodeIndex)
            {
                int targetNodeIndex = (a == nodeIndex) ? b : a;
                var targetNode = Nodes[targetNodeIndex];

                // Update connection weights based on temporal weights
                ConnectionWeights[(a, b)] += Nodes[nodeIndex].TemporalWeight * ReinforcementFactor;
                ConnectionWeights[(a, b)] = Mathf.Clamp(ConnectionWeights[(a, b)], 0, 1);

                ActivateNode(targetNodeIndex);
            }
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

        // Draw connections
        foreach (var connection in ConnectionWeights)
        {
            var (a, b) = connection.Key;
            float weight = connection.Value;

            var line = Instantiate(ConnectionRendererPrefab);
            line.startColor = Nodes[a].BaseColor;
            line.endColor = Nodes[b].BaseColor;
            line.startWidth = weight * 0.1f;
            line.endWidth = weight * 0.1f;

            line.SetPosition(0, Nodes[a].Position);
            line.SetPosition(1, Nodes[b].Position);

            activeConnections.Add(line);
        }
    }
}
