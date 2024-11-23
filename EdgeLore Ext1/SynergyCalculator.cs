using System.Collections.Generic;
using UnityEngine;
public class SynergyCalculator
{
    public static float CalculateSynergy(Dictionary<string, float> inputs)
    {
        float synergyOutput = 0f;

        foreach (var input in inputs.Values)
        {
            synergyOutput += Mathf.Pow(input, 2); // Example: additive square contributions
        }

        return Mathf.Sqrt(synergyOutput); // Normalize synergy effect
    }
}
