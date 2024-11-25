using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstructionManager
{
    private List<Instruction> instructions = new List<Instruction>();

    public void AddInstruction(Instruction instruction)
    {
        instructions.Add(instruction);
        instructions = instructions.OrderBy(i => i.Priority).ToList();
    }

    public string GetResponse(string input)
    {
        foreach (var instruction in instructions)
        {
            if (input.Contains(instruction.Trigger))
                return instruction.ResponseTemplate;
        }
        return "No matching instruction found.";
    }
}
