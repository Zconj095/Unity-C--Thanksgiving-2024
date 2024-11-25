using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class AugmentedRetrievalGenerator
{
    private HebbianEnhancedRecollection enhancedRecollection;
    private OutputGenerator outputGenerator;

    public AugmentedRetrievalGenerator(HebbianEnhancedRecollection enhancedRecollection, OutputGenerator generator)
    {
        this.enhancedRecollection = enhancedRecollection;
        outputGenerator = generator;
    }

    public string GenerateResponse(float[] inputEmbedding, int topN = 5)
    {
        // Retrieve relevant memories
        var memories = enhancedRecollection.Recollect(inputEmbedding, topN);

        // Convert memories to context embeddings
        var contextEmbeddings = memories.Select(m => m.Embedding).ToList();

        // Generate output
        return outputGenerator.GenerateOutput(inputEmbedding, contextEmbeddings);
    }
}
