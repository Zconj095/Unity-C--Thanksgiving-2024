using System;
using System.Collections.Generic;

public class OneQubitGateErrorMap
{
    private List<Dictionary<string, float>> _errorMap;

    public OneQubitGateErrorMap(int numQubits)
    {
        _errorMap = new List<Dictionary<string, float>>(numQubits);
        for (int i = 0; i < numQubits; i++)
        {
            _errorMap.Add(new Dictionary<string, float>());
        }
    }

    public void AddQubit(Dictionary<string, float> gateErrors)
    {
        _errorMap.Add(gateErrors);
    }

    public Dictionary<string, float> GetQubitErrors(int qubitIndex)
    {
        return _errorMap[qubitIndex];
    }
}
