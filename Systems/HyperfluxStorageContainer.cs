using System;
using System.Collections.Generic;

public class HyperfluxStorageContainer
{
    private List<AizenFlowData> _storage;
    private TimeSpan _timeDecay;

    public HyperfluxStorageContainer(TimeSpan timeDecay)
    {
        _storage = new List<AizenFlowData>();
        _timeDecay = timeDecay;
    }

    public void StoreData(AizenFlowData data)
    {
        _storage.Add(data);
    }

    public void Cleanup()
    {
        _storage.RemoveAll(d => (DateTime.Now - d.Timestamp) > _timeDecay);
    }

    public IEnumerable<AizenFlowData> RetrieveAll()
    {
        Cleanup();
        return _storage;
    }
}
