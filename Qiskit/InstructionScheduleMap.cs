using System;
using System.Collections.Generic;

public class InstructionScheduleMap
{
    private Dictionary<string, List<string>> _instructionMap;

    public InstructionScheduleMap()
    {
        _instructionMap = new Dictionary<string, List<string>>();
    }

    public void AddInstruction(string name, List<string> instructions)
    {
        _instructionMap[name] = instructions;
    }

    public bool HasInstruction(string name, List<int> qubits)
    {
        // For simplicity, check if the instruction exists for the qubits provided
        return _instructionMap.ContainsKey(name);
    }

    public List<string> Get(string name, int qubitIndex)
    {
        if (_instructionMap.ContainsKey(name))
        {
            return _instructionMap[name];
        }
        return new List<string>();  // Return an empty list if the instruction isn't found
    }
}
