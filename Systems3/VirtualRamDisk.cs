using System;
using System.Collections.Generic;

public class VirtualRamDisk : IDisposable
{
    public string Name { get; private set; }
    public long SizeBytes { get; private set; }
    private Dictionary<string, byte[]> storage; // Emulates file storage

    public VirtualRamDisk(string name, long sizeBytes)
    {
        Name = name;
        SizeBytes = sizeBytes;
        storage = new Dictionary<string, byte[]>();
    }

    public void Write(string key, byte[] data)
    {
        if (GetUsedSpace() + data.Length > SizeBytes)
        {
            throw new Exception("Insufficient space on RAM disk.");
        }

        storage[key] = data;
    }

    public byte[] Read(string key)
    {
        return storage.ContainsKey(key) ? storage[key] : null;
    }

    public void Delete(string key)
    {
        if (storage.ContainsKey(key))
        {
            storage.Remove(key);
        }
    }

    public long GetUsedSpace()
    {
        long usedSpace = 0;
        foreach (var file in storage.Values)
        {
            usedSpace += file.Length;
        }
        return usedSpace;
    }

    public void Dispose()
    {
        storage.Clear();
    }
}
