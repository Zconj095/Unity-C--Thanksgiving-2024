using System.Collections.Generic;
using System;
using System.Linq;

public class AerOperatorUtils
{
    /// <summary>
    /// Convert operator to SparsePauliOp parameters for saving.
    /// </summary>
    public List<Tuple<string, (double, double)>> ExpValParams(SparsePauliOp operatorObj, bool variance)
    {
        var paramsDict = new Dictionary<string, (double, double)>();

        // Add Pauli basis components of the operator
        foreach (var (pauli, coeff) in operatorObj.LabelIterator())
        {
            if (paramsDict.ContainsKey(pauli))
            {
                var current = paramsDict[pauli];
                paramsDict[pauli] = (current.Item1 + coeff.Real, current.Item2);
            }
            else
            {
                paramsDict[pauli] = (coeff.Real, 0.0);
            }
        }

        // Add Pauli basis components of the operator squared if variance is required
        if (variance)
        {
            foreach (var (pauli, coeff) in operatorObj.Dot(operatorObj).LabelIterator())
            {
                if (paramsDict.ContainsKey(pauli))
                {
                    var current = paramsDict[pauli];
                    paramsDict[pauli] = (current.Item1, current.Item2 + coeff.Real);
                }
                else
                {
                    paramsDict[pauli] = (0.0, coeff.Real);
                }
            }
        }

        // Convert dictionary to list of tuples
        return paramsDict.Select(kvp => Tuple.Create(kvp.Key, kvp.Value)).ToList();
    }
}