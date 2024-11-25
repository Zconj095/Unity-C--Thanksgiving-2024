using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class DiskBasedLongTermMemory
{
    private string storagePath;

    public DiskBasedLongTermMemory(string path)
    {
        storagePath = path;
        Directory.CreateDirectory(storagePath);
    }

    /// <summary>
    /// Adds an item to disk storage.
    /// </summary>
    public void AddItem(MemoryItem item)
    {
        string filePath = Path.Combine(storagePath, $"{item.Key}.txt");

        // Save key, content, and embedding as text
        string content = $"{item.Key}\n{item.Content}\n{string.Join(",", item.Embedding)}\n{item.Timestamp:O}\n{item.RelevanceScore}";
        File.WriteAllText(filePath, content);
    }

    /// <summary>
    /// Retrieves an item from disk storage.
    /// </summary>
    public MemoryItem RetrieveItem(string key)
    {
        string filePath = Path.Combine(storagePath, $"{key}.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length >= 5) // Ensure file has all components
            {
                string content = lines[1];
                float[] embedding = Array.ConvertAll(lines[2].Split(','), float.Parse);
                DateTime timestamp = DateTime.Parse(lines[3], null, System.Globalization.DateTimeStyles.RoundtripKind);
                float relevanceScore = float.Parse(lines[4]);

                return new MemoryItem(lines[0], content, embedding)
                {
                    Timestamp = timestamp,
                    RelevanceScore = relevanceScore
                };
            }
        }
        return null;
    }
}
