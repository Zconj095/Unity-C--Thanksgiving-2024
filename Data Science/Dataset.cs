using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Represents a dataset consisting of a collection of data points.
/// </summary>
public class Dataset
{
    public List<DataPoint> DataPoints { get; }

    public Dataset(List<DataPoint> dataPoints)
    {
        DataPoints = dataPoints ?? throw new ArgumentNullException(nameof(dataPoints));
    }
}
