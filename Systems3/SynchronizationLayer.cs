using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SynchronizationLayer
{
    public void SynchronizeDatabases(MultiDatabaseManager dbManager)
    {
        List<float[]> allVectors = new List<float[]>();

        // Aggregate vectors from all databases
        foreach (var db in dbManager.GetAllDatabases())
        {
            allVectors.AddRange(db.GetAllVectors().Values);
        }

        // Synchronize each database with the unified set
        foreach (var db in dbManager.GetAllDatabases())
        {
            foreach (var vector in allVectors)
            {
                if (!db.ContainsVector(vector)) // Add only if it doesn't exist
                {
                    db.AddVector(db.GenerateUniqueId(), vector);
                }
            }
        }
    }
}
