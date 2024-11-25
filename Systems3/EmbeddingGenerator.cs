public class EmbeddingGenerator
{
    private float[,] embeddingMatrix;
    private int vocabSize;
    private int embeddingSize;

    public EmbeddingGenerator(int vocabSize, int embeddingSize)
    {
        this.vocabSize = vocabSize;
        this.embeddingSize = embeddingSize;
        InitializeMatrix();
    }

    private void InitializeMatrix()
    {
        embeddingMatrix = new float[vocabSize, embeddingSize];
        System.Random random = new System.Random();
        for (int i = 0; i < vocabSize; i++)
        {
            for (int j = 0; j < embeddingSize; j++)
            {
                embeddingMatrix[i, j] = (float)random.NextDouble() * 2 - 1; // Random values between -1 and 1
            }
        }
    }

    public float[] GenerateEmbedding(int token)
    {
        float[] embedding = new float[embeddingSize];
        for (int i = 0; i < embeddingSize; i++)
        {
            embedding[i] = embeddingMatrix[token, i];
        }
        return embedding;
    }
}
