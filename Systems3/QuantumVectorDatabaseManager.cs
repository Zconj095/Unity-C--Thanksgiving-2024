using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuantumVectorDatabase2Manager
{
    private List<VectorDatabase2> databases = new List<VectorDatabase2>();

    public void AddDatabase(VectorDatabase2 db)
    {
        databases.Add(db);
    }

    public (int dbIndex, int vectorIndex) QuantumSearch(float[] queryVector, float similarityThreshold = 0.9f, int iterations = 3)
    {
        GroverSearch groverSearch = new GroverSearch();

        for (int i = 0; i < databases.Count; i++)
        {
            VectorDatabase2 db = databases[i];
            float[][] databaseVectors = db.GetAllVectors().Values.ToArray();

            int vectorIndex = groverSearch.PerformSearch(databaseVectors, queryVector, similarityThreshold, iterations);
            if (vectorIndex >= 0)
            {
                return (i, vectorIndex); // Return database and vector index
            }
        }

        return (-1, -1); // No match found
    }
}
