using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MemoryRetrievalSystem
{
    private SynergyEngine synergyEngine = new SynergyEngine();

    public List<(int dbIndex, int vectorId, float score)> RetrieveRelevantVectors(
        float[] query,
        MultiDatabaseManager dbManager,
        int topN = 5)
    {
        return synergyEngine.FindSimilarVectors(query, dbManager)
                            .Take(topN)
                            .ToList();
    }
}
