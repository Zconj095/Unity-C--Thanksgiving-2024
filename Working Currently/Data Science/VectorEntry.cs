using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a single vector entry with a position and unique identifier.
/// </summary>
public class VectorEntry
{
    public Vector3 Position { get; }
    public string ID { get; }

    public VectorEntry(Vector3 position, string id)
    {
        Position = position;
        ID = id;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Position: {Position}";
    }
}
