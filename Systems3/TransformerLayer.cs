using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class TransformerLayer
{
    public Tuple<float[], float[]> Forward(Tuple<float[], float[]> input, Func<float, float, float> transformationFunc)
    {
        float[] query = input.Item1;
        float[] key = input.Item2;

        float[] transformed = new float[query.Length];
        for (int i = 0; i < query.Length; i++)
        {
            transformed[i] = transformationFunc(query[i], key[i]);
        }

        return new Tuple<float[], float[]>(query, transformed);
    }
}
