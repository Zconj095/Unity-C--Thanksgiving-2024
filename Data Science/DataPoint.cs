using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Represents a single data point in the dataset.
/// </summary>
public class DataPoint
{
    public float[] Features { get; set; }
    public int Label { get; set; }

    public DataPoint(float[] features, int label)
    {
        Features = features;
        Label = label;
    }
}
