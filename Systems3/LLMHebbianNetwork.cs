using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class LLMHebbianNetwork
{
    private Dictionary<(int, int), float> weights;

    public LLMHebbianNetwork()
    {
        weights = new Dictionary<(int, int), float>();
    }

    public void StrengthenConnection(int memoryId1, int memoryId2)
    {
        var key = (Math.Min(memoryId1, memoryId2), Math.Max(memoryId1, memoryId2));

        if (weights.ContainsKey(key))
        {
            weights[key] += 0.1f; // Increment weight
        }
        else
        {
            weights[key] = 0.1f; // Initialize weight
        }
    }

    public List<int> GetAssociatedMemories(int memoryId)
    {
        return weights
            .Where(pair => pair.Key.Item1 == memoryId || pair.Key.Item2 == memoryId)
            .OrderByDescending(pair => pair.Value)
            .Select(pair => pair.Key.Item1 == memoryId ? pair.Key.Item2 : pair.Key.Item1)
            .ToList();
    }
}
