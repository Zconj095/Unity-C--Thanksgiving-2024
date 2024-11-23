using System.Collections.Generic;
using UnityEngine;

public class MovingAveragePredictor
{
    public float Predict(List<HiddenTimeField> fields, string variableName, int windowSize = 5)
    {
        float sum = 0f;
        int count = 0;

        // Iterate over the last `windowSize` fields
        for (int i = fields.Count - 1; i >= 0 && count < windowSize; i--)
        {
            if (fields[i].Variables.ContainsKey(variableName))
            {
                sum += fields[i].Variables[variableName];
                count++;
            }
        }

        return count > 0 ? sum / count : 0f; // Return the average or 0 if no data
    }
}
