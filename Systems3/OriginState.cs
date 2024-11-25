using System.Collections.Generic;
using System.Linq;
using System;

public class OriginState
{
    public float[] Position { get; private set; } // x, y, z, t
    public MultiStateVector StateVector { get; private set; }

    public OriginState(float[] position, int vectorDimensions)
    {
        Position = position;
        StateVector = new MultiStateVector(vectorDimensions);
    }

    public void UpdateOrigin(float[] newPosition, float[] feedback, float learningRate)
    {
        Position = newPosition;
        StateVector.Optimize(feedback, learningRate);
    }

    public float ComputeDistance(OriginState other)
    {
        float distance = 0;
        for (int i = 0; i < Position.Length; i++)
        {
            distance += (Position[i] - other.Position[i]) * (Position[i] - other.Position[i]);
        }
        return (float)Math.Sqrt(distance);
    }
}
