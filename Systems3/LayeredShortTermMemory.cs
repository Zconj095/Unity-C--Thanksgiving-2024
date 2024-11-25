using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class LayeredShortTermMemory
{
    private Queue<MemoryItem> l1Cache;
    private Queue<MemoryItem> l2Cache;
    private Queue<MemoryItem> l3Cache;
    private List<MemoryItem> ramMemory;
    private int l1Capacity;
    private int l2Capacity;
    private int l3Capacity;
    private long ramCapacityBytes;

    public LayeredShortTermMemory(int l1Size, int l2Size, int l3Size, long ramSizeBytes)
    {
        l1Capacity = l1Size;
        l2Capacity = l2Size;
        l3Capacity = l3Size;
        ramCapacityBytes = ramSizeBytes;

        l1Cache = new Queue<MemoryItem>();
        l2Cache = new Queue<MemoryItem>();
        l3Cache = new Queue<MemoryItem>();
        ramMemory = new List<MemoryItem>();
    }

    public void AddItem(MemoryItem item)
    {
        if (l1Cache.Count < l1Capacity)
        {
            l1Cache.Enqueue(item);
        }
        else if (l2Cache.Count < l2Capacity)
        {
            l2Cache.Enqueue(item);
        }
        else if (l3Cache.Count < l3Capacity)
        {
            l3Cache.Enqueue(item);
        }
        else if (GetRAMUsage() + item.Embedding.Length * sizeof(float) <= ramCapacityBytes)
        {
            ramMemory.Add(item);
        }
        else
        {
            Console.WriteLine("RAM memory limit reached. Item cannot be stored.");
        }
    }

    public MemoryItem RetrieveItem(string key)
    {
        // Check L1 Cache
        foreach (var item in l1Cache)
        {
            if (item.Key == key) return item;
        }

        // Check L2 Cache
        foreach (var item in l2Cache)
        {
            if (item.Key == key) return item;
        }

        // Check L3 Cache
        foreach (var item in l3Cache)
        {
            if (item.Key == key) return item;
        }

        // Check RAM
        return ramMemory.Find(item => item.Key == key);
    }

    /// <summary>
    /// Exposes RAM capacity as a public property.
    /// </summary>
    public long RamCapacityBytes => ramCapacityBytes;

    /// <summary>
    /// Provides a public method to get RAM usage.
    /// </summary>
    public long GetCurrentRAMUsage()
    {
        return GetRAMUsage();
    }

    // Private method for calculating RAM usage.
    private long GetRAMUsage()
    {
        long totalUsage = 0;
        foreach (var item in ramMemory)
        {
            totalUsage += item.Embedding.Length * sizeof(float);
        }
        return totalUsage;
    }
}
