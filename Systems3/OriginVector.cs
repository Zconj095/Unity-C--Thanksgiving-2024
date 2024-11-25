using System.Collections.Generic;
using System.Linq;
using System;

public class OriginVector
{
    public float[] Position { get; private set; } // 3D Space-Time Coordinates (x, y, z, t)
    public CrossDimensionalVector CrossVector { get; private set; } // Cross-Dimensional Vector

    public OriginVector(float[] position, int dimensions)
    {
        Position = position;
        CrossVector = new CrossDimensionalVector(dimensions);
    }

    public float ComputeDistance(OriginVector other)
    {
        // Euclidean distance between origin points in 4D space-time
        float distance = 0;
        for (int i = 0; i < Position.Length; i++)
        {
            distance += (Position[i] - other.Position[i]) * (Position[i] - other.Position[i]);
        }
        return (float)Math.Sqrt(distance);
    }

    public float[] SynchronizeFeedback(OriginVector other, Func<float, float, float> feedbackFunction)
    {
        float[] feedback = new float[CrossVector.HyperVector.Length];
        for (int i = 0; i < feedback.Length; i++)
        {
            feedback[i] = feedbackFunction(
                CrossVector.HyperVector[i],
                other.CrossVector.HyperVector[i]
            );
        }
        return feedback;
    }
}
