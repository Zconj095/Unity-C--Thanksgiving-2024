using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PromptCache
{
    private Dictionary<int, float[]> cache = new Dictionary<int, float[]>(); // Maps prompt ID to vector
    private int nextId = 0;

    public int AddPrompt(string prompt, float[] embedding)
    {
        cache[nextId] = embedding;
        return nextId++; // Return ID for correlation
    }

    public float[] GetPromptVector(int id)
    {
        return cache.ContainsKey(id) ? cache[id] : null;
    }
}
