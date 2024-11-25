using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class EmbeddingCompressor
{
    public byte[] CompressEmbedding(float[] embedding)
    {
        return embedding.Select(e => (byte)(e * 255)).ToArray();
    }

    public float[] DecompressEmbedding(byte[] compressedEmbedding)
    {
        return compressedEmbedding.Select(b => b / 255f).ToArray();
    }
}
