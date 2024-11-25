using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class MemoryItem
{
    public string Key { get; set; }          // Unique identifier
    public string Content { get; set; }     // Raw content (e.g., text)
    public float[] Embedding { get; set; }  // Embedding vector representation
    public DateTime Timestamp { get; set; } // Time of storage
    public float RelevanceScore { get; set; } // Score for prioritization

    public MemoryItem(string key, string content, float[] embedding)
    {
        Key = key;
        Content = content;
        Embedding = embedding;
        Timestamp = DateTime.UtcNow;
        RelevanceScore = 1.0f; // Default score
    }
}
