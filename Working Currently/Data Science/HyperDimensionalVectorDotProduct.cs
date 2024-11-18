using UnityEngine;

public class HyperDimensionalVectorDotProduct : MonoBehaviour
{
    [Header("HyperDimensional Vectors")]
    [SerializeField] private HyperDimensionalVector hv1;  // First vector
    [SerializeField] private HyperDimensionalVector hv2;  // Second vector

    private void Start()
    {
        // Check if both HyperDimensionalVector references are assigned
        if (hv1 == null || hv2 == null)
        {
            Debug.LogError("HyperDimensionalVectors are not assigned for dot product calculation.");
            return;
        }

        // Validate that both vectors are initialized
        if (hv1.Components == null || hv2.Components == null)
        {
            Debug.LogError("One or both HyperDimensionalVectors are not initialized.");
            return;
        }

        // Perform the dot product calculation and log the result
        float dotProduct = CalculateDotProduct(hv1, hv2);
        Debug.Log($"Dot Product: {dotProduct:F4}");
    }

    /// <summary>
    /// Calculates the dot product between two HyperDimensionalVectors.
    /// </summary>
    private float CalculateDotProduct(HyperDimensionalVector vector1, HyperDimensionalVector vector2)
    {
        float dot = 0;

        // Assumes 4 dimensions per Vector4 component
        int limit = Mathf.Min(vector1.Components.Length, vector2.Components.Length) * 4;

        for (int i = 0; i < limit; i++)
        {
            dot += GetDimension(vector1, i) * GetDimension(vector2, i);
        }

        return dot;
    }

    /// <summary>
    /// Retrieves the value of a specific dimension from a HyperDimensionalVector.
    /// </summary>
    private float GetDimension(HyperDimensionalVector vector, int index)
    {
        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= vector.Components.Length)
            return 0;

        Vector4 vec = vector.Components[vectorIndex];

        return componentIndex switch
        {
            0 => vec.x,
            1 => vec.y,
            2 => vec.z,
            3 => vec.w,
            _ => 0
        };
    }
}
