using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OutputGenerator
{
    private AttentionMechanism attentionMechanism;
    private DenseLayer denseLayer;

    public OutputGenerator(AttentionMechanism attentionMechanism, DenseLayer denseLayer)
    {
        this.attentionMechanism = attentionMechanism;
        this.denseLayer = denseLayer;
    }

    public string GenerateOutput(float[] inputEmbedding, List<float[]> contextEmbeddings)
    {
        // Compute attention over context embeddings
        float[] combinedContext = new float[inputEmbedding.Length];
        foreach (var context in contextEmbeddings)
        {
            var attentionResult = attentionMechanism.ComputeAttention(inputEmbedding, new[] { context }, new[] { context });
            for (int i = 0; i < combinedContext.Length; i++)
            {
                combinedContext[i] += attentionResult[i];
            }
        }

        // Prepare weights and biases for the dense layer
        var weights = denseLayer.GetWeights();
        var biases = denseLayer.GetBiases();

        // Pass through dense layer for final output embedding
        float[] outputEmbedding = denseLayer.Forward(combinedContext, weights, biases);

        // Convert embedding to text output (detokenization is a placeholder here)
        return Detokenize(outputEmbedding);
    }

    private string Detokenize(float[] embedding)
    {
        // This is a placeholder for converting embeddings back to text.
        return $"Generated output based on embedding: [{string.Join(", ", embedding)}]";
    }
}
