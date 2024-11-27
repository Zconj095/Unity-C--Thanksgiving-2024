using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class BaseQGT
{
    protected object Estimator { get; private set; }
    protected bool PhaseFix { get; private set; }
    protected string DerivativeType { get; private set; }
    private Dictionary<string, object> qgtCircuitCache;
    private Dictionary<string, object> gradientCircuitCache;

    protected BaseQGT(
        object estimator,
        bool phaseFix = true,
        string derivativeType = "COMPLEX"
    )
    {
        Estimator = estimator ?? throw new ArgumentNullException(nameof(estimator));
        PhaseFix = phaseFix;
        DerivativeType = derivativeType;
        qgtCircuitCache = new Dictionary<string, object>();
        gradientCircuitCache = new Dictionary<string, object>();
    }

    public object Run(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters = null
    )
    {
        // Validate arguments using reflection
        ValidateArguments(circuits, parameterValues, parameters);

        // Preprocess circuits and parameters
        var preprocessedData = Preprocess(circuits, parameterValues, parameters);

        // Compute QGT (abstract for specific implementation)
        var rawResults = ComputeQGT(preprocessedData.Item1, preprocessedData.Item2, preprocessedData.Item3);

        // Postprocess results
        return Postprocess(rawResults, circuits, parameterValues, parameters);
    }

    protected abstract object ComputeQGT(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    );

    private Tuple<List<object>, List<List<float>>, List<List<object>>> Preprocess(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    )
    {
        var processedCircuits = circuits.Select(circuit =>
        {
            string circuitKey = InvokeMethod<string>(circuit, "GetKey");

            if (!gradientCircuitCache.ContainsKey(circuitKey))
            {
                gradientCircuitCache[circuitKey] = CreateGradientCircuit(circuit);
            }

            return GetProperty<object>(gradientCircuitCache[circuitKey], "Circuit");
        }).ToList();

        var processedParameterValues = circuits.Select(c => parameterValues[circuits.IndexOf(c)]).ToList();

        var processedParameters = circuits.Select(c =>
        {
            string circuitKey = InvokeMethod<string>(c, "GetKey");
            return parameters?[circuits.IndexOf(c)] ?? GetProperty<List<object>>(gradientCircuitCache[circuitKey], "Parameters");
        }).ToList();

        return Tuple.Create(processedCircuits, processedParameterValues, processedParameters);
    }

    private object Postprocess(
        object rawResults,
        List<object> originalCircuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    )
    {
        var qgts = new List<float[,]>();
        var metadata = new List<Dictionary<string, object>>();

        foreach (var (originalCircuit, idx) in originalCircuits.Select((value, index) => (value, index)))
        {
            int paramCount = parameters[idx].Count;
            var qgtMatrix = new float[paramCount, paramCount];

            // Dynamically access QGT matrices in rawResults
            var rawQGTMatrix = GetProperty<List<float[,]>>(rawResults, "QGTMatrices")[idx];
            for (int i = 0; i < paramCount; i++)
            {
                for (int j = 0; j < paramCount; j++)
                {
                    qgtMatrix[i, j] = rawQGTMatrix[i, j];
                }
            }

            qgts.Add(qgtMatrix);
            metadata.Add(new Dictionary<string, object>
            {
                { "parameters", parameters[idx] }
            });
        }

        return new
        {
            QGTMatrices = qgts,
            Metadata = metadata
        };
    }

    private void ValidateArguments(
        List<object> circuits,
        List<List<float>> parameterValues,
        List<List<object>> parameters
    )
    {
        if (circuits.Count != parameterValues.Count)
            throw new ArgumentException("The number of circuits must match the number of parameter value sets.");

        if (parameters != null && circuits.Count != parameters.Count)
            throw new ArgumentException("The number of circuits must match the number of parameter sets.");

        foreach (var (circuit, values) in circuits.Zip(parameterValues, Tuple.Create))
        {
            int paramCount = GetProperty<List<object>>(circuit, "Parameters").Count;
            if (paramCount != values.Count)
            {
                string name = GetProperty<string>(circuit, "Name");
                throw new ArgumentException($"Mismatch between circuit parameters and parameter values for circuit: {name}");
            }
        }
    }

    private object CreateGradientCircuit(object circuit)
    {
        var constructor = typeof(GradientCircuit).GetConstructor(new[] { typeof(object) });
        return constructor?.Invoke(new object[] { circuit });
    }

    private T GetProperty<T>(object obj, string propertyName)
    {
        PropertyInfo property = obj.GetType().GetProperty(propertyName);
        return (T)property.GetValue(obj);
    }

    private T InvokeMethod<T>(object obj, string methodName, params object[] args)
    {
        MethodInfo method = obj.GetType().GetMethod(methodName);
        return (T)method.Invoke(obj, args);
    }

    // Inner Helper Class
    public class GradientCircuit
    {
        public object Circuit { get; }
        public List<object> Parameters { get; }

        public GradientCircuit(object circuit)
        {
            Circuit = circuit;
            Parameters = new List<object>(
                GetProperty<List<object>>(circuit, "Parameters").Select(p =>
                    new
                    {
                        Name = GetProperty<string>(p, "Name"),
                        Value = GetProperty<float>(p, "Value")
                    }));
        }

        private T GetProperty<T>(object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            return (T)property.GetValue(obj);
        }
    }
}
