using System;
using System.Collections.Generic;
public class SaveAverageData : SaveData
{
    /// <summary>
    /// Save averageable data.
    /// </summary>
    public SaveAverageData(
        string name,
        int numQubits,
        string label,
        bool unnormalized = false,
        bool pershot = false,
        bool conditional = false,
        List<object> parameters = null)
        : base(
            name,
            numQubits,
            label,
            DetermineSubtype(unnormalized, pershot, conditional),
            parameters)
    {
    }

    private static string DetermineSubtype(bool unnormalized, bool pershot, bool conditional)
    {
        string subtype = pershot ? "list" : unnormalized ? "accum" : "average";
        if (conditional)
        {
            subtype = "c_" + subtype;
        }
        return subtype;
    }
}