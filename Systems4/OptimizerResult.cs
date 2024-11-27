using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class OptimizerResult : MonoBehaviour
{
    private Dictionary<string, object> properties;

    // Constructor to initialize properties
    public OptimizerResult()
    {
        properties = new Dictionary<string, object>
        {
            { "x", null },    // Final point of minimization
            { "fun", null },  // Final value of minimization
            { "jac", null },  // Final gradient of minimization
            { "nfev", null }, // Total number of function evaluations
            { "njev", null }, // Total number of gradient evaluations
            { "nit", null }   // Total number of iterations
        };
    }

    // Get property value dynamically
    public object GetProperty(string propertyName)
    {
        if (properties.ContainsKey(propertyName))
        {
            return properties[propertyName];
        }
        throw new ArgumentException($"Property '{propertyName}' does not exist.");
    }

    // Set property value dynamically
    public void SetProperty(string propertyName, object value)
    {
        if (properties.ContainsKey(propertyName))
        {
            properties[propertyName] = value;
        }
        else
        {
            throw new ArgumentException($"Property '{propertyName}' does not exist.");
        }
    }

    // Get all properties as a dictionary
    public Dictionary<string, object> GetAllProperties()
    {
        return properties.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    // Print all property values to console
    public void PrintProperties()
    {
        foreach (var property in properties)
        {
            Debug.Log($"{property.Key}: {property.Value ?? "null"}");
        }
    }

    // Invoke a private method dynamically
    public object InvokeMethod(string methodName, params object[] parameters)
    {
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method != null)
        {
            return method.Invoke(this, parameters);
        }
        throw new ArgumentException($"Method '{methodName}' does not exist.");
    }

    // Example private method for reflection invocation
    private void ExamplePrivateMethod()
    {
        Debug.Log("This is an example private method, invoked dynamically using reflection.");
    }

    // Static utility method to compute gradient using numerical differentiation
    public static float[] GradientNumDiff(float[] xCenter, Func<float[], float> func, float epsilon)
    {
        int length = xCenter.Length;
        float[] gradient = new float[length];
        float originalValue = func(xCenter);

        for (int i = 0; i < length; i++)
        {
            float[] perturbed = (float[])xCenter.Clone();
            perturbed[i] += epsilon;
            gradient[i] = (func(perturbed) - originalValue) / epsilon;
        }

        return gradient;
    }

    // Static method to wrap a function for injected arguments
    public static Func<float[], float> WrapFunction(Func<float[], float> func, object[] args)
    {
        return input =>
        {
            object[] combinedArgs = new object[input.Length + args.Length];
            input.CopyTo(combinedArgs, 0);
            args.CopyTo(combinedArgs, input.Length);
            return (float)func.DynamicInvoke(combinedArgs);
        };
    }

    // Settings as a formatted string
    public string Settings()
    {
        string result = "Optimizer Settings:\n";
        foreach (var property in properties)
        {
            result += $"{property.Key}: {property.Value ?? "null"}\n";
        }
        return result;
    }

    // Simulation of optimization routine
    public void Minimize(Func<float[], float> objectiveFunction, float[] initialPoint, float epsilon)
    {
        Debug.Log("Starting optimization...");
        float[] gradient = GradientNumDiff(initialPoint, objectiveFunction, epsilon);
        SetProperty("x", initialPoint);
        SetProperty("fun", objectiveFunction(initialPoint));
        SetProperty("jac", gradient);
        SetProperty("nfev", 1);
        SetProperty("njev", 1);
        SetProperty("nit", 1);

        Debug.Log("Optimization completed.");
        PrintProperties();
    }

    // Unity Start method for initialization
    void Start()
    {
        Debug.Log("OptimizerResult instance created.");
        
        // Example usage of dynamic properties
        SetProperty("x", new float[] { 1.0f, 2.0f });
        SetProperty("fun", 0.5f);
        Debug.Log($"Property x: {GetProperty("x")}");
        Debug.Log($"Property fun: {GetProperty("fun")}");

        // Example numerical differentiation
        float[] initialPoint = { 1.0f, 2.0f };
        float epsilon = 0.01f;

        Func<float[], float> objectiveFunction = x => x[0] * x[0] + x[1] * x[1];
        Minimize(objectiveFunction, initialPoint, epsilon);

        // Example of invoking a private method dynamically
        InvokeMethod("ExamplePrivateMethod");
    }
}
