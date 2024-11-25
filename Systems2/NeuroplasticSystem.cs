using UnityEngine;
using System.Collections.Generic;

public class NeuroplasticSystem : MonoBehaviour
{
    [System.Serializable]
    public class NeuralNode
    {
        public string Name;
        public float ActivationLevel; // Current activation (0-1)
        public float Weight; // Dynamic weight of the connection
        public Vector3 Position; // Visualization
    }

    public List<NeuralNode> Nodes;
    public Dictionary<(int, int), float> Connections;

    private float plasticityRate = 0.05f; // Controls neuroplastic adjustment

    void Start()
    {
        InitializeConnections();
    }

    void Update()
    {
        EvolveConnections();
        VisualizeConnections();
    }

    private void InitializeConnections()
    {
        Connections = new Dictionary<(int, int), float>();
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = i + 1; j < Nodes.Count; j++)
            {
                Connections[(i, j)] = Random.Range(0.1f, 0.5f);
            }
        }
    }

    private void EvolveConnections()
    {
        foreach (var connection in Connections)
        {
            var (a, b) = connection.Key;
            float activity = Nodes[a].ActivationLevel * Nodes[b].ActivationLevel;

            // Adjust weights dynamically
            Connections[(a, b)] += plasticityRate * activity;
            Connections[(a, b)] = Mathf.Clamp(Connections[(a, b)], 0, 1);
        }
    }

    private void VisualizeConnections()
    {
        foreach (var connection in Connections)
        {
            var (a, b) = connection.Key;
            Debug.DrawLine(Nodes[a].Position, Nodes[b].Position, Color.Lerp(Color.blue, Color.red, Connections[(a, b)]));
        }
    }
}
