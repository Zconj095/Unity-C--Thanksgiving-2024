using System.Collections.Generic;
using System.Linq;
using System;
public class OriginLocation
{
    public float[] Coordinates { get; private set; } // Space-time coordinates (x, y, z, t)
    public float[] StateVector { get; private set; } // State vector associated with the origin
    public float[] Feedback { get; private set; }    // Feedback for adjustments

    public OriginLocation(float[] coordinates, int vectorDimensions)
    {
        Coordinates = coordinates;
        StateVector = new float[vectorDimensions];
        Feedback = new float[vectorDimensions];
        InitializeState();
    }

    private void InitializeState()
    {
        Random random = new Random();
        for (int i = 0; i < StateVector.Length; i++)
        {
            StateVector[i] = (float)(random.NextDouble() * 2 - 1); // Initialize state between -1 and 1
        }
    }

    public void ApplyFeedback(float[] newFeedback, float learningRate)
    {
        for (int i = 0; i < StateVector.Length; i++)
        {
            Feedback[i] = newFeedback[i];
            StateVector[i] += learningRate * Feedback[i]; // Adjust state based on feedback
        }
    }

    public float ComputeDistance(OriginLocation other)
    {
        float distance = 0;
        for (int i = 0; i < Coordinates.Length; i++)
        {
            distance += (Coordinates[i] - other.Coordinates[i]) * (Coordinates[i] - other.Coordinates[i]);
        }
        return (float)Math.Sqrt(distance);
    }
}
