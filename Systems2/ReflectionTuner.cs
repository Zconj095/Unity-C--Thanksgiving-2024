using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

public class ReflectionTuner : MonoBehaviour
{
    public HyperCogniCortex Cortex;

    void Start()
    {
        if (Cortex == null)
        {
            Debug.LogError("Cogni Cortex is not assigned!");
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReflectAndTune("Dimensions", "Cognition Intensity", 0.2f);
        }
    }

    public void ReflectAndTune(string listName, string dimensionName, float delta)
    {
        // Use Reflection to access the Cortex's Dimensions
        Type cortexType = typeof(HyperCogniCortex);
        FieldInfo fieldInfo = cortexType.GetField(listName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (fieldInfo != null)
        {
            var dimensions = fieldInfo.GetValue(Cortex) as List<HyperDimension>;
            if (dimensions != null)
            {
                var dimension = dimensions.Find(d => d.Name == dimensionName);
                if (dimension != null)
                {
                    dimension.Tune(delta);
                    Debug.Log($"Reflection Tuned {dimensionName} by {delta}. New Value: {dimension.Value}");
                }
            }
        }
    }
}
