using System;
using System.Collections.Generic;
using System.Linq;

public class SamplerPubResult
{
    // Dictionary to store measurement results
    public Dictionary<string, int> MeasurementResults { get; set; }

    // Constructor to initialize the results
    public SamplerPubResult()
    {
        MeasurementResults = new Dictionary<string, int>();
    }

    // Method to add a measurement result
    public void AddMeasurement(string name, int result)
    {
        MeasurementResults[name] = result;
    }

    // Method to get a string representation of the results
    public override string ToString()
    {
        return string.Join(", ", MeasurementResults.Select(kv => $"{kv.Key}: {kv.Value}"));
    }
}
