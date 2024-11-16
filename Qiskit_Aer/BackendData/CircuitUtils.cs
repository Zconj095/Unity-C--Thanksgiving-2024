using System;
using System.Collections.Generic;
using UnityEngine;
public static class CircuitUtils
{
    public static void AddFinalSaveOperation(List<string> circuits, string state)
    {
        for (int i = 0; i < circuits.Count; i++)
        {
            circuits[i] += $" | Save {state}";
            Debug.Log($"Added save operation to Circuit {i}: {state}");
        }
    }
}
