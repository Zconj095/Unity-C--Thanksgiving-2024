public class UnifiedMemorySystem
{
    private LayeredShortTermMemory shortTermMemory;
    private DiskBasedLongTermMemory longTermMemory;

    public UnifiedMemorySystem(LayeredShortTermMemory stm, DiskBasedLongTermMemory ltm)
    {
        shortTermMemory = stm;
        longTermMemory = ltm;
    }

    public MemoryItem RetrieveMemory(string key)
    {
        // Check STM first
        var item = shortTermMemory.RetrieveItem(key);
        if (item != null) return item;

        // Fallback to LTM
        return longTermMemory.RetrieveItem(key);
    }

    public void AddMemory(MemoryItem item)
    {
        shortTermMemory.AddItem(item);

        // Backup to LTM if RAM limit is exceeded
        if (shortTermMemory.GetCurrentRAMUsage() + item.Embedding.Length * sizeof(float) > shortTermMemory.RamCapacityBytes)
        {
            longTermMemory.AddItem(item);
        }
    }
}
