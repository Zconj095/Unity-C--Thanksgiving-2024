using System;
using System.Collections.Generic;

public class SelicdanaDimensionalShift
{
    private Dictionary<int, List<AizenFlowData>> _dimensionMap;

    public SelicdanaDimensionalShift()
    {
        _dimensionMap = new Dictionary<int, List<AizenFlowData>>();
    }

    public void AddData(int dimension, AizenFlowData data)
    {
        if (!_dimensionMap.ContainsKey(dimension))
        {
            _dimensionMap[dimension] = new List<AizenFlowData>();
        }
        _dimensionMap[dimension].Add(data);
    }

    public List<AizenFlowData> GetData(int dimension)
    {
        return _dimensionMap.ContainsKey(dimension) ? _dimensionMap[dimension] : new List<AizenFlowData>();
    }
}
