using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputProcessor
{
    private Tokenizer tokenizer;
    private EmbeddingGenerator embeddingGenerator;

    public InputProcessor(Tokenizer tokenizer, EmbeddingGenerator embeddingGenerator)
    {
        this.tokenizer = tokenizer;
        this.embeddingGenerator = embeddingGenerator;
    }

    public float[] ProcessInput(string input)
    {
        int[] tokens = tokenizer.Tokenize(input);
        List<float> embedding = new List<float>();

        foreach (int token in tokens)
        {
            embedding.AddRange(embeddingGenerator.GenerateEmbedding(token));
        }

        return embedding.ToArray();
    }
}
