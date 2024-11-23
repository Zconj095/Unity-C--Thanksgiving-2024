using UnityEngine;
using System.Collections.Generic;

public class EmotionalLayoutManager : MonoBehaviour
{
    public GameObject emotionNodePrefab; // Prefab for visualizing emotions
    public Transform layoutContainer;    // Parent for emotion nodes

    private readonly Dictionary<string, GameObject> emotionNodes = new Dictionary<string, GameObject>();

    void Start()
    {
        InitializeEmotionNodes();
    }

    void InitializeEmotionNodes()
    {
        string[] emotions = { "COURAGE", "FAITH", "GRATITUDE", "HAPPINESS", "HOPE", "JOY", "LOVE", "SERENE", "SERENITY", "SERIOUS", "TEMPERANCE", "UNKNOWN" };

        foreach (string emotion in emotions)
        {
            GameObject node = Instantiate(emotionNodePrefab, layoutContainer);
            node.name = emotion;
            node.GetComponentInChildren<TextMesh>().text = emotion; // Optional: Label the node
            emotionNodes[emotion] = node;

            // Assign an initial random position for visualization
            node.transform.localPosition = GetRandomPosition();
        }
    }

    public void UpdateEmotionLayout(string emotion, float pitch, float energy, float[] spectralContent)
    {
        if (!emotionNodes.ContainsKey(emotion)) return;

        GameObject node = emotionNodes[emotion];
        Vector3 position = CalculatePosition(pitch, energy, spectralContent);
        node.transform.localPosition = position;

        // Optional: Scale or color the node based on intensity
        float intensity = Mathf.Clamp01(energy);
        node.transform.localScale = Vector3.one * (0.5f + intensity);
        node.GetComponent<Renderer>().material.color = Color.Lerp(Color.gray, Color.red, intensity);
    }

    private Vector3 CalculatePosition(float pitch, float energy, float[] spectralContent)
    {
        // Map pitch, energy, and spectralContent to a 3D position
        float x = Mathf.Clamp(pitch / 500f, -1f, 1f); // Normalize pitch to [-1, 1]
        float y = Mathf.Clamp(energy, 0f, 1f);        // Energy as [0, 1]
        float z = Mathf.Clamp(spectralContent[0], 0f, 1f); // First spectral component as Z

        return new Vector3(x * 10f, y * 10f, z * 10f); // Scale for better visualization
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
