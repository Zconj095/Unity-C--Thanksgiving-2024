using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventMemoryUpdater
{
    private EnhancedPromptCache memoryCache = new EnhancedPromptCache();

    public void RecordEvent(string eventDescription, Vector3 location, float[] embedding)
    {
        string memoryKey = $"{eventDescription}@{location}";
        memoryCache.AddPrompt(memoryKey, embedding, initialPriority: 1.0f, ttl: 600f);
        Debug.Log($"Event Recorded: {memoryKey}");
    }

    public string RecallRelevantMemory(Vector3 queryLocation, float proximityThreshold = 10f)
    {
        foreach (var promptChar in memoryCache.GetHighestPriorityPrompt()) // Handle char collection
        {
            string prompt = promptChar.ToString(); // Convert char to string
            string[] parts = prompt.Split('@'); // Split works on strings
            if (parts.Length == 2 && Vector3.Distance(queryLocation, ParseLocation(parts[1])) < proximityThreshold)
            {
                return prompt;
            }
        }
        return "No relevant memories found.";
    }

    private Vector3 ParseLocation(string location)
    {
        string[] coords = location.Trim('(', ')').Split(','); // Split on ',' as a string
        return new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2]));
    }
}
