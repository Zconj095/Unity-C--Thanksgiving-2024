using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class OptimizationProcessor : MonoBehaviour
{
    [Tooltip("Specify the tag to filter objects for optimization.")]
    public string optimizationTag = "Optimizable";

    private void Start()
    {
        if (string.IsNullOrEmpty(optimizationTag))
        {
            Debug.LogError("Optimization tag is not specified.");
            return;
        }

        Debug.Log($"Starting optimization for objects with tag: {optimizationTag}");
        OptimizeObjects(optimizationTag);
    }

    private void OptimizeObjects(string tag)
    {
        // Find all GameObjects with the specified tag
        var optimizableObjects = GameObject.FindGameObjectsWithTag(tag);

        if (optimizableObjects.Length == 0)
        {
            Debug.LogWarning($"No objects found with tag: {tag}");
            return;
        }

        foreach (var obj in optimizableObjects)
        {
            Debug.Log($"Optimizing object: {obj.name}");
            ProcessOptimization(obj);
        }
    }

    private void ProcessOptimization(GameObject obj)
    {
        // Analyze all components of the object
        var components = obj.GetComponents<Component>();

        foreach (var component in components)
        {
            Debug.Log($"Analyzing component: {component.GetType().Name}");
            OptimizeComponent(component);
        }
    }

    private void OptimizeComponent(Component component)
    {
        var type = component.GetType();

        // Identify optimizable properties
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && IsOptimizable(p));
        foreach (var property in properties)
        {
            try
            {
                var currentValue = property.GetValue(component);
                var optimizedValue = GetOptimizedValue(property, currentValue);
                property.SetValue(component, optimizedValue);
                Debug.Log($"Optimized Property: {property.Name}, New Value: {optimizedValue}");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to optimize property {property.Name}: {e.Message}");
            }
        }

        // Identify and invoke optimization-related methods
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.Name.StartsWith("Optimize"));
        foreach (var method in methods)
        {
            try
            {
                method.Invoke(component, null);
                Debug.Log($"Invoked Optimization Method: {method.Name}");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to invoke method {method.Name}: {e.Message}");
            }
        }
    }

    private bool IsOptimizable(PropertyInfo property)
    {
        // Define criteria for optimizable properties (e.g., numerical types)
        return property.PropertyType == typeof(float) || property.PropertyType == typeof(int);
    }

    private object GetOptimizedValue(PropertyInfo property, object currentValue)
    {
        // Simple optimization logic: Clamp numerical values within a desired range
        if (property.PropertyType == typeof(float))
        {
            float value = (float)currentValue;
            return Mathf.Clamp(value, 0f, 100f); // Example range
        }
        else if (property.PropertyType == typeof(int))
        {
            int value = (int)currentValue;
            return Mathf.Clamp(value, 0, 100); // Example range
        }

        // Return the original value if no optimization is applicable
        return currentValue;
    }
}
