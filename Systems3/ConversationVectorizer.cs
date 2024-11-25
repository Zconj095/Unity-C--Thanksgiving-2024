using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class ConversationVectorizer
{
    private EmbeddingGenerator embeddingGenerator;

    public ConversationVectorizer(EmbeddingGenerator generator)
    {
        embeddingGenerator = generator;
    }

    public float[] VectorizeMessage(ConversationMessage message)
    {
        return embeddingGenerator.GenerateEmbedding(message.Message.GetHashCode());
    }

    public List<float[]> VectorizeConversation(ConversationHistory conversation)
    {
        List<float[]> embeddings = new List<float[]>();
        foreach (var message in conversation.Messages)
        {
            embeddings.Add(VectorizeMessage(message));
        }
        return embeddings;
    }
}
