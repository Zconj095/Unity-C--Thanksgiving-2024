using System.Collections.Generic;
using System.Linq;

public class ShortTermMemory
{
    private Queue<MemoryItem> memoryQueue;
    private int capacity;

    public ShortTermMemory(int capacity)
    {
        this.capacity = capacity;
        memoryQueue = new Queue<MemoryItem>();
    }

    public void AddItem(MemoryItem item)
    {
        if (memoryQueue.Count >= capacity)
        {
            memoryQueue.Dequeue(); // Remove the oldest item
        }
        memoryQueue.Enqueue(item);
    }

    public List<MemoryItem> RetrieveItems(int count)
    {
        return memoryQueue.Take(count).ToList();
    }

    public void Clear()
    {
        memoryQueue.Clear();
    }
}
