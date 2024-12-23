using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoMemoryRetrievalSystem : MonoBehaviour
{
    private class Memory
    {
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public DateTime Timestamp { get; set; }
        public float Relevance { get; set; }

        public Memory(string content, List<string> tags, float initialRelevance = 1.0f)
        {
            Content = content;
            Tags = tags;
            Timestamp = DateTime.Now;
            Relevance = initialRelevance;
        }
    }

    private List<Memory> memories = new List<Memory>();
    private string[] sampleTags = { "AI", "Unity", "Physics", "Math", "Programming", "Quantum" };
    private string[] samplePhrases = {
        "Explored neural networks",
        "Learned about quantum entanglement",
        "Experimented with Unity animations",
        "Practiced matrix transformations",
        "Simulated physical systems",
        "Optimized game performance"
    };

    // Auto-generate and store a random memory
    private void GenerateAndStoreMemory()
    {
        System.Random random = new System.Random();

        // Generate random content
        string content = samplePhrases[random.Next(samplePhrases.Length)];

        // Generate random tags
        int tagCount = random.Next(1, 4); // Between 1 and 3 tags
        List<string> tags = new List<string>();
        for (int i = 0; i < tagCount; i++)
        {
            string tag = sampleTags[random.Next(sampleTags.Length)];
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
            }
        }

        // Generate random relevance
        float relevance = (float)(random.NextDouble() * 0.5 + 0.5); // Between 0.5 and 1.0

        // Store the memory
        Memory newMemory = new Memory(content, tags, relevance);
        memories.Add(newMemory);

        Debug.Log($"Memory Generated: {content} | Tags: {string.Join(", ", tags)} | Relevance: {relevance:F2}");
    }

    // Retrieve memories based on autogenerated queries
    private List<string> RetrieveAutoGeneratedMemories(string queryTag, bool useRelevance = true)
    {
        List<Memory> results = memories.Where(memory =>
            memory.Tags.Contains(queryTag)
        ).ToList();

        if (useRelevance)
        {
            results = results.OrderByDescending(memory => memory.Relevance).ToList();
        }

        return results.Select(memory => memory.Content).ToList();
    }

    // Apply temporal decay to memories
    private void ApplyTemporalDecay(float decayRate = 0.1f)
    {
        foreach (var memory in memories)
        {
            TimeSpan age = DateTime.Now - memory.Timestamp;
            memory.Relevance = Mathf.Max(0.0f, memory.Relevance - decayRate * (float)age.TotalMinutes);
        }
    }

    void Start()
    {
        // Generate random memories
        for (int i = 0; i < 10; i++)
        {
            GenerateAndStoreMemory();
        }

        // Apply temporal decay
        ApplyTemporalDecay(0.05f);

        // Retrieve memories with autogenerated tag query
        string queryTag = sampleTags[new System.Random().Next(sampleTags.Length)];
        var retrieved = RetrieveAutoGeneratedMemories(queryTag);

        Debug.Log($"Retrieved Memories for tag '{queryTag}': {string.Join(", ", retrieved)}");
    }
}
