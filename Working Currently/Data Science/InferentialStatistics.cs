using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InferentialStatistics : MonoBehaviour
{
    [Header("Data Set")]
    public List<float> dataSet = new List<float>();  // Store numerical data points

    [Header("Predicted Value")]
    public float predictedValue;  // Predicted value based on the dataset

    private void Start()
    {
        // Example data for demonstration (can be dynamically updated)
        dataSet.AddRange(new float[] { 10, 12, 14, 11, 13, 15, 10, 14, 12, 13 });

        // Calculate statistics
        CalculateStatistics();

        // Make a prediction
        MakePrediction();

        // Make a prediction with a confidence interval
        MakePredictionWithConfidenceInterval(0.95f);  // 95% confidence level
    }

    /// <summary>
    /// Calculates and logs the mean and standard deviation of the dataset.
    /// </summary>
    public void CalculateStatistics()
    {
        if (!dataSet.Any())
        {
            Debug.LogWarning("Dataset is empty. Cannot calculate statistics.");
            return;
        }

        float mean = CalculateMean(dataSet);
        float standardDeviation = CalculateStandardDeviation(dataSet, mean);

        Debug.Log($"Mean: {mean:F2}");
        Debug.Log($"Standard Deviation: {standardDeviation:F2}");
    }

    /// <summary>
    /// Calculates the mean of the dataset.
    /// </summary>
    /// <param name="data">Dataset.</param>
    /// <returns>Mean value.</returns>
    public float CalculateMean(List<float> data)
    {
        return data.Average(); // Using LINQ for simplicity
    }

    /// <summary>
    /// Calculates the standard deviation of the dataset.
    /// </summary>
    /// <param name="data">Dataset.</param>
    /// <param name="mean">Mean value of the dataset.</param>
    /// <returns>Standard deviation.</returns>
    public float CalculateStandardDeviation(List<float> data, float mean)
    {
        float sumOfSquares = data.Sum(d => Mathf.Pow(d - mean, 2));
        return Mathf.Sqrt(sumOfSquares / data.Count);
    }

    /// <summary>
    /// Makes a prediction using a basic model with random deviation.
    /// </summary>
    public void MakePrediction()
    {
        if (dataSet.Count < 2)
        {
            Debug.LogWarning("Not enough data to make a prediction.");
            return;
        }

        float mean = CalculateMean(dataSet);
        float stdDev = CalculateStandardDeviation(dataSet, mean);

        // Generate a prediction with a small random deviation
        predictedValue = mean + Random.Range(-stdDev, stdDev);

        Debug.Log($"Predicted Value: {predictedValue:F2}");
    }

    /// <summary>
    /// Makes a prediction with a confidence interval.
    /// </summary>
    /// <param name="confidenceLevel">Confidence level (e.g., 0.95 for 95%).</param>
    public void MakePredictionWithConfidenceInterval(float confidenceLevel = 0.95f)
    {
        if (dataSet.Count < 2)
        {
            Debug.LogWarning("Not enough data to calculate a confidence interval.");
            return;
        }

        float mean = CalculateMean(dataSet);
        float stdDev = CalculateStandardDeviation(dataSet, mean);
        float zScore = GetZScoreForConfidenceLevel(confidenceLevel);

        // Calculate margin of error
        float marginOfError = zScore * (stdDev / Mathf.Sqrt(dataSet.Count));

        // Confidence interval bounds
        float lowerBound = mean - marginOfError;
        float upperBound = mean + marginOfError;

        Debug.Log($"Predicted Range with {confidenceLevel * 100}% confidence: [{lowerBound:F2}, {upperBound:F2}]");
    }

    /// <summary>
    /// Gets the Z-score for a given confidence level.
    /// </summary>
    /// <param name="confidenceLevel">Confidence level (e.g., 0.95 for 95%).</param>
    /// <returns>Z-score corresponding to the confidence level.</returns>
    private float GetZScoreForConfidenceLevel(float confidenceLevel)
    {
        // Common Z-scores for standard confidence levels
        return confidenceLevel switch
        {
            0.95f => 1.96f, // 95% confidence
            0.90f => 1.64f, // 90% confidence
            _ => 1.96f      // Default to 95% confidence
        };
    }
}
