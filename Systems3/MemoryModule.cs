using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MemoryModule
{
    private List<string> shortTermMemory = new List<string>();
    private Dictionary<string, string> longTermMemory = new Dictionary<string, string>();

    public void Remember(string key, string value)
    {
        if (shortTermMemory.Count > 10) shortTermMemory.RemoveAt(0); // Limit short-term memory
        shortTermMemory.Add(value);

        if (!longTermMemory.ContainsKey(key)) longTermMemory[key] = value;
    }

    public string Recall(string key)
    {
        return longTermMemory.ContainsKey(key) ? longTermMemory[key] : "No memory found.";
    }
}
