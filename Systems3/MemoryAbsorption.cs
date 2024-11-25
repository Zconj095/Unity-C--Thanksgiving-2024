
public class MemoryAbsorption
{
    private VectorDatabase2 vectorDatabase;
    private MemoryRecollection memoryRecollection;

    public MemoryAbsorption(VectorDatabase2 vectorDatabase, MemoryRecollection memoryRecollection)
    {
        this.vectorDatabase = vectorDatabase;
        this.memoryRecollection = memoryRecollection;
    }

    public void AbsorbDatabase()
    {
        foreach (var entry in vectorDatabase.GetAllVectors())
        {
            MemoryItem memoryItem = new MemoryItem(
                entry.Key.ToString(),
                $"Absorbed from database: {entry.Key}",
                entry.Value
            );

            memoryRecollection.AddToMemory(memoryItem);
        }
    }
}
