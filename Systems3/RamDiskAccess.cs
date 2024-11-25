using System.IO;
using System;
using System.Collections.Generic;
public class RamDiskAccess
{
    public static string BasePath = "D:\\"; // Mounted Dimmdrive RAM disk path

    public static void SaveVectorToDisk(string filename, float[] vector)
    {
        string path = Path.Combine(BasePath, filename);
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            foreach (float value in vector)
            {
                writer.Write(value);
            }
        }
    }

    public static float[] LoadVectorFromDisk(string filename)
    {
        string path = Path.Combine(BasePath, filename);
        if (!File.Exists(path)) return null;

        List<float> vector = new List<float>();
        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                vector.Add(reader.ReadSingle());
            }
        }
        return vector.ToArray();
    }
}
