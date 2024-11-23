using UnityEngine;

public class HopfieldNetwork
{
    private int size;
    private float[,] weights;

    public HopfieldNetwork(int size)
    {
        this.size = size;
        weights = new float[size, size];
    }

    public void Train(float[][] patterns)
    {
        foreach (var pattern in patterns)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        weights[i, j] += pattern[i] * pattern[j];
                    }
                }
            }
        }
    }

    public float[] Recall(float[] input, int steps = 5)
    {
        float[] state = (float[])input.Clone();

        for (int step = 0; step < steps; step++)
        {
            for (int i = 0; i < size; i++)
            {
                float sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += weights[i, j] * state[j];
                }
                state[i] = sum >= 0 ? 1 : -1; // Threshold function
            }
        }

        return state;
    }
}
