using UnityEngine;

public class CosineSimilarity : MonoBehaviour
{
    // Public vectors for comparison
    [SerializeField] private Vector3 vectorA = new Vector3(1, 0, 0); // Example vector A
    [SerializeField] private Vector3 vectorB = new Vector3(1, 1, 0); // Example vector B

    void Start()
    {
        // Compute cosine similarity
        float similarity = CalculateCosineSimilarity(vectorA, vectorB);

        if (!float.IsNaN(similarity))
        {
            Debug.Log($"Cosine Similarity between {vectorA} and {vectorB}: {similarity:F4}");
        }
        else
        {
            Debug.LogError("Cosine Similarity calculation resulted in NaN. Check for zero-length vectors.");
        }
    }

    /// <summary>
    /// Calculates the cosine similarity between two vectors.
    /// </summary>
    /// <param name="A">First vector.</param>
    /// <param name="B">Second vector.</param>
    /// <returns>Cosine similarity value between -1 and 1. Returns NaN if one of the vectors is zero.</returns>
    private float CalculateCosineSimilarity(Vector3 A, Vector3 B)
    {
        // Dot product of A and B
        float dotProduct = Vector3.Dot(A, B);

        // Magnitudes of A and B
        float magnitudeA = A.magnitude;
        float magnitudeB = B.magnitude;

        // Check for zero-length vectors to avoid division by zero
        if (magnitudeA == 0f || magnitudeB == 0f)
        {
            Debug.LogWarning("One or both vectors have zero magnitude. Cannot compute cosine similarity.");
            return float.NaN; // Indicate an invalid result
        }

        // Cosine similarity formula
        float similarity = dotProduct / (magnitudeA * magnitudeB);

        return similarity;
    }
}
