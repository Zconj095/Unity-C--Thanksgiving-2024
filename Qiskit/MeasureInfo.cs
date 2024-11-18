using System;
using System.Collections.Generic;

public class MeasureInfo
{
    public int QubitIndex { get; set; }
    public string MeasurementBasis { get; set; }

    // Constructor to initialize measurement information
    public MeasureInfo(int qubitIndex, string measurementBasis)
    {
        QubitIndex = qubitIndex;
        MeasurementBasis = measurementBasis;
    }

    // Method to simulate the measurement process
    public void Measure()
    {
        // Simulate the measurement process
        System.Threading.Thread.Sleep(200);  // Simulate delay
        Console.WriteLine($"Qubit {QubitIndex} measured in {MeasurementBasis} basis.");
    }

    public override string ToString()
    {
        return $"Qubit: {QubitIndex}, Measurement Basis: {MeasurementBasis}";
    }
}
