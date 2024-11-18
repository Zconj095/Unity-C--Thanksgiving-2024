using System;
using System.Collections.Generic;
public class PrimitiveResult
{
    public int ResultId { get; set; }
    public double ResultValue { get; set; }
    public DateTime Timestamp { get; set; }

    public PrimitiveResult(int resultId, double resultValue)
    {
        ResultId = resultId;
        ResultValue = resultValue;
        Timestamp = DateTime.Now;
    }

    // Method to display the result
    public string DisplayResult()
    {
        return $"Result {ResultId}: {ResultValue} (Timestamp: {Timestamp})";
    }
}
