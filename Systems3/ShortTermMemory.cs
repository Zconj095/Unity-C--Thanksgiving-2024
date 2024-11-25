using System.Collections.Generic;
using System.Linq;

public class ShortTermMemory
{
    private Queue<MemoryItem> memoryQueue;
    private Dictionary<string, MemoryItem> memoryLookup;
    private int capacity;

    public ShortTermMemory(int capacity)
    {
        this.capacity = capacity;
        memoryQueue = new Queue<MemoryItem>();
        memoryLookup = new Dictionary<string, MemoryItem>();
    }

    /// <summary>
    /// Adds a memory item to the short-term memory. Evicts the oldest item if capacity is exceeded.
    /// </summary>
    public void AddItem(MemoryItem item)
    {
        if (item == null) throw new System.ArgumentNullException(nameof(item), "MemoryItem cannot be null.");

        if (memoryLookup.ContainsKey(item.Key))
        {
            // Update the existing item (if needed)
            memoryLookup[item.Key] = item;
        }
        else
        {
            if (memoryQueue.Count >= capacity)
            {
                var oldestItem = memoryQueue.Dequeue();
                memoryLookup.Remove(oldestItem.Key); // Remove the oldest item from the lookup as well
            }

            memoryQueue.Enqueue(item);
            memoryLookup[item.Key] = item; // Add to lookup for quick access
        }
    }

    /// <summary>
    /// Retrieves the most recent memory items up to the specified count.
    /// </summary>
    public List<MemoryItem> RetrieveItems(int count)
    {
        return memoryQueue.Reverse().Take(count).ToList(); // Reverse to get the most recent items
    }

    /// <summary>
    /// Retrieves a specific memory item by its unique key.
    /// </summary>
    public MemoryItem RetrieveItem(string key)
    {
        if (string.IsNullOrEmpty(key)) throw new System.ArgumentException("Key cannot be null or empty.", nameof(key));

        return memoryLookup.TryGetValue(key, out var item) ? item : null;
    }

    /// <summary>
    /// Clears all memory items from the short-term memory.
    /// </summary>
    public void Clear()
    {
        memoryQueue.Clear();
        memoryLookup.Clear();
    }
}
