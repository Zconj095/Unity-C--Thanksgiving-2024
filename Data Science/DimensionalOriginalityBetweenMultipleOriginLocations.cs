using UnityEngine;

public class DimensionalOriginalityBetweenMultipleOriginLocations : MonoBehaviour
{
    // Array to store multiple origin points, configurable in the Unity Inspector
    [SerializeField] private Transform[] originPoints;

    // Number of origins to compare (used for limiting comparisons dynamically, if needed)
    [SerializeField] private int comparisonCount = 3;

    // Stores the distances and angles between origin points
    private float[,] distances;
    private float[,] angles;

    // Optional custom transformation to simulate "dimensional" uniqueness
    [SerializeField] private Transform customTransform;

    void Start()
    {
        // Initialize the arrays for storing distances and angles
        int originCount = originPoints.Length;
        distances = new float[originCount, originCount];
        angles = new float[originCount, originCount];

        // Perform initial comparisons between origins
        CompareOrigins();
    }

    void Update()
    {
        // Example: Update and recalculate when a key is pressed
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            // Example of moving the first origin
            originPoints[0].position += new Vector3(1, 0, 0);
            CompareOrigins(); // Recalculate distances and angles
        }
    }

    /// <summary>
    /// Compares the distances and angles between all pairs of origin points.
    /// </summary>
    private void CompareOrigins()
    {
        int originCount = originPoints.Length;

        for (int i = 0; i < originCount; i++)
        {
            for (int j = i + 1; j < originCount; j++)
            {
                // Calculate the distance between two origins
                distances[i, j] = Vector3.Distance(originPoints[i].position, originPoints[j].position);

                // Calculate the angle between two origins relative to the transform's position
                Vector3 directionI = originPoints[i].position - transform.position;
                Vector3 directionJ = originPoints[j].position - transform.position;
                angles[i, j] = Vector3.Angle(directionI, directionJ);

                // Log the computed distance and angle
                Debug.Log($"Distance between Origin {i} and Origin {j}: {distances[i, j]}");
                Debug.Log($"Angle between Origin {i} and Origin {j}: {angles[i, j]}");
            }
        }
    }

    /// <summary>
    /// Updates a specific origin point and recalculates comparisons.
    /// </summary>
    /// <param name="index">Index of the origin point to update.</param>
    /// <param name="newOrigin">The new origin Transform.</param>
    public void UpdateOrigin(int index, Transform newOrigin)
    {
        if (index >= 0 && index < originPoints.Length)
        {
            originPoints[index] = newOrigin;
            CompareOrigins(); // Recalculate after updating the origin
        }
        else
        {
            Debug.LogWarning("Invalid origin index provided.");
        }
    }

    /// <summary>
    /// Applies a custom transformation to all origins to simulate a new dimensional space.
    /// </summary>
    private void ApplyDimensionalTransformation()
    {
        if (customTransform != null)
        {
            for (int i = 0; i < originPoints.Length; i++)
            {
                // Apply the custom transformation to each origin
                originPoints[i].position = customTransform.TransformPoint(originPoints[i].position);
            }
        }
        else
        {
            Debug.LogWarning("Custom transform is not set.");
        }
    }

    /// <summary>
    /// Visualizes origins and their relationships in the Scene view using Gizmos.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (originPoints != null && originPoints.Length > 0)
        {
            Gizmos.color = Color.red;

            // Draw spheres at each origin point
            foreach (Transform origin in originPoints)
            {
                Gizmos.DrawSphere(origin.position, 0.1f);
            }

            // Draw lines between origins
            for (int i = 0; i < originPoints.Length; i++)
            {
                for (int j = i + 1; j < originPoints.Length; j++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(originPoints[i].position, originPoints[j].position);
                }
            }
        }
    }
}
