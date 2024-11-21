using UnityEngine;
using System.Collections.Generic;

public class MultiLocationalOriginBasis : MonoBehaviour
{
    /// <summary>
    /// Represents an origin in space with a name, position, and rotation.
    /// </summary>
    [System.Serializable]
    public struct Origin
    {
        public string Name; // Name of the origin
        public Vector3 Position; // Position in world space
        public Quaternion Rotation; // Orientation in world space
    }

    [Header("Origins Configuration")]
    [SerializeField] private List<Origin> origins = new List<Origin>(); // List of all origins
    [SerializeField] private Transform objectToTransform; // Object to position relative to origins
    [SerializeField] private string currentOriginName = "Origin1"; // Currently active origin

    private void Start()
    {
        // Initialize some example origins if not already set
        if (origins.Count == 0)
        {
            origins.Add(new Origin { Name = "Origin1", Position = new Vector3(0, 0, 0), Rotation = Quaternion.identity });
            origins.Add(new Origin { Name = "Origin2", Position = new Vector3(10, 0, 0), Rotation = Quaternion.Euler(0, 45, 0) });
            origins.Add(new Origin { Name = "Origin3", Position = new Vector3(-10, 0, 10), Rotation = Quaternion.Euler(0, 90, 0) });
        }

        // Set the object's position relative to the initial origin
        SetObjectRelativeToOrigin(currentOriginName);
    }

    private void Update()
    {
        // Continuously update the object's position relative to the selected origin
        if (objectToTransform != null)
        {
            SetObjectRelativeToOrigin(currentOriginName);
        }
    }

    /// <summary>
    /// Sets the object's position and rotation relative to the specified origin.
    /// </summary>
    /// <param name="originName">The name of the origin to use as a reference.</param>
    private void SetObjectRelativeToOrigin(string originName)
    {
        // Find the origin by name
        Origin? origin = GetOriginByName(originName);

        if (origin.HasValue)
        {
            // Apply the origin's position and rotation to the object
            objectToTransform.position = origin.Value.Position;
            objectToTransform.rotation = origin.Value.Rotation;

            Debug.Log($"Object set relative to {originName}: Position {objectToTransform.position}, Rotation {objectToTransform.rotation}");
        }
        else
        {
            Debug.LogError($"Origin not found: {originName}");
        }
    }

    /// <summary>
    /// Finds an origin by its name.
    /// </summary>
    /// <param name="name">The name of the origin.</param>
    /// <returns>The found origin or null if not found.</returns>
    private Origin? GetOriginByName(string name)
    {
        foreach (var origin in origins)
        {
            if (origin.Name == name)
            {
                return origin;
            }
        }
        return null;
    }

    /// <summary>
    /// Adds a new origin to the list.
    /// </summary>
    /// <param name="name">Name of the new origin.</param>
    /// <param name="position">Position of the new origin.</param>
    /// <param name="rotation">Rotation of the new origin.</param>
    public void AddOrigin(string name, Vector3 position, Quaternion rotation)
    {
        if (GetOriginByName(name).HasValue)
        {
            Debug.LogWarning($"Origin with name {name} already exists.");
            return;
        }

        origins.Add(new Origin { Name = name, Position = position, Rotation = rotation });
        Debug.Log($"New origin added: {name}");
    }

    /// <summary>
    /// Switches to a new origin by name.
    /// </summary>
    /// <param name="newOriginName">The name of the new origin.</param>
    public void SwitchOrigin(string newOriginName)
    {
        if (GetOriginByName(newOriginName).HasValue)
        {
            currentOriginName = newOriginName;
            Debug.Log($"Switched to origin: {newOriginName}");
        }
        else
        {
            Debug.LogError($"Origin not found: {newOriginName}");
        }
    }
}
