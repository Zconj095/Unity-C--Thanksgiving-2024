using System.Collections.Generic;
using System.Linq;

public class VectorDatabase2
{
    private Dictionary<int, float[]> vectors = new Dictionary<int, float[]>();
    private string databasePath;

    public VectorDatabase2(string path)
    {
        databasePath = path;
    }

    public void AddVector(int id, float[] vector)
    {
        vectors[id] = vector;
        SaveToDisk(id, vector);
    }

    public float[] GetVector(int id)
    {
        if (vectors.ContainsKey(id))
            return vectors[id];
        return LoadFromDisk(id);
    }

    public Dictionary<int, float[]> GetAllVectors()
    {
        return vectors;
    }

    public bool ContainsVector(float[] vector)
    {
        return vectors.Values.Any(existingVector => existingVector.SequenceEqual(vector));
    }

    public int GenerateUniqueId()
    {
        int newId = vectors.Keys.DefaultIfEmpty(0).Max() + 1; // Generate the next unique ID
        return newId;
    }

    private void SaveToDisk(int id, float[] vector)
    {
        string filePath = $"{databasePath}/{id}.vec";
        System.IO.File.WriteAllBytes(filePath, VectorToBytes(vector));
    }

    private float[] LoadFromDisk(int id)
    {
        string filePath = $"{databasePath}/{id}.vec";
        if (!System.IO.File.Exists(filePath)) return null;
        return BytesToVector(System.IO.File.ReadAllBytes(filePath));
    }

    private byte[] VectorToBytes(float[] vector)
    {
        List<byte> bytes = new List<byte>();
        foreach (var value in vector)
        {
            bytes.AddRange(System.BitConverter.GetBytes(value));
        }
        return bytes.ToArray();
    }

    private float[] BytesToVector(byte[] bytes)
    {
        int size = sizeof(float);
        float[] vector = new float[bytes.Length / size];
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] = System.BitConverter.ToSingle(bytes, i * size);
        }
        return vector;
    }
}
