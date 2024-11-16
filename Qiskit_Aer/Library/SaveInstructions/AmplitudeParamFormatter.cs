using System;
using System.Collections.Generic;
public class AmplitudeParamFormatter
{
    public List<int> FormatAmplitudeParams(List<string> parameters, int? numQubits = null)
    {
        var formatted = new List<int>();

        foreach (var param in parameters)
        {
            int value = param.StartsWith("0x") ? Convert.ToInt32(param, 16) : Convert.ToInt32(param, 2);
            formatted.Add(value);
        }

        if (numQubits.HasValue && formatted.Max() >= Math.Pow(2, numQubits.Value))
        {
            throw new ArgumentException("Param values contain a state larger than the number of qubits.");
        }

        return formatted;
    }
}