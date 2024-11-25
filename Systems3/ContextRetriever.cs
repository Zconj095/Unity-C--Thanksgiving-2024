using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ContextRetriever
{
    private MemoryRetrievalSystem memoryRetrievalSystem;
    private MultiDatabaseManager dbManager;

    public ContextRetriever(MemoryRetrievalSystem retrievalSystem, MultiDatabaseManager dbManager)
    {
        this.memoryRetrievalSystem = retrievalSystem;
        this.dbManager = dbManager;
    }

    public List<float[]> GetRelevantContext(float[] inputEmbedding, int topN = 3)
    {
        var results = memoryRetrievalSystem.RetrieveRelevantVectors(inputEmbedding, dbManager, topN);
        List<float[]> contextEmbeddings = new List<float[]>();

        foreach (var result in results)
        {
            var db = dbManager.GetDatabase(result.dbIndex);
            contextEmbeddings.Add(db.GetVector(result.vectorId));
        }

        return contextEmbeddings;
    }
}
