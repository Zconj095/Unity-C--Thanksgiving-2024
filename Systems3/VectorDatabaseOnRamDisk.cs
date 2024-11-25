using System;
using System.Collections.Generic;
public class VectorDatabaseOnRamDisk
{
    private VirtualRamDisk ramDisk;

    public VectorDatabaseOnRamDisk(VirtualRamDisk ramDisk)
    {
        this.ramDisk = ramDisk;
    }

    public void AddVector(int id, float[] vector)
    {
        string key = $"vector_{id}";
        byte[] data = VectorToBytes(vector);
        ramDisk.Write(key, data);
    }

    public float[] GetVector(int id)
    {
        string key = $"vector_{id}";
        byte[] data = ramDisk.Read(key);
        return data != null ? BytesToVector(data) : null;
    }

    private byte[] VectorToBytes(float[] vector)
    {
        List<byte> bytes = new List<byte>();
        foreach (var value in vector)
        {
            bytes.AddRange(BitConverter.GetBytes(value));
        }
        return bytes.ToArray();
    }

    private float[] BytesToVector(byte[] bytes)
    {
        int size = sizeof(float);
        float[] vector = new float[bytes.Length / size];
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] = BitConverter.ToSingle(bytes, i * size);
        }
        return vector;
    }
}
