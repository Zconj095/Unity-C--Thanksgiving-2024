using System.Collections.Generic;
using System.Linq;
public class LongTermMemory
{
    private Dictionary<string, MemoryItem> memoryStorage;

    public LongTermMemory()
    {
        memoryStorage = new Dictionary<string, MemoryItem>();
    }

    public void AddItem(MemoryItem item)
    {
        if (!memoryStorage.ContainsKey(item.Key))
        {
            memoryStorage[item.Key] = item;
        }
    }

    public MemoryItem RetrieveItem(string key)
    {
        return memoryStorage.ContainsKey(key) ? memoryStorage[key] : null;
    }

    public List<MemoryItem> RetrieveRelevantItems(float[] queryEmbedding, int topN = 5)
    {
        return memoryStorage.Values
            .OrderByDescending(item => ComputeSimilarity(queryEmbedding, item.Embedding))
            .Take(topN)
            .ToList();
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
        return dot / (UnityEngine.Mathf.Sqrt(mag1) * UnityEngine.Mathf.Sqrt(mag2)); // Cosine similarity
    }
}
