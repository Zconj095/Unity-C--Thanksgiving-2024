using UnityEngine;
using System.Linq;

public class CalculativeMeanAdjustment : MonoBehaviour
{
    [SerializeField] private float[] dataPoints; // Data points to adjust
    [SerializeField] private float targetMean;   // The target mean value to achieve

    private void Start()
    {
        if (dataPoints != null && dataPoints.Length > 0)
        {
            // Calculate the current mean
            float currentMean = CalculateMean(dataPoints);

            // Adjust the data points to match the target mean
            AdjustDataPointsToMean(ref dataPoints, currentMean, targetMean);

            // Log the results
            Debug.Log($"Current Mean: {currentMean}");
            Debug.Log($"Adjusted Mean: {targetMean}");
            Debug.Log($"Adjusted Data: {string.Join(", ", dataPoints)}");
        }
        else
        {
            Debug.LogError("No data points provided!");
        }
    }

    // Function to calculate the mean of an array of data points
    private float CalculateMean(float[] values)
    {
        return values.Average(); // Use LINQ to calculate the mean
    }

    // Function to adjust the data points to the target mean
    private void AdjustDataPointsToMean(ref float[] values, float currentMean, float targetMean)
    {
        float adjustment = targetMean - currentMean;

        // Adjust all values by the difference between the target and current mean
        for (int i = 0; i < values.Length; i++)
        {
            values[i] += adjustment;
        }
    }
}
