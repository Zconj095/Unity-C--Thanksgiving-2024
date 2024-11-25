using System.Collections.Generic;
using System.Linq;
using System;
public class HyperState
{
    public float[] Vector { get; private set; }

    public HyperState(int dimensions)
    {
        Vector = new float[dimensions];
        Randomize();
    }

    public void Randomize()
    {
        Random random = new Random();
        for (int i = 0; i < Vector.Length; i++)
        {
            Vector[i] = (float)(random.NextDouble() * 2 - 1); // Random values between -1 and 1
        }
    }

    public HyperState Bind(HyperState other)
    {
        float[] result = new float[Vector.Length];
        for (int i = 0; i < Vector.Length; i++)
        {
            result[i] = Vector[i] * other.Vector[i]; // Element-wise multiplication
        }
        return new HyperState(result);
    }

    public HyperState Superpose(HyperState other)
    {
        float[] result = new float[Vector.Length];
        for (int i = 0; i < Vector.Length; i++)
        {
            result[i] = Vector[i] + other.Vector[i]; // Element-wise addition
        }
        return new HyperState(result);
    }

    // Make this constructor public to allow external use
    public HyperState(float[] vector)
    {
        Vector = vector;
    }
}
