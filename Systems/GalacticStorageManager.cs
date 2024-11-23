using System;
using System.Collections.Generic;
using UnityEngine;


public class GalacticStorageManager : MonoBehaviour
{
    private SelicdanaDimensionalShift _dimensionalShift;
    private HyperfluxStorageContainer _hyperfluxStorage;

    void Start()
    {
        _dimensionalShift = new SelicdanaDimensionalShift();
        _hyperfluxStorage = new HyperfluxStorageContainer(TimeSpan.FromMinutes(10));
    }

    public void AddGalacticData(int dimension, string key, object value)
    {
        var data = new AizenFlowData
        {
            Key = key,
            Value = value,
            Timestamp = DateTime.Now
        };

        _dimensionalShift.AddData(dimension, data);
        _hyperfluxStorage.StoreData(data);
    }

    public List<AizenFlowData> GetDimensionData(int dimension)
    {
        return _dimensionalShift.GetData(dimension);
    }

    void Update()
    {
        // Periodic cleanup
        _hyperfluxStorage.Cleanup();
    }
}
