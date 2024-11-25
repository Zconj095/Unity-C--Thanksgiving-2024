using System;
using System.Collections.Generic;
using System.Linq;

public class LongTermMemory
{
    private Dictionary<string, MemoryItem> memoryStorage;

    public LongTermMemory()
    {
        memoryStorage = new Dictionary<string, MemoryItem>();
    }

    /// <summary>
    /// Stores a new memory item in the storage. If an item with the same key exists, it will be skipped.
    /// </summary>
    /// <param name="item">The memory item to store.</param>
    public void StoreItem(MemoryItem item) // Renamed from AddItem
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "MemoryItem cannot be null.");
        }

        if (!memoryStorage.ContainsKey(item.Key))
        {
            memoryStorage[item.Key] = item;
        }
    }

    /// <summary>
    /// Retrieves a specific memory item by its key.
    /// </summary>
    public MemoryItem RetrieveItem(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        return memoryStorage.ContainsKey(key) ? memoryStorage[key] : null;
    }

    /// <summary>
    /// Retrieves the top N most relevant memory items based on a query embedding.
    /// </summary>
    public List<MemoryItem> RetrieveRelevantItems(float[] queryEmbedding, int topN = 5)
    {
        if (queryEmbedding == null || queryEmbedding.Length == 0)
        {
            throw new ArgumentException("Query embedding cannot be null or empty.", nameof(queryEmbedding));
        }

        return memoryStorage.Values
            .OrderByDescending(item => ComputeSimilarity(queryEmbedding, item.Embedding))
            .Take(topN)
            .ToList();
    }

    private float ComputeSimilarity(float[] query, float[] target)
    {
        if (query.Length != target.Length)
        {
            throw new ArgumentException("Embedding vectors must have the same length.");
        }

        float dot = 0, mag1 = 0, mag2 = 0;

        for (int i = 0; i < query.Length; i++)
        {
            dot += query[i] * target[i];
            mag1 += query[i] * query[i];
            mag2 += target[i] * target[i];
        }

        return (mag1 > 0 && mag2 > 0) ? dot / (UnityEngine.Mathf.Sqrt(mag1) * UnityEngine.Mathf.Sqrt(mag2)) : 0;
    }
}
