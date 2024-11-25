using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class MultiInstructionParser
{
    public List<List<string>> ParseInstructionSequences(string input)
    {
        // Split input into sequences by logical connectors (e.g., "then", "next")
        string[] rawSequences = Regex.Split(input, @"\bthen\b|\bnext\b|\band\b", RegexOptions.IgnoreCase);
        List<List<string>> instructionSequences = new List<List<string>>();

        foreach (var sequence in rawSequences)
        {
            List<string> instructions = Regex.Split(sequence, @"(?<=[.!?])\s+").ToList();
            instructions.RemoveAll(string.IsNullOrWhiteSpace); // Remove empty instructions
            if (instructions.Count > 0)
            {
                instructionSequences.Add(instructions);
            }
        }

        return instructionSequences;
    }
}
