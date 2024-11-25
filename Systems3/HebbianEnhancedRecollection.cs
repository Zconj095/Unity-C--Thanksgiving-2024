using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class HebbianEnhancedRecollection
{
    private MemoryRecollection memoryRecollection;
    private LLMHebbianNetwork hebbianNetwork;

    public HebbianEnhancedRecollection(MemoryRecollection recollection, LLMHebbianNetwork network)
    {
        memoryRecollection = recollection;
        hebbianNetwork = network;
    }

    public List<MemoryItem> Recollect(float[] queryEmbedding, int topN = 5)
    {
        // Retrieve initial results from MemoryRecollection
        var initialResults = memoryRecollection.Recollect(queryEmbedding, topN);

        // Retrieve associated memories based on Hebbian connections
        var associatedMemories = initialResults
            .SelectMany(item => hebbianNetwork.GetAssociatedMemories(item.Key.GetHashCode())) // Convert string key to int using GetHashCode
            .Distinct()
            .Take(topN)
            .Select(id => memoryRecollection.GetMemoryById(id.ToString())) // Convert back to string for GetMemoryById
            .Where(item => item != null) // Filter out null results
            .ToList();

        // Combine initial results with associated memories and prioritize
        return initialResults.Concat(associatedMemories)
                             .Distinct()
                             .Take(topN)
                             .ToList();
    }
}
