using System;
using System.Collections.Generic;

public class SaveData : Instruction
{
    /// <summary>
    /// Pragma Instruction to save simulator data.
    /// </summary>
    private static readonly HashSet<string> AllowedSubtypes = new HashSet<string>
    {
        "single", "c_single", "list", "c_list", "average", "c_average", "accum", "c_accum"
    };

    private string _label;
    private string _subtype;

    public SaveData(string name, int numQubits, string label, string subtype = "single", List<object> parameters = null)
        : base(name, numQubits, 0, parameters ?? new List<object>())
    {
        if (!AllowedSubtypes.Contains(subtype))
        {
            throw new ArgumentException("Invalid data subtype for SaveData instruction.");
        }

        if (string.IsNullOrEmpty(label))
        {
            throw new ArgumentException("Label for SaveData instruction must be a non-empty string.");
        }

        _label = label;
        _subtype = subtype;
    }

    public SaveData Inverse(bool annotated = false)
    {
        // Special case: Return a copy of itself
        return (SaveData)this.MemberwiseClone();
    }
}