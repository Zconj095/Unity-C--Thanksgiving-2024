using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class FileScanner
{
    private FileParserManager parserManager;
    private EmbeddingGenerator embeddingGenerator;
    private VectorDatabase2 vectorDatabase;

    public FileScanner(FileParserManager parserManager, EmbeddingGenerator embeddingGenerator, VectorDatabase2 vectorDatabase)
    {
        this.parserManager = parserManager;
        this.embeddingGenerator = embeddingGenerator;
        this.vectorDatabase = vectorDatabase;
    }

    public void ScanAndAbsorb(string filePath)
    {
        // Parse the file
        string content = parserManager.ParseFile(filePath);

        // Generate embeddings for the content
        float[] embedding = embeddingGenerator.GenerateEmbedding(content.GetHashCode());

        // Store the embedding in the vector database
        vectorDatabase.AddVector(filePath.GetHashCode(), embedding);

        Console.WriteLine($"File {filePath} absorbed into memory.");
    }
}
