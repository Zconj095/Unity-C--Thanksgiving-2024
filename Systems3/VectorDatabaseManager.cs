using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class VectorDatabaseManager
{
    private List<VectorDatabase2> databases = new List<VectorDatabase2>();

    public VectorDatabase2 CreateDatabase(string path)
    {
        VectorDatabase2 db = new VectorDatabase2(path);
        databases.Add(db);
        return db;
    }

    public VectorDatabase2 GetDatabase(int index)
    {
        return databases[index];
    }
}
