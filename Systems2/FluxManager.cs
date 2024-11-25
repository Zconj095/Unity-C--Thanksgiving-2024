using UnityEngine;
using System.Collections.Generic;

public class FluxManager : MonoBehaviour
{
    public HyperCogniCortex Cortex;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StabilizeFlux();
        }
    }

    public void StabilizeFlux()
    {
        foreach (var dimension in Cortex.Dimensions)
        {
            float midpoint = (dimension.MinValue + dimension.MaxValue) / 2;
            float adjustment = (midpoint - dimension.Value) * 0.1f;
            dimension.Tune(adjustment);

            Debug.Log($"Stabilized {dimension.Name}. Adjusted by {adjustment}. New Value: {dimension.Value}");
        }
    }
}
