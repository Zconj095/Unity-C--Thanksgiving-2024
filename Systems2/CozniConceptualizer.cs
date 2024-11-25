using System.Collections.Generic;
using UnityEngine;

public class CozniConceptualizer : MonoBehaviour
{
    [System.Serializable]
    public class ConceptNode
    {
        public string Name;
        public float ActivationLevel; // Current activation (0-1)
        public float Importance; // Concept relevance
        public Vector3 Position; // For visualization
        public List<int> ConnectedNodes; // Indices of connected nodes
        public Color BaseColor; // Default color
        public Color ActiveColor; // Color during activation
        public string Context; // Additional metadata
        public float Timestamp; // Time of creation
    }

    public List<ConceptNode> ConceptGraph; // The conceptual network
    public Dictionary<(int, int), float> ConnectionWeights; // Connection strength between nodes
    public LineRenderer ConnectionRendererPrefab; // For visualizing connections
    private List<LineRenderer> activeConnections = new List<LineRenderer>();

    public float HindsightSensitivity = 0.5f; // Sensitivity to hindsight reevaluation

    void Start()
    {
        ConnectionWeights = new Dictionary<(int, int), float>();

        // Initialize connections
        for (int i = 0; i < ConceptGraph.Count; i++)
        {
            for (int j = i + 1; j < ConceptGraph.Count; j++)
            {
                ConnectionWeights[(i, j)] = Random.Range(0.1f, 0.5f); // Initial random weights
            }
        }
    }

    void Update()
    {
        VisualizeConceptGraph();
    }

    public void FormNewConcept(string name, float importance, Vector3 position, string context)
    {
        ConceptNode newConcept = new ConceptNode
        {
            Name = name,
            ActivationLevel = 0,
            Importance = importance,
            Position = position,
            ConnectedNodes = new List<int>(),
            BaseColor = Color.white,
            ActiveColor = Color.yellow,
            Context = context,
            Timestamp = Time.time
        };

        ConceptGraph.Add(newConcept);
    }

    public void ActivateConcept(int nodeIndex, float inputSignal)
    {
        if (nodeIndex < 0 || nodeIndex >= ConceptGraph.Count)
        {
            Debug.LogWarning("Invalid node index.");
            return;
        }

        var node = ConceptGraph[nodeIndex];
        node.ActivationLevel += inputSignal;

        // Check if activation threshold is met
        if (node.ActivationLevel >= node.Importance)
        {
            Debug.Log($"Concept Activated: {node.Name}");
            ReflectOnHindsight(nodeIndex);
        }
    }

    private void ReflectOnHindsight(int activatedNodeIndex)
    {
        var activatedNode = ConceptGraph[activatedNodeIndex];

        foreach (var connection in ConnectionWeights)
        {
            var (a, b) = connection.Key;
            if (a == activatedNodeIndex || b == activatedNodeIndex)
            {
                int targetNodeIndex = (a == activatedNodeIndex) ? b : a;
                var targetNode = ConceptGraph[targetNodeIndex];

                // Adjust weights based on hindsight
                float hindsightAdjustment = HindsightSensitivity * activatedNode.Importance;
                ConnectionWeights[(a, b)] += hindsightAdjustment;

                Debug.Log($"Hindsight Updated Connection ({a}, {b}): {ConnectionWeights[(a, b)]}");
            }
        }
    }

    private void VisualizeConceptGraph()
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
            line.startColor = ConceptGraph[a].BaseColor;
            line.endColor = ConceptGraph[b].BaseColor;
            line.startWidth = weight * 0.1f;
            line.endWidth = weight * 0.1f;

            line.SetPosition(0, ConceptGraph[a].Position);
            line.SetPosition(1, ConceptGraph[b].Position);

            activeConnections.Add(line);
        }
    }
}
