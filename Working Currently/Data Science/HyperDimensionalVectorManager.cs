using UnityEngine;

public class HyperDimensionalVectorManager : MonoBehaviour
{
    [Header("HyperDimensional Vectors")]
    [SerializeField] private HyperDimensionalVector hv1;  // First vector
    [SerializeField] private HyperDimensionalVector hv2;  // Second vector

    [Header("Vector Configuration")]
    [SerializeField] private int vectorDimensions = 8;   // Number of dimensions for both vectors

    private void Awake()
    {
        // Dynamically add HyperDimensionalVector components if not already assigned
        if (hv1 == null)
        {
            hv1 = gameObject.AddComponent<HyperDimensionalVector>();
        }

        if (hv2 == null)
        {
            hv2 = gameObject.AddComponent<HyperDimensionalVector>();
        }

        // Initialize each HyperDimensionalVector with the specified number of dimensions
        hv1.Initialize(vectorDimensions);
        hv2.Initialize(vectorDimensions);
    }

    private void Start()
    {
        // Example setup: Set dimensions and normalize
        Debug.Log("Initializing HyperDimensional Vectors...");

        hv1.SetDimension(0, 1.0f);
        hv1.SetDimension(4, 0.5f);
        hv1.Normalize();

        hv2.SetDimension(0, 0.7f);
        hv2.SetDimension(4, 0.8f);

        // Calculate and display the dot product of hv1 and hv2
        float dotProduct = hv1.DotProduct(hv2);
        Debug.Log($"Dot Product of hv1 and hv2: {dotProduct:F4}");
    }
}
