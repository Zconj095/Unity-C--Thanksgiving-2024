using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class BaseSamplerGradient
{
    protected object Sampler { get; private set; } // Dynamic sampler object
    private Dictionary<string, object> gradientCircuitCache;
    private object defaultOptions;

    /// <summary>
    /// Constructor for BaseSamplerGradient
    /// </summary>
    /// <param name="sampler">Dynamic sampler object.</param>
    /// <param name="options">Gradient options (optional).</param>
    protected BaseSamplerGradient(object sampler, object options = null)
    {
        Sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
        gradientCircuitCache = new Dictionary<string, object>();
        defaultOptions = options ?? Activator.CreateInstance(GetTypeByName("GradientOptions"));
    }

    /// <summary>
    /// Executes the gradient computation workflow.
    /// </summary>
    public object Run(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters = null
    )
    {
        // Validate arguments
        ValidateArguments(circuits, parameterValues, parameters);

        // Preprocess circuits and parameters
        var preprocessedData = Preprocess(circuits, parameterValues, parameters);

        // Compute gradients
        var results = ComputeGradients(preprocessedData.Item1, preprocessedData.Item2, preprocessedData.Item3);

        // Postprocess results
        return Postprocess(results, circuits, parameterValues, parameters);
    }

    /// <summary>
    /// Abstract method to compute gradients (to be implemented in derived classes).
    /// </summary>
    protected abstract object ComputeGradients(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    );

    /// <summary>
    /// Preprocesses the input data and prepares gradient circuits.
    /// </summary>
    private Tuple<List<object>, List<List<float>>, List<List<object>>> Preprocess(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    )
    {
        var processedCircuits = new List<object>();
        var processedParameterValues = new List<List<float>>();
        var processedParameters = new List<List<object>>();

        foreach (var circuit in circuits)
        {
            string circuitKey = InvokeMethod<string>(circuit, "GetKey");

            if (!gradientCircuitCache.ContainsKey(circuitKey))
            {
                var gradientCircuit = Activator.CreateInstance(GetTypeByName("GradientCircuit"), circuit);
                gradientCircuitCache[circuitKey] = gradientCircuit;
            }

            var cachedCircuit = gradientCircuitCache[circuitKey];
            processedCircuits.Add(GetProperty<object>(cachedCircuit, "Circuit"));
            processedParameterValues.Add(parameterValues[circuits.IndexOf(circuit)]);
            processedParameters.Add(parameters?[circuits.IndexOf(circuit)] ?? GetProperty<List<object>>(cachedCircuit, "Parameters"));
        }

        return new Tuple<List<object>, List<List<float>>, List<List<object>>>(
            processedCircuits,
            processedParameterValues,
            processedParameters
        );
    }

    /// <summary>
    /// Processes the computed gradients and prepares the final result.
    /// </summary>
    private object Postprocess(
        object rawResults,
        List<object> originalCircuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    )
    {
        var gradients = new List<List<Dictionary<int, float>>>();
        var metadata = new List<Dictionary<string, object>>();

        foreach (var (originalCircuit, idx) in originalCircuits.Select((value, index) => (value, index)))
        {
            var gradientResult = new List<Dictionary<int, float>>();

            foreach (var parameter in parameters[idx])
            {
                var gradient = new Dictionary<int, float>();
                gradientResult.Add(gradient); // Placeholder for actual chain rule logic
            }

            gradients.Add(gradientResult);

            metadata.Add(new Dictionary<string, object>
            {
                { "parameters", parameters[idx] }
            });
        }

        return Activator.CreateInstance(
            GetTypeByName("GradientJob"),
            gradients,
            metadata
        );
    }

    /// <summary>
    /// Validates the input arguments for the gradient computation.
    /// </summary>
    private void ValidateArguments(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    )
    {
        if (circuits.Count != parameterValues.Count)
        {
            throw new ArgumentException("The number of circuits must match the number of parameter value sets.");
        }

        if (parameters != null && circuits.Count != parameters.Count)
        {
            throw new ArgumentException("The number of circuits must match the number of parameter sets.");
        }

        foreach (var (circuit, values) in circuits.Zip(parameterValues, Tuple.Create))
        {
            var circuitParameters = GetProperty<List<object>>(circuit, "Parameters");
            if (circuitParameters.Count != values.Count)
            {
                string circuitName = GetProperty<string>(circuit, "Name");
                throw new ArgumentException($"Mismatch between circuit parameters and parameter values for circuit: {circuitName}");
            }
        }
    }

    /// <summary>
    /// Dynamically retrieves a property value from an object.
    /// </summary>
    private static T GetProperty<T>(object obj, string propertyName)
    {
        PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new MissingMemberException($"Property '{propertyName}' not found on type '{obj.GetType()}'.");
        }
        return (T)property.GetValue(obj);
    }

    /// <summary>
    /// Dynamically invokes a method on an object.
    /// </summary>
    private static T InvokeMethod<T>(object obj, string methodName, params object[] parameters)
    {
        MethodInfo method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"Method '{methodName}' not found on type '{obj.GetType()}'.");
        }
        return (T)method.Invoke(obj, parameters);
    }

    /// <summary>
    /// Dynamically retrieves a type by name.
    /// </summary>
    private static Type GetTypeByName(string typeName)
    {
        Type type = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == typeName);

        if (type == null)
        {
            throw new TypeLoadException($"Type '{typeName}' not found.");
        }

        return type;
    }
}
