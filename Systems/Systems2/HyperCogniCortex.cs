using System;
using System.Collections.Generic;
using UnityEngine;

public class HyperCogniCortex : MonoBehaviour
{
    public List<HyperDimension> Dimensions;

    void Start()
    {
        // Initialize some dimensions
        Dimensions = new List<HyperDimension>
        {
            new HyperDimension("Time", 1.0f, 0.1f, 10.0f),
            new HyperDimension("Space", 5.0f, 0.0f, 20.0f),
            new HyperDimension("Energy", 10.0f, 0.0f, 100.0f),
            new HyperDimension("Cognition Intensity", 0.5f, 0.0f, 1.0f)
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TuneDimension("Cognition Intensity", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(GetDimensionStatus());
        }
    }

    public void TuneDimension(string dimensionName, float delta)
    {
        var dimension = Dimensions.Find(d => d.Name == dimensionName);
        if (dimension != null)
        {
            dimension.Tune(delta);
            Debug.Log($"Tuned {dimensionName} by {delta}. New Value: {dimension.Value}");
        }
    }

    public string GetDimensionStatus()
    {
        return string.Join("\n", Dimensions);
    }
}
