using System.Collections.Generic;
public class InstructionExecutor
{
    private FunctionRegistry functionRegistry;

    public InstructionExecutor(FunctionRegistry registry)
    {
        functionRegistry = registry;
    }

    public object ExecuteInstructions(List<string> instructions, object input = null)
    {
        object currentOutput = input;

        foreach (var instruction in instructions)
        {
            // Match instruction to registered functions
            if (instruction.Contains("summarize"))
            {
                currentOutput = Summarize(currentOutput.ToString());
            }
            else if (instruction.Contains("translate into Spanish"))
            {
                currentOutput = Translate(currentOutput.ToString(), "Spanish");
            }
            else if (functionRegistry.HasFunction(instruction))
            {
                currentOutput = functionRegistry.ExecuteFunction(instruction, currentOutput);
            }
        }

        return currentOutput;
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
