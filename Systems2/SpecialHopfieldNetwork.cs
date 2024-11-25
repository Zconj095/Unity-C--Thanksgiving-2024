using System;
using System.Collections.Generic;
using UnityEngine;

public class SpecialHopfieldNetwork
{
    private int size;
    private float[,] weights;

    public SpecialHopfieldNetwork(int size)
    {
        this.size = size;
        weights = new float[size, size];
    }

    // Train the Hopfield network with a set of patterns
    public void Train(List<int[]> patterns)
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

    // Predict the output for a given input
    public int[] Predict(int[] inputPattern, int maxIterations = 10)
    {
        int[] state = (int[])inputPattern.Clone();

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            int[] previousState = (int[])state.Clone();

            for (int i = 0; i < size; i++)
            {
                float sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += weights[i, j] * state[j];
                }

                state[i] = sum >= 0 ? 1 : -1;
            }

            // Stop if the state does not change
            if (AreArraysEqual(state, previousState))
                break;
        }

        return state;
    }

    private bool AreArraysEqual(int[] a, int[] b)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}
