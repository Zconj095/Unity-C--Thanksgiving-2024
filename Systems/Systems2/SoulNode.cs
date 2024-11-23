using System.Collections.Generic;
using UnityEngine;

public class SoulUnificationWithHebbian : MonoBehaviour
{
    [System.Serializable]
    public class SoulNode
    {
        public string Name;
        public float Intensity; // Represents emotional intensity (0-1)
        public Vector3 Position; // Position in 3D space
        public Color SoulColor; // Visual representation
    }

    public List<SoulNode> Souls; // All individual souls in the system
    public Dictionary<(int, int), float> HebbianWeights; // Hebbian connection weights
    public SoulNode UnifiedSoul; // Result of unification

    public LineRenderer ConnectionRendererPrefab; // For visualizing connections
    private List<LineRenderer> activeConnections = new List<LineRenderer>();

    void Start()
    {
        HebbianWeights = new Dictionary<(int, int), float>();

        // Initialize Hebbian weights
        for (int i = 0; i < Souls.Count; i++)
        {
            for (int j = i + 1; j < Souls.Count; j++)
            {
                HebbianWeights[(i, j)] = Random.Range(0.1f, 0.5f); // Initial weights
            }
        }
    }

    void Update()
    {
        VisualizeConnections();
    }

    public void UnifySouls()
    {
        if (Souls.Count == 0)
        {
            Debug.LogWarning("No souls available for unification.");
            return;
        }

        Vector3 combinedPosition = Vector3.zero;
        Color blendedColor = Color.black;
        float totalIntensity = 0f;

        // Contribution of each soul influenced by Hebbian weights
        foreach (var soul in Souls)
        {
            foreach (var weight in HebbianWeights)
            {
                var (soulIndexA, soulIndexB) = weight.Key;
                float weightValue = weight.Value;

                if (soulIndexA == Souls.IndexOf(soul) || soulIndexB == Souls.IndexOf(soul))
                {
                    soul.Intensity *= weightValue;
                }
            }

            combinedPosition += soul.Position * soul.Intensity;
            blendedColor += soul.SoulColor * soul.Intensity;
            totalIntensity += soul.Intensity;
        }

        combinedPosition /= totalIntensity;
        blendedColor /= Souls.Count;

        UnifiedSoul = new SoulNode
        {
            Name = "Unified Soul",
            Intensity = Mathf.Clamp(totalIntensity, 0, 1),
            Position = combinedPosition,
            SoulColor = blendedColor
        };

        Debug.Log($"Unified Soul Created: {UnifiedSoul.Name}");
    }

    public void HebbianLearning(int soulIndexA, int soulIndexB, float delta)
    {
        // Strengthen the connection weight based on co-activation
        var key = (Mathf.Min(soulIndexA, soulIndexB), Mathf.Max(soulIndexA, soulIndexB));

        if (HebbianWeights.ContainsKey(key))
        {
            HebbianWeights[key] = Mathf.Clamp(HebbianWeights[key] + delta, 0f, 1f);
            Debug.Log($"Updated Hebbian Weight ({soulIndexA}, {soulIndexB}): {HebbianWeights[key]}");
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

        // Draw connections based on Hebbian weights
        foreach (var weight in HebbianWeights)
        {
            var (soulIndexA, soulIndexB) = weight.Key;
            float weightValue = weight.Value;

            var line = Instantiate(ConnectionRendererPrefab);
            line.startColor = Souls[soulIndexA].SoulColor;
            line.endColor = Souls[soulIndexB].SoulColor;
            line.startWidth = weightValue * 0.1f;
            line.endWidth = weightValue * 0.1f;

            line.SetPosition(0, Souls[soulIndexA].Position);
            line.SetPosition(1, Souls[soulIndexB].Position);

            activeConnections.Add(line);
        }
    }
}
