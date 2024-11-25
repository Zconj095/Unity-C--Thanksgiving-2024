using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class MultiLayeredEmbedding
{
    public float[] BaseEmbedding { get; private set; }
    public List<float[]> Layers { get; private set; }

    public MultiLayeredEmbedding(float[] baseEmbedding)
    {
        BaseEmbedding = baseEmbedding;
        Layers = new List<float[]>();
    }

    public void AddLayer(float[] layer)
    {
        if (layer.Length != BaseEmbedding.Length)
        {
            throw new Exception("Layer dimensions must match the base embedding dimensions.");
        }
        Layers.Add(layer);
    }

    public float[] GetHighDimensionalRepresentation()
    {
        float[] combined = new float[BaseEmbedding.Length];
        Array.Copy(BaseEmbedding, combined, BaseEmbedding.Length);

        foreach (var layer in Layers)
        {
            for (int i = 0; i < combined.Length; i++)
            {
                combined[i] += layer[i]; // Combine layers additively for now
            }
        }

        return combined;
    }
}
