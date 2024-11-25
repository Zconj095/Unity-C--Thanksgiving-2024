using System.Collections.Generic;
using System.Text.RegularExpressions;

public class InstructionParser
{
    public List<string> ParseInstructions(string input)
    {
        // Split input into individual instructions based on punctuation or connectors
        string[] rawInstructions = Regex.Split(input, @"(?<=[.!?])\s+|and then|, then");
        List<string> instructions = new List<string>();

        foreach (var instruction in rawInstructions)
        {
            if (!string.IsNullOrWhiteSpace(instruction))
            {
                instructions.Add(instruction.Trim());
            }
        }

        return instructions;
    }
}
