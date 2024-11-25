using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class MemoryRetrievalSystem2
{
    private ShortTermMemory shortTermMemory;
    private LongTermMemory longTermMemory;

    public MemoryRetrievalSystem2(ShortTermMemory stm, LongTermMemory ltm)
    {
        shortTermMemory = stm;
        longTermMemory = ltm;
    }

    public List<MemoryItem> RetrieveMemory(float[] queryEmbedding, int topN = 5)
    {
        // Retrieve from STM
        var stmItems = shortTermMemory.RetrieveItems(topN);

        // Retrieve from LTM
        var ltmItems = longTermMemory.RetrieveRelevantItems(queryEmbedding, topN);

        // Combine and prioritize
        var combinedItems = stmItems.Concat(ltmItems).ToList();
        return combinedItems
            .OrderByDescending(item => ComputeCombinedRelevance(queryEmbedding, item))
            .Take(topN)
            .ToList();
    }

    private float ComputeCombinedRelevance(float[] query, MemoryItem item)
    {
        float similarity = ComputeSimilarity(query, item.Embedding);
        float recencyFactor = (float)(1 / (1 + (DateTime.UtcNow - item.Timestamp).TotalSeconds));
        return similarity * 0.8f + recencyFactor * 0.2f; // Weighted relevance
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
        return dot / (Mathf.Sqrt(mag1) * Mathf.Sqrt(mag2));
    }
}
