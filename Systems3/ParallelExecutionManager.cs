using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class ParallelExecutionManager
{
    private SynchronousExecutionManager executionManager;

    public ParallelExecutionManager(SynchronousExecutionManager manager)
    {
        executionManager = manager;
    }

    public async Task<List<object>> ExecuteSequencesInParallel(List<List<string>> sequences, object initialInput = null)
    {
        List<Task<List<object>>> tasks = new List<Task<List<object>>>();

        foreach (var sequence in sequences)
        {
            // Wrap each sequence into a new List<List<string>> to match the expected parameter type
            var wrappedSequence = new List<List<string>> { sequence };
            tasks.Add(Task.Run(() => executionManager.ExecuteInstructionSequences(wrappedSequence, initialInput)));
        }

        // Await all tasks to complete
        List<object>[] results = await Task.WhenAll(tasks);

        // Flatten the results into a single list of objects
        return results.SelectMany(list => list).ToList();
    }
}
