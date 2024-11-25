using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class DistributedVectorStorage
{
    private List<string> nodeAddresses;

    public DistributedVectorStorage(List<string> nodes)
    {
        nodeAddresses = nodes;
    }

    public string GetNodeForVector(int vectorId)
    {
        int index = vectorId % nodeAddresses.Count;
        return nodeAddresses[index];
    }

    public void StoreVector(int vectorId, float[] vector)
    {
        string node = GetNodeForVector(vectorId);
        // Send vector to the designated node (placeholder logic)
        Console.WriteLine($"Storing vector {vectorId} on node {node}");
    }

    public float[] RetrieveVector(int vectorId)
    {
        string node = GetNodeForVector(vectorId);
        // Retrieve vector from the designated node (placeholder logic)
        Console.WriteLine($"Retrieving vector {vectorId} from node {node}");
        return new float[128]; // Dummy data
    }
}
