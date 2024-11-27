using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; // Added for MethodInfo and BindingFlags
using UnityEngine;


public class SciPyOptimizer : MonoBehaviour
{
    private string _method;
    private Dictionary<string, object> _options;
    private int _maxEvalsGrouped;
    private Dictionary<string, string> _supportLevels;

    public SciPyOptimizer(
        string method,
        Dictionary<string, object> options = null,
        int maxEvalsGrouped = 1)
    {
        // Initialize method and options
        _method = method.ToLower();
        _options = options ?? new Dictionary<string, object>();
        _maxEvalsGrouped = Math.Max(1, maxEvalsGrouped); // Ensure valid grouping count

        // Define support levels
        _supportLevels = new Dictionary<string, string>
        {
            { "gradient", IsGradientSupported() ? "supported" : "ignored" },
            { "bounds", IsBoundsSupported() ? "supported" : "ignored" },
            { "initial_point", "required" }
        };
    }

    public Dictionary<string, string> GetSupportLevels()
    {
        return _supportLevels;
    }

    public OptimizerResult Minimize(
        Func<double[], double> objectiveFunction,
        double[] initialPoint,
        Func<double[], double[]>? gradientFunction = null,
        Tuple<double, double>[]? bounds = null)
    {
        // Handle bounds and gradient support
        if (_supportLevels["bounds"] == "ignored")
        {
            bounds = null;
        }

        if (_supportLevels["gradient"] == "ignored")
        {
            gradientFunction = null;
        }

        // Use numerical gradient if supported and necessary
        if (gradientFunction == null && _supportLevels["gradient"] == "supported" && _maxEvalsGrouped > 1)
        {
            gradientFunction = (x) => ComputeNumericalGradient(objectiveFunction, x);
        }

        // Execute optimization
        var result = ExecuteOptimization(objectiveFunction, initialPoint, gradientFunction, bounds);
        return result;
    }

    private OptimizerResult ExecuteOptimization(
        Func<double[], double> objectiveFunction,
        double[] initialPoint,
        Func<double[], double[]>? gradientFunction,
        Tuple<double, double>[]? bounds)
    {
        int iterations = 0;
        int functionEvaluations = 0;
        double[] currentPoint = (double[])initialPoint.Clone();
        double bestLoss = objectiveFunction(currentPoint);

        // Perform optimization loop
        while (iterations < (_options.ContainsKey("maxiter") ? (int)_options["maxiter"] : 100))
        {
            iterations++;

            // Update step using gradient
            double[] step = gradientFunction != null ? gradientFunction(currentPoint) : new double[currentPoint.Length];
            for (int i = 0; i < currentPoint.Length; i++)
            {
                currentPoint[i] -= (_options.ContainsKey("learning_rate") ? (double)_options["learning_rate"] : 0.1) * step[i];
            }

            // Check bounds if applicable
            if (bounds != null)
            {
                for (int i = 0; i < currentPoint.Length; i++)
                {
                    currentPoint[i] = Math.Max(bounds[i].Item1, Math.Min(bounds[i].Item2, currentPoint[i]));
                }
            }

            // Evaluate loss
            double loss = objectiveFunction(currentPoint);
            functionEvaluations++;

            // Early stopping if loss does not improve
            if (Math.Abs(loss - bestLoss) < (_options.ContainsKey("tol") ? (double)_options["tol"] : 1e-6))
            {
                break;
            }

            bestLoss = loss;
        }

        return new OptimizerResult
        {
            X = currentPoint,
            Fun = bestLoss,
            Iterations = iterations,
            FunctionEvaluations = functionEvaluations
        };
    }

    private double[] ComputeNumericalGradient(Func<double[], double> objectiveFunction, double[] x)
    {
        double[] gradient = new double[x.Length];
        double epsilon = _options.ContainsKey("eps") ? (double)_options["eps"] : 1e-8;

        for (int i = 0; i < x.Length; i++)
        {
            double[] xPlus = (double[])x.Clone();
            double[] xMinus = (double[])x.Clone();
            xPlus[i] += epsilon;
            xMinus[i] -= epsilon;

            gradient[i] = (objectiveFunction(xPlus) - objectiveFunction(xMinus)) / (2 * epsilon);
        }

        return gradient;
    }

    private bool IsGradientSupported()
    {
        string[] gradientMethods = { "cg", "bfgs", "l-bfgs-b", "trust-ncg", "newton-cg" };
        return gradientMethods.Contains(_method);
    }

    private bool IsBoundsSupported()
    {
        string[] boundsMethods = { "l-bfgs-b", "tnc", "slsqp", "trust-constr", "powell" };
        return boundsMethods.Contains(_method);
    }

    public class OptimizerResult
    {
        public double[] X { get; set; }
        public double Fun { get; set; }
        public int Iterations { get; set; }
        public int FunctionEvaluations { get; set; }
    }

    public object InvokePrivateMethod(string methodName, object[] parameters)
    {
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method != null)
        {
            return method.Invoke(this, parameters);
        }
        throw new ArgumentException($"Method '{methodName}' does not exist.");
    }
}
