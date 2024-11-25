using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class CrossVectorAllocator
{
    private List<VectorState> layers;

    public CrossVectorAllocator()
    {
        layers = new List<VectorState>();
    }

    public void AddLayer(VectorState state)
    {
        layers.Add(state);
    }

    public VectorState GetLayer(int index)
    {
        return layers[index];
    }

    public VectorState AllocateCrossVectors(Func<VectorState, VectorState, VectorState> mergeFunc)
    {
        VectorState crossState = layers[0];
        for (int i = 1; i < layers.Count; i++)
        {
            crossState = mergeFunc(crossState, layers[i]);
        }
        return crossState;
    }
}
