using System.Collections.Generic;
using System;

public class SynchronousExecutionManager
{
    private FunctionRegistry functionRegistry;
    private InstructionCache instructionCache;

    public SynchronousExecutionManager(FunctionRegistry registry, InstructionCache cache)
    {
        functionRegistry = registry;
        instructionCache = cache;
    }

    public List<object> ExecuteInstructionSequences(List<List<string>> sequences, object initialInput = null)
    {
        List<object> results = new List<object>();
        object currentInput = initialInput;

        foreach (var sequence in sequences)
        {
            foreach (var instruction in sequence)
            {
                if (instructionCache.IsInCache(instruction))
                {
                    currentInput = instructionCache.GetFromCache(instruction);
                }
                else
                {
                    currentInput = ExecuteInstruction(instruction, currentInput);
                    instructionCache.AddToCache(instruction, currentInput);
                }

                results.Add(currentInput);
            }
        }

        return results;
    }

    private object ExecuteInstruction(string instruction, object input)
    {
        // Match the instruction to a registered function or built-in logic
        if (instruction.Contains("summarize"))
        {
            return Summarize(input.ToString());
        }
        else if (instruction.Contains("translate into Spanish"))
        {
            return Translate(input.ToString(), "Spanish");
        }
        else if (instruction.Contains("translate into French"))
        {
            return Translate(input.ToString(), "French");
        }
        else if (functionRegistry.HasFunction(instruction))
        {
            return functionRegistry.ExecuteFunction(instruction, input);
        }

        throw new Exception($"Unknown instruction: {instruction}");
    }

    private string Summarize(string text)
    {
        return $"(Summary of: {text})"; // Placeholder
    }

    private string Translate(string text, string language)
    {
        return $"(Translation of {text} to {language})"; // Placeholder
    }
}
