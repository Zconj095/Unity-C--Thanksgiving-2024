using System.Collections.Generic;
using UnityEngine;

public class HimmawariZScores : MonoBehaviour
{
    public List<float[]> dataPoints; // Multi-dimensional points
    private float[] means;
    private float[] stdDevs;
    private float[] weights;

    void Start()
    {
        CalculateMeansAndStdDevs();
        CalculateWeights();
        CalculateZScores();
    }

    void CalculateMeansAndStdDevs()
    {
        int dimensions = dataPoints[0].Length;
        means = new float[dimensions];
        stdDevs = new float[dimensions];

        for (int d = 0; d < dimensions; d++)
        {
            float sum = 0f, sumSq = 0f;
            foreach (var point in dataPoints)
            {
                sum += point[d];
                sumSq += point[d] * point[d];
            }
            means[d] = sum / dataPoints.Count;
            stdDevs[d] = Mathf.Sqrt(sumSq / dataPoints.Count - means[d] * means[d]);
        }
    }

    void CalculateWeights()
    {
        int dimensions = dataPoints[0].Length;
        weights = new float[dimensions];

        for (int d = 0; d < dimensions; d++)
        {
            float influence = 0f;
            foreach (var point in dataPoints)
            {
                influence += Mathf.Abs(point[d] - means[d]) / stdDevs[d];
            }
            weights[d] = influence;
        }

        float totalWeight = 0f;
        foreach (var weight in weights)
        {
            totalWeight += weight;
        }
        for (int d = 0; d < dimensions; d++)
        {
            weights[d] /= totalWeight;
        }
    }

    void CalculateZScores()
    {
        foreach (var point in dataPoints)
        {
            float relevanceScore = 0f;

            for (int d = 0; d < means.Length; d++)
            {
                float zScore = weights[d] * (point[d] - means[d]) / stdDevs[d];
                relevanceScore += zScore;
            }

            Debug.Log($"Relevance Score: {relevanceScore}");
        }
    }
}
