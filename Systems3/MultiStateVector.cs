using System.Collections.Generic;
using System.Linq;
using System;
public class MultiStateVector
{
    public float[] PrimaryState { get; private set; }
    public float[] SecondaryState { get; private set; }

    public MultiStateVector(int dimensions)
    {
        PrimaryState = new float[dimensions];
        SecondaryState = new float[dimensions];
        InitializeStates();
    }

    private void InitializeStates()
    {
        Random random = new Random();
        for (int i = 0; i < PrimaryState.Length; i++)
        {
            PrimaryState[i] = (float)(random.NextDouble() * 2 - 1); // Initialize between -1 and 1
            SecondaryState[i] = (float)(random.NextDouble() * 2 - 1);
        }
    }

    public void Optimize(float[] feedback, float learningRate)
    {
        PrimaryState = HyperstateOptimizer.RefineState(PrimaryState, feedback, learningRate);
        SecondaryState = HyperstateOptimizer.RefineState(SecondaryState, feedback, learningRate);
    }
}
