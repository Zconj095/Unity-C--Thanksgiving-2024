using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class DataSourceTracker
{
    private Dictionary<string, string> sourceMetadata;

    public DataSourceTracker()
    {
        sourceMetadata = new Dictionary<string, string>();
    }

    public void AddSource(string dataId, string source)
    {
        sourceMetadata[dataId] = source;
    }

    public string GetSource(string dataId)
    {
        return sourceMetadata.ContainsKey(dataId) ? sourceMetadata[dataId] : "Unknown Source";
    }

    public Dictionary<string, string> GetAllSources()
    {
        return sourceMetadata;
    }
}
