using System.Collections.Generic;
using System.Linq;
using System;

public class MemoryRecollection
{
    private ShortTermMemory stm;
    private LongTermMemory ltm;

    public MemoryRecollection(ShortTermMemory shortTermMemory, LongTermMemory longTermMemory)
    {
        stm = shortTermMemory ?? throw new ArgumentNullException(nameof(shortTermMemory));
        ltm = longTermMemory ?? throw new ArgumentNullException(nameof(longTermMemory));
    }

    public void AddToMemory(MemoryItem memoryItem)
    {
        if (memoryItem == null)
        {
            throw new ArgumentNullException(nameof(memoryItem), "MemoryItem cannot be null.");
        }

        // Example logic to store based on RelevanceScore
        if (memoryItem.RelevanceScore >= 0.7f)
        {
            stm.AddItem(memoryItem);
        }
        else
        {
            ltm.StoreItem(memoryItem); // Corrected to StoreItem
        }
    }

    /// <summary>
    /// Retrieves a memory item by its unique key from STM and LTM.
    /// </summary>
    public MemoryItem GetMemoryById(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        // Check STM first
        var stmItem = stm.RetrieveItem(key);
        if (stmItem != null)
        {
            return stmItem;
        }

        // Check LTM if not found in STM
        return ltm.RetrieveItem(key);
    }

    public List<MemoryItem> Recollect(float[] queryEmbedding, int topN = 5)
    {
        if (queryEmbedding == null || queryEmbedding.Length == 0)
        {
            throw new ArgumentException("Query embedding cannot be null or empty.", nameof(queryEmbedding));
        }

        var stmItems = stm.RetrieveItems(topN);
        var ltmItems = ltm.RetrieveRelevantItems(queryEmbedding, topN);

        var combinedItems = stmItems.Concat(ltmItems).Distinct().ToList();
        return combinedItems
            .OrderByDescending(item => ComputeRelevance(queryEmbedding, item))
            .Take(topN)
            .ToList();
    }

    private float ComputeRelevance(float[] query, MemoryItem item)
    {
        float similarity = ComputeSimilarity(query, item.Embedding);
        float recencyFactor = ComputeRecencyFactor(item.Timestamp);
        return similarity * 0.8f + recencyFactor * 0.2f;
    }

    private float ComputeSimilarity(float[] query, float[] target)
    {
        float dot = 0, mag1 = 0, mag2 = 0;

        for (int i = 0; i < query.Length; i++)
        {
            dot += query[i] * target[i];
            mag1 += query[i] * query[i];
            mag2 += target[i] * target[i];
        }

        return mag1 > 0 && mag2 > 0 ? dot / (UnityEngine.Mathf.Sqrt(mag1) * UnityEngine.Mathf.Sqrt(mag2)) : 0;
    }

    private float ComputeRecencyFactor(DateTime timestamp)
    {
        double secondsElapsed = (DateTime.UtcNow - timestamp).TotalSeconds;
        return (float)(1 / (1 + secondsElapsed)); // Example decay
    }
}
