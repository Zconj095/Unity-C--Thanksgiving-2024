using System;
using System.IO;
using System.Collections.Generic;

public static class InstructionSetLoader
{
    public static InstructionSet LoadInstructionSet(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException("Instruction set file not found.");

        string name = string.Empty;
        var instructions = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("InstructionSet Name:"))
                {
                    name = line.Substring("InstructionSet Name:".Length).Trim();
                }
                else if (line.StartsWith("Instruction:"))
                {
                    instructions.Add(line.Substring("Instruction:".Length).Trim());
                }
                // The line for Instruction Count is ignored as it's not necessary to parse.
            }
        }

        return new InstructionSet(name, instructions);
    }
}

