using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnhancedPromptCache
{
    private class PromptData
    {
        public string Prompt { get; set; }
        public float[] Embedding { get; set; }
        public float Priority { get; set; }
        public float TimeToLive { get; set; } // In seconds
    }

    private List<PromptData> cache = new List<PromptData>();
    private float decayRate = 0.1f; // Decay priority over time

    public void AddPrompt(string prompt, float[] embedding, float initialPriority = 1.0f, float ttl = 300f)
    {
        cache.Add(new PromptData
        {
            Prompt = prompt,
            Embedding = embedding,
            Priority = initialPriority,
            TimeToLive = ttl
        });
    }

    public string GetHighestPriorityPrompt()
    {
        UpdatePriorities();
        return cache.OrderByDescending(p => p.Priority).FirstOrDefault()?.Prompt;
    }

    private void UpdatePriorities()
    {
        foreach (var promptData in cache)
        {
            promptData.Priority -= decayRate * Time.deltaTime;
            promptData.TimeToLive -= Time.deltaTime;
        }
        cache.RemoveAll(p => p.TimeToLive <= 0 || p.Priority <= 0);
    }
}
