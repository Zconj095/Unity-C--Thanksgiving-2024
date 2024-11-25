using System.Collections.Generic;

public class RamDiskManager
{
    private Dictionary<string, VirtualRamDisk> ramDisks;

    public RamDiskManager()
    {
        ramDisks = new Dictionary<string, VirtualRamDisk>();
    }

    public VirtualRamDisk CreateRamDisk(string name, long sizeBytes)
    {
        if (ramDisks.ContainsKey(name))
        {
            throw new System.Exception($"RAM disk {name} already exists.");
        }

        VirtualRamDisk ramDisk = new VirtualRamDisk(name, sizeBytes);
        ramDisks[name] = ramDisk;
        return ramDisk;
    }

    public void DeleteRamDisk(string name)
    {
        if (ramDisks.ContainsKey(name))
        {
            ramDisks[name].Dispose();
            ramDisks.Remove(name);
        }
    }

    public VirtualRamDisk GetRamDisk(string name)
    {
        return ramDisks.ContainsKey(name) ? ramDisks[name] : null;
    }
}
