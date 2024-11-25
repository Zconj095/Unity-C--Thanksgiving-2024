using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MultiDatabaseManager
{
    private List<VectorDatabase2> databases = new List<VectorDatabase2>();

    public void AddDatabase(VectorDatabase2 db)
    {
        databases.Add(db);
    }

    public VectorDatabase2 GetDatabase(int index)
    {
        return databases[index];
    }

    public List<VectorDatabase2> GetAllDatabases()
    {
        return databases;
    }
}
