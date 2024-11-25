using System;
using System.Collections.Generic;
using UnityEngine;

public class CognizantCortexReaderGroversAlgorithm
{
    private int searchSpaceSize;
    private List<int> markedStates;

    public CognizantCortexReaderGroversAlgorithm(int size)
    {
        searchSpaceSize = size;
        markedStates = new List<int>();
    }

    public void MarkState(int state)
    {
        if (!markedStates.Contains(state))
        {
            markedStates.Add(state);
        }
    }

    public int PerformSearch()
    {
        if (markedStates.Count == 0)
        {
            Debug.LogWarning("No marked states to search for.");
            return -1;
        }

        int iterations = Mathf.CeilToInt(Mathf.Sqrt(searchSpaceSize));
        int bestState = -1;
        float bestAmplitude = float.MinValue;

        // Simulate quantum state amplitudes
        float[] amplitudes = new float[searchSpaceSize];
        for (int i = 0; i < searchSpaceSize; i++)
        {
            amplitudes[i] = 1f / Mathf.Sqrt(searchSpaceSize);
        }

        for (int i = 0; i < iterations; i++)
        {
            // Amplify marked states
            foreach (int marked in markedStates)
            {
                amplitudes[marked] *= -1f;
            }

            // Diffuse across all states
            float averageAmplitude = 0f;
            foreach (float amp in amplitudes)
            {
                averageAmplitude += amp;
            }
            averageAmplitude /= searchSpaceSize;

            for (int j = 0; j < amplitudes.Length; j++)
            {
                amplitudes[j] = 2f * averageAmplitude - amplitudes[j];
            }
        }

        // Find the state with the highest amplitude
        for (int i = 0; i < amplitudes.Length; i++)
        {
            if (amplitudes[i] > bestAmplitude)
            {
                bestAmplitude = amplitudes[i];
                bestState = i;
            }
        }

        return bestState;
    }
}
