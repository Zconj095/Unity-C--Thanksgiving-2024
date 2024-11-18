using System;
using System.Collections.Generic;
using System.Linq;

public class SamplerResult
{
    public int SampleId { get; set; }
    public Dictionary<string, int> MeasurementResults { get; set; }
    public DateTime Timestamp { get; set; }

    // Constructor to initialize with a sample ID and results
    public SamplerResult(int sampleId, Dictionary<string, int> measurementResults)
    {
        SampleId = sampleId;
        MeasurementResults = measurementResults;
        Timestamp = DateTime.Now;
    }

    // Method to display the sampling results
    public string DisplayResults()
    {
        string results = string.Join(", ", MeasurementResults.Select(kv => $"{kv.Key}: {kv.Value}"));
        return $"Sample {SampleId}: {results} (Timestamp: {Timestamp})";
    }
}
