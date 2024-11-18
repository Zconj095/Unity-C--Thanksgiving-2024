using UnityEngine;

public class QuantumEntangledHyperdimensionalVector : MonoBehaviour
{
    [Header("Vector Configuration")]
    [SerializeField] private Vector4[] components; // Components stored as Vector4 array
    [SerializeField] private int dimensions;       // Total number of dimensions

    /// <summary>
    /// Exposes components publicly for read access.
    /// </summary>
    public Vector4[] Components => components;

    /// <summary>
    /// Initializes the vector with the specified number of dimensions.
    /// </summary>
    public void Initialize(int dimensions)
    {
        this.dimensions = dimensions;
        int numVectors = Mathf.CeilToInt(dimensions / 4.0f); // Calculate required Vector4 components
        components = new Vector4[numVectors];
    }

    /// <summary>
    /// Sets the value of a specific dimension.
    /// </summary>
    public void SetDimension(int index, float value)
    {
        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= components.Length)
            return;

        Vector4 vec = components[vectorIndex];
        switch (componentIndex)
        {
            case 0: vec.x = value; break;
            case 1: vec.y = value; break;
            case 2: vec.z = value; break;
            case 3: vec.w = value; break;
        }
        components[vectorIndex] = vec;
    }

    /// <summary>
    /// Gets the value of a specific dimension.
    /// </summary>
    public float GetDimension(int index)
    {
        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= components.Length)
            return 0;

        Vector4 vec = components[vectorIndex];
        return componentIndex switch
        {
            0 => vec.x,
            1 => vec.y,
            2 => vec.z,
            3 => vec.w,
            _ => 0
        };
    }

    /// <summary>
    /// Normalizes the vector to unit magnitude.
    /// </summary>
    public void Normalize()
    {
        float magnitude = 0;
        foreach (Vector4 vec in components)
        {
            magnitude += vec.sqrMagnitude;
        }
        magnitude = Mathf.Sqrt(magnitude);

        if (magnitude > 0)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i] /= magnitude;
            }
        }
    }
}
