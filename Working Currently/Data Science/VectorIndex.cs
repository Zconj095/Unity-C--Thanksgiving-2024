using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class VectorIndex : MonoBehaviour
{
    // The list of data points in the index
    private List<VectorDataPoint> dataPoints;

    void Start()
    {
        // Initialize the vector index with example data
        dataPoints = new List<VectorDataPoint>
        {
            new VectorDataPoint(new float[] { 1.0f, 2.0f }),
            new VectorDataPoint(new float[] { 1.5f, 1.8f }),
            new VectorDataPoint(new float[] { 5.0f, 8.0f }),
            new VectorDataPoint(new float[] { 8.0f, 8.0f }),
            new VectorDataPoint(new float[] { 1.0f, 0.6f }),
            new VectorDataPoint(new float[] { 9.0f, 11.0f }),
            new VectorDataPoint(new float[] { 8.0f, 4.0f }),
            new VectorDataPoint(new float[] { 6.0f, 4.0f }),
            new VectorDataPoint(new float[] { 3.0f, 7.0f }),
            new VectorDataPoint(new float[] { 3.5f, 6.5f })
        };

        // Query for nearest neighbor
        VectorDataPoint queryPoint = new VectorDataPoint(new float[] { 5.0f, 5.0f });
        var nearestNeighbor = FindNearestNeighbor(queryPoint);

        // Output the nearest neighbor to Unity's console
        Debug.Log($"Nearest Neighbor: {nearestNeighbor}");
    }

    /// <summary>
    /// Finds the nearest neighbor to the query point using Euclidean distance.
    /// </summary>
    /// <param name="queryPoint">The query vector.</param>
    /// <returns>The nearest neighbor as a <see cref="VectorDataPoint"/>.</returns>
    public VectorDataPoint FindNearestNeighbor(VectorDataPoint queryPoint)
    {
        return dataPoints.OrderBy(point => CalculateEuclideanDistance(queryPoint, point))
                         .FirstOrDefault();
    }

    /// <summary>
    /// Finds the indices of the nearest neighbors to the query point.
    /// </summary>
    /// <param name="queryPoint">The query vector.</param>
    /// <param name="numNeighbors">The number of nearest neighbors to find.</param>
    /// <returns>A list of the nearest neighbors as <see cref="VectorDataPoint"/>.</returns>
    public List<VectorDataPoint> FindNearestNeighbors(VectorDataPoint queryPoint, int numNeighbors)
    {
        return dataPoints.OrderBy(point => CalculateEuclideanDistance(queryPoint, point))
                         .Take(numNeighbors)
                         .ToList();
    }

    /// <summary>
    /// Calculates the Euclidean distance between two data points.
    /// </summary>
    /// <param name="point1">The first vector.</param>
    /// <param name="point2">The second vector.</param>
    /// <returns>The Euclidean distance.</returns>
    private float CalculateEuclideanDistance(VectorDataPoint point1, VectorDataPoint point2)
    {
        float sum = 0;
        for (int i = 0; i < point1.Features.Length; i++)
        {
            sum += Mathf.Pow(point1.Features[i] - point2.Features[i], 2);
        }

        return Mathf.Sqrt(sum);
    }
}
