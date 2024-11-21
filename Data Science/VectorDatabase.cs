using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A database to store and manage 3D vectors with associated IDs.
/// </summary>
public class VectorDatabase : MonoBehaviour
{
    private List<VectorEntry> vectorEntries;

    private void Start()
    {
        vectorEntries = new List<VectorEntry>();
    }

    /// <summary>
    /// Adds a vector entry to the database.
    /// </summary>
    /// <param name="position">The 3D position of the vector.</param>
    /// <param name="id">The unique identifier for the vector.</param>
    public void AddVector(Vector3 position, string id)
    {
        if (vectorEntries.Exists(v => v.ID == id))
        {
            Debug.LogWarning($"A vector with ID '{id}' already exists in the database.");
            return;
        }

        vectorEntries.Add(new VectorEntry(position, id));
        Debug.Log($"Added vector with ID '{id}' at position {position}.");
    }

    /// <summary>
    /// Removes a vector entry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the vector to remove.</param>
    /// <returns>True if the vector was removed; otherwise, false.</returns>
    public bool RemoveVector(string id)
    {
        var entry = vectorEntries.Find(v => v.ID == id);
        if (entry != null)
        {
            vectorEntries.Remove(entry);
            Debug.Log($"Removed vector with ID '{id}'.");
            return true;
        }

        Debug.LogWarning($"Vector with ID '{id}' not found.");
        return false;
    }

    /// <summary>
    /// Queries all vectors within a specified radius of a query point.
    /// </summary>
    /// <param name="queryPoint">The center of the search area.</param>
    /// <param name="radius">The search radius.</param>
    /// <returns>A list of vector entries within the radius.</returns>
    public List<VectorEntry> QueryNearbyVectors(Vector3 queryPoint, float radius)
    {
        List<VectorEntry> result = new List<VectorEntry>();
        float radiusSquared = radius * radius; // Avoids repeated sqrt calculations

        foreach (var entry in vectorEntries)
        {
            if ((entry.Position - queryPoint).sqrMagnitude <= radiusSquared)
            {
                result.Add(entry);
            }
        }

        Debug.Log($"Found {result.Count} vectors within a radius of {radius} units from {queryPoint}.");
        return result;
    }
}

