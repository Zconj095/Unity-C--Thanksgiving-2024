using System;
using System.Collections.Generic;

public class Result
{
    // Stores the measurement outcomes in a dictionary (key = qubit, value = result)
    public Dictionary<int, int> MeasurementResults { get; set; }

    // Stores the number of shots performed in the simulation or experiment
    public int Shots { get; set; }

    // Constructor to initialize the Result object
    public Result(Dictionary<int, int> measurementResults, int shots)
    {
        MeasurementResults = measurementResults;
        Shots = shots;
    }

    // Example method to get the result of a specific qubit
    public int GetQubitResult(int qubit)
    {
        if (MeasurementResults.ContainsKey(qubit))
        {
            return MeasurementResults[qubit];
        }
        else
        {
            throw new ArgumentException($"No result for qubit {qubit}.");
        }
    }

    // Example method to get the counts of all measurement outcomes
    public Dictionary<int, int> GetCounts()
    {
        var counts = new Dictionary<int, int>();

        foreach (var result in MeasurementResults.Values)
        {
            if (!counts.ContainsKey(result))
            {
                counts[result] = 0;
            }
            counts[result]++;
        }

        return counts;
    }

    // Method to print the results in a human-readable format
    public void PrintResults()
    {
        Console.WriteLine($"Total Shots: {Shots}");
        foreach (var result in MeasurementResults)
        {
            Console.WriteLine($"Qubit {result.Key}: {result.Value}");
        }
    }
}
