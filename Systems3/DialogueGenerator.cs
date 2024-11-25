using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class DialogueGenerator
{
    public string Generate(string userInput, List<MemoryItem> relevantMemories, object instructionResult)
    {
        string memoryContent = string.Join("\n", relevantMemories.Select(m => m.Content));
        string instructionContent = instructionResult != null ? instructionResult.ToString() : "";

        // Use memory and instruction context for reply generation
        return $"I recall the following:\n{memoryContent}\n\nBased on your instruction:\n{instructionContent}\n\nLet's continue the discussion!";
    }
}
