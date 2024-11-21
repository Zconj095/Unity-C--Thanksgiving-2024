using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class KmeansDataPoint : MonoBehaviour
{
    public float[] Features { get; private set; }
    public int AssignedCluster { get; set; }

    public KmeansDataPoint(float[] features)
    {
        if (features == null)
            throw new ArgumentNullException(nameof(features), "Features cannot be null.");

        Features = features;
        AssignedCluster = -1;
    }

    public override string ToString()
    {
        return "[" + string.Join(", ", Features.Select(f => f.ToString("F2"))) + "]";
    }

    public override bool Equals(object obj)
    {
        if (obj is not KmeansDataPoint other || Features == null || other.Features == null)
            return false;
        return Features.SequenceEqual(other.Features);
    }

    public override int GetHashCode()
    {
        return Features?.Aggregate(0, (hash, feature) => hash * 31 + feature.GetHashCode()) ?? 0;
    }
}
