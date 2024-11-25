using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class ConversationalInstructionHandler
{
    private FunctionRegistry functionRegistry;
    private SynchronousExecutionManager executionManager;

    public ConversationalInstructionHandler(FunctionRegistry functionRegistry, SynchronousExecutionManager executionManager)
    {
        this.functionRegistry = functionRegistry;
        this.executionManager = executionManager;
    }

    public object ExecuteFromInput(string userInput)
    {
        MultiInstructionParser parser = new MultiInstructionParser();
        List<List<string>> instructionSequences = parser.ParseInstructionSequences(userInput);

        if (instructionSequences.Count > 0)
        {
            return executionManager.ExecuteInstructionSequences(instructionSequences);
        }

        return null; // No instructions to execute
    }
}
