using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;
public class QuantumLogic
{
    public static float[] Interfere(float[] stateA, float[] stateB)
    {
        // Interference by averaging (quantum-like behavior)
        float[] result = new float[stateA.Length];
        for (int i = 0; i < stateA.Length; i++)
        {
            result[i] = (stateA[i] + stateB[i]) / 2;
        }
        return result;
    }

    public static float Measure(float[] state)
    {
        // Quantum measurement: Return the norm (probability amplitude)
        return (float)Math.Sqrt(state.Sum(x => x * x));
    }
}
