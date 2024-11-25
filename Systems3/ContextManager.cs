using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class ContextManager
{
    private int maxTokens;
    private List<MemoryItem> contextBuffer;

    public ContextManager(int maxTokens)
    {
        this.maxTokens = maxTokens;
        contextBuffer = new List<MemoryItem>();
    }

    public void AddToContext(MemoryItem memoryItem)
    {
        // Calculate token count for the new item
        int newTokens = Tokenize(memoryItem.Content).Length;

        // Remove oldest items until there's room
        while (GetContextTokenCount() + newTokens > maxTokens && contextBuffer.Count > 0)
        {
            contextBuffer.RemoveAt(0);
        }

        contextBuffer.Add(memoryItem);
    }

    public string GetContextAsString()
    {
        return string.Join(" ", contextBuffer.Select(item => item.Content));
    }

    private int GetContextTokenCount()
    {
        return contextBuffer.Sum(item => Tokenize(item.Content).Length);
    }

    private int[] Tokenize(string input)
    {
        return input.Split(' ').Select(word => word.GetHashCode()).ToArray();
    }
}
