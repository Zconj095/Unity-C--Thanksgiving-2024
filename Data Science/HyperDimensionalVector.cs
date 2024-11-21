using UnityEngine;

public class HyperDimensionalVector : MonoBehaviour
{
    [Header("HyperDimensional Vector Settings")]
    [SerializeField] private Vector4[] components; // Private components array
    public Vector4[] Components => components;    // Public property for read access

    private int dimensions; // Total number of dimensions

    /// <summary>
    /// Gets the total number of dimensions.
    /// </summary>
    public int Dimensions => dimensions;

    /// <summary>
    /// Initializes the HyperDimensionalVector with the specified number of dimensions.
    /// </summary>
    public void Initialize(int dimensions)
    {
        this.dimensions = dimensions; // Store the total number of dimensions
        int numVectors = Mathf.CeilToInt(dimensions / 4.0f);
        components = new Vector4[numVectors];
    }

    /// <summary>
    /// Sets a specific dimension value.
    /// </summary>
    /// <param name="index">The dimension index to set.</param>
    /// <param name="value">The value to assign to the dimension.</param>
    public void SetDimension(int index, float value)
    {
        if (components == null)
        {
            Debug.LogError("Components array is not initialized. Call Initialize() first.");
            return;
        }

        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= components.Length)
        {
            Debug.LogError($"Index out of range: Dimension {index} is beyond the initialized size.");
            return;
        }

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
    /// <param name="index">The dimension index to retrieve.</param>
    /// <returns>The value of the specified dimension.</returns>
    public float GetDimension(int index)
    {
        if (components == null)
        {
            Debug.LogError("Components array is not initialized. Call Initialize() first.");
            return 0;
        }

        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= components.Length)
        {
            Debug.LogError($"Index out of range: Dimension {index} is beyond the initialized size.");
            return 0;
        }

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
    /// Normalizes the vector to have a magnitude of 1.
    /// </summary>
    public void Normalize()
    {
        if (components == null || components.Length == 0)
        {
            Debug.LogError("Components array is not initialized. Call Initialize() first.");
            return;
        }

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

    /// <summary>
    /// Computes the dot product with another HyperDimensionalVector.
    /// </summary>
    /// <param name="other">The other vector to compute the dot product with.</param>
    /// <returns>The dot product value.</returns>
    public float DotProduct(HyperDimensionalVector other)
    {
        if (this.Dimensions != other.Dimensions)
        {
            Debug.LogError("Dimension mismatch: Both vectors must have the same number of dimensions.");
            return 0;
        }

        float dot = 0;
        for (int i = 0; i < Dimensions; i++)
        {
            dot += this.GetDimension(i) * other.GetDimension(i);
        }
        return dot;
    }
}
