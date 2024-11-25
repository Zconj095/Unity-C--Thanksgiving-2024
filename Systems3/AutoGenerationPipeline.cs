using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class AutoGenerationPipeline
{
    private MemoryRetrievalSystem2 memoryRetrievalSystem;
    private OutputGenerator outputGenerator;

    public AutoGenerationPipeline(MemoryRetrievalSystem2 memoryRetrievalSystem, OutputGenerator outputGenerator)
    {
        this.memoryRetrievalSystem = memoryRetrievalSystem;
        this.outputGenerator = outputGenerator;
    }

    public string Generate(string userInput, float[] inputEmbedding)
    {
        // Step 1: Retrieve relevant memories
        var relevantMemories = memoryRetrievalSystem.RetrieveMemory(inputEmbedding);

        // Step 2: Convert retrieved memories into context embeddings
        var contextEmbeddings = relevantMemories.Select(m => m.Embedding).ToList();

        // Step 3: Generate output using context and input
        return outputGenerator.GenerateOutput(inputEmbedding, contextEmbeddings);
    }
}
