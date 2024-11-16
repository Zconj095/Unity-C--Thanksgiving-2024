using System.Collections.Generic;

public class SaveSingleData : SaveData
{
    /// <summary>
    /// Save non-averagable single data type.
    /// </summary>
    public SaveSingleData(
        string name,
        int numQubits,
        string label,
        bool pershot = false,
        bool conditional = false,
        List<object> parameters = null)
        : base(
            name,
            numQubits,
            label,
            DetermineSubtype(pershot, conditional),
            parameters)
    {
    }

    private static string DetermineSubtype(bool pershot, bool conditional)
    {
        string subtype = pershot ? "list" : "single";
        if (conditional)
        {
            subtype = "c_" + subtype;
        }
        return subtype;
    }
}