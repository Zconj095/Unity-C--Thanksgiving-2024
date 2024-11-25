using System.Collections.Generic;

public class InstructionCache
{
    private Dictionary<string, object> cache;

    public InstructionCache()
    {
        cache = new Dictionary<string, object>();
    }

    public void AddToCache(string instruction, object result)
    {
        cache[instruction] = result;
    }

    public object GetFromCache(string instruction)
    {
        return cache.ContainsKey(instruction) ? cache[instruction] : null;
    }

    public bool IsInCache(string instruction)
    {
        return cache.ContainsKey(instruction);
    }
}
