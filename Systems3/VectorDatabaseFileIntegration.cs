using System;

public class VectorDatabaseFileIntegration
{
    private FileParserManager parserManager;
    private VectorDatabase2 vectorDatabase;
    private EmbeddingGenerator embeddingGenerator;

    public VectorDatabaseFileIntegration(FileParserManager parserManager, VectorDatabase2 vectorDatabase, EmbeddingGenerator embeddingGenerator)
    {
        this.parserManager = parserManager;
        this.vectorDatabase = vectorDatabase;
        this.embeddingGenerator = embeddingGenerator;
    }

    public void AddFileToDatabase(string filePath)
    {
        // Step 1: Parse file
        string content = parserManager.ParseFile(filePath);

        // Step 2: Generate embedding
        int contentHash = content.GetHashCode(); // Generate a consistent integer hash for content
        float[] embedding = embeddingGenerator.GenerateEmbedding(contentHash);

        // Step 3: Store embedding and metadata
        vectorDatabase.AddVector(contentHash, embedding); // Use contentHash (int) as key
        Console.WriteLine($"File {filePath} added to the database.");
    }
}
