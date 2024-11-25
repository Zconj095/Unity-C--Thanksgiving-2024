using System.IO;

public static class InstructionSetSaver
{
    public static void SaveInstructionSet(InstructionSet instructionSet, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write properties of the InstructionSet to the file
            writer.WriteLine($"InstructionSet Name: {instructionSet.Name}");
            writer.WriteLine($"Instruction Count: {instructionSet.Instructions.Count}");

            foreach (var instruction in instructionSet.Instructions)
            {
                writer.WriteLine($"Instruction: {instruction}");
            }
        }
    }

    
}
